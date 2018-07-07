using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HLog.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reflow.Contract.Data;
using Reflow.Contract.DataExchange;
using Reflow.Contract.Entity;
using Reflow.Contract.Enum;
using Reflow.Core.Cache;
using Reflow.Core.DataExchnage;
using Reflow.Core.Utility;
using Reflow.Data;
using Reflow.Models.Entity;
using Reflow.Models.Entity.Tag;
using Reflow.Models.Internal;
using Reflow.Contract.DTO;
using Reflow.Models.RenamingTags;

namespace Reflow.Core.Services
{
    internal class RenamingService
    {
        private readonly IUnitOfWork _database;
        private readonly IDataImporter _importer;
        private readonly IDictionary<string, Type> _tagTypes;

        private int _filesProgess;
        //private readonly IInMemoryRepository<Tag> _tags;

        public RenamingService()
        {
            this._database = new UnitOfWork();
            this._importer = new JsonDataImporter();
            this._tagTypes = LoadTagsFromAssembly();

            this._filesProgess = 0;
        }

        private IDictionary<string, Type> LoadTagsFromAssembly()
        {
            var ts = new AutoIncrementTag();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = assemblies.First(a => a.FullName.Split(',')[0] == Consts.ModelsAssemblyName);

            return assembly.ExportedTypes
                .Where(t => typeof(ITag).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToDictionary(t => t.Name);
        }

        internal ILog Log { get; set; }

        internal IDictionary<string, ReflowFile> GetFileNamesByDir(string path)
        {
            try
            {
                var inFiles = Directory.EnumerateFiles(path);
                var files = new Dictionary<string, ReflowFile>();

                var index = 0;
                foreach (var file in inFiles)
                {
                    try
                    {
                        var fileName = Utils.GetFullFilename(file);
                        var fileModel = new ReflowFile(
                            id: index++,
                            originalName: fileName[0],
                            type: fileName[1] ?? "NONE",
                            size: GetFileSize(file),
                            selected: true
                            );

                        files.Add(fileModel.FullName, fileModel);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Failed reding file {file}. Exception message: {e.Message}");
                    }
                }

                return files;
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed parsing files in {path}. Please check names. Exception message: {ex.Message}");
                throw;
            }
        }

        internal void ResetData()
        {
            NameBuilder.Instance.Flush();
            FileProgress.Instance.Reset();
        }

        internal IEnumerable<ITagModel> GetTags()
        {
            return _database.Tags.Entities.ToList();
        }

        public IEnumerable<IFilter> GetFilters()
        {
            return _database.Filters.Entities;
        }

        private string GetFileSize(string filePath)
        {
            long chunk = 1024L;
            long oneKb = chunk;
            long oneMb = chunk * oneKb;
            long oneGb = chunk * oneMb;
            long oneTb = chunk * oneGb;

            var size = double.Parse(new FileInfo(filePath).Length.ToString());
            if (size < 0L)
                throw new ArgumentException("Negative file size!");
            if (size >= 0L && size <= oneKb)
                return $"{(int)size / oneKb} KB";
            if (size > oneKb && size <= oneMb)
                return $"{(int)size / oneKb} KB";

            if (size > oneMb && size <= oneGb)
                return $"{size / oneMb:f2} MB";
            if (size > oneGb && size <= oneTb)
                return $"{size / oneGb:f2} GB";
            if (size > oneGb && size <= oneTb)
                return $"{size / oneTb:f2} TB";

            return "1+ PB";
        }

        public bool FillCache(IDictionary<string, ReflowFile> files)
        {
            if (FilesCache.Cache.Size > 0)
                FilesCache.Cache.Flush();

            FilesCache.Cache.TryAddRange(files.Values);
            return true;
        }

        public int GetFileCount()
        {
            return FilesCache.Cache.Size;
        }

        public async Task<string> RenameFiles(string settingsJson)
        {
            var settings = _importer.ParseSettings(settingsJson);
            if (settings.CreateBackup)
            {
                foreach (var file in FilesCache.Cache)
                {
                    BackupFile(file.OriginalName, file.Type, settings.BackupFolder);
                }
            }

            this._filesProgess = 0;
            foreach (var file in FilesCache.Cache)
            {
                var shouldBreak = false;
                if (FileExists(file))
                {
                    switch (settings.FileExistsStrategy)
                    {
                        case FileExistsStrategy.Skip: continue;
                        case FileExistsStrategy.Replace: break;
                        case FileExistsStrategy.MoveOriginalToFolder: MoveOriginalFile(file.OriginalName, file.Type, settings.FileExistsFolder); break;
                        case FileExistsStrategy.KeepBoth: RenameFile(file.OriginalName, file.NewName + "(1)", file.Type); continue;
                        default: break;
                    }
                }

                if (shouldBreak)
                    break;

                await Task.Run(() => RenameFile(file.OriginalName, file.NewName, file.Type));
                FileProgress.Instance.UpdateProgress();
            }

            return "Success";
        }

        private bool MoveOriginalFile(string filename, string fileType, string newFolder)
        {
            try
            {
                var fullName = $"{filename}.{fileType}";
                System.IO.File.Move
                (Path.Combine(ReflowStateCache.WorkingDirectory, fullName),
                    Path.Combine(newFolder, fullName));
            }
            catch (Exception e)
            {
                Log.Error($"Failed backing up file {filename} to {newFolder}. FileType {fileType}. Exception Message: {e.Message}");
                throw;
            }
            return true;
        }

        private bool FileExists(ReflowFile file)
        {
            return System.IO.File.Exists(Path.Combine(ReflowStateCache.WorkingDirectory, file.NewName, file.Type));
        }

        private bool BackupFile(string filename, string fileType, string newLocation)
        {
            try
            {
                var fullName = $"{filename}.{fileType}";

                System.IO.File.Copy
                           (Path.Combine(ReflowStateCache.WorkingDirectory, fullName),
                             Path.Combine(newLocation, fullName));
            }
            catch (Exception e)
            {
                //Log.Error($"Failed moving file {filename} to {newLocation}. FileType {fileType}. Exception Message: {e.Message}");
                throw new Exception($"Failed moving file {filename} to {newLocation}. FileType {fileType}. Exception Message: {e.Message}", e);
            }
            return true;
        }

        private bool RenameFile(string oldName, string newName, string fileType)
        {
            try
            {
                System.IO.File.Move
                           (Path.Combine(ReflowStateCache.WorkingDirectory, oldName + "." + fileType),
                             Path.Combine(ReflowStateCache.WorkingDirectory, newName + "." + fileType));
            }
            catch (Exception e)
            {
                Log.Error($"Failed renaming file {oldName} to {newName}. FileType {fileType}. Exception Message: {e.Message}");
                return false;
            }
            return true;
        }

        public bool AddTag(string json)
        {
            var update = JsonConvert.DeserializeObject<TagRequest>(json);
            var options = JObject.Parse(json)["Options"].ToString();
            var tagInfo = _importer.ParseTag(update.TagType, options, _tagTypes);

            try
            {
                NameBuilder.Instance.Tags.Add(tagInfo);
                this.UpdateFiles();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateTagsStructureInternal(string tagsJson)
        {
            try
            {
                NameBuilder.Instance.Tags = _importer.ParseTagCollection(tagsJson, _tagTypes);
                this.UpdateFiles();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateTagsDataInternal(string json)
        {
            try
            {
                var update = JsonConvert.DeserializeObject<TagRequest>(json);
                var options = JObject.Parse(json)["Options"].ToString();
                var tagInfo = _importer.ParseTag(update.TagType, options, _tagTypes);

                if (update.OrderId >= 0 && update.OrderId < NameBuilder.Instance.Tags.Count)
                {
                    NameBuilder.Instance.Tags[update.OrderId] = tagInfo;
                    this.UpdateFiles();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void UpdateFiles()
        {
            NameBuilder.Instance.Resolve(FilesCache.Cache.FilesByName);
        }

        public ICollection<ReflowFile> GetFiles()
        {
            return FilesCache.Cache.FilesByName.Values;
        }

        public List<ReflowOption> GetSettings()
        {
            var options = new List<ReflowOption>();

            options.Add(new ReflowOption("Create backup?", "CheckBox", 0, new[] { "Yes", "No" }));
            options.Add(new ReflowOption("Backup location", "Filepath", 0, new[] { Directory.GetCurrentDirectory().ToString() }));
            options.Add(new ReflowOption("If new name exists", "List", 0, new[] { "Skip", "Overwrite", "Move old file to directory" }));
            options.Add(new ReflowOption("Old files location", "Filepath", 0, new[] { Directory.GetCurrentDirectory().ToString() }));

            return options;
        }

        public IEnumerable<ITag> GetTagsInMemory()
        {
            //return _tags.Load();
            return null;
        }

        public IDictionary<string, ReflowFile> DeserializeFiles(string filesJson)
        {
            return _importer.ParseFiles(filesJson);
        }

        public double GetProgress()
        {
            return FilesCache.Cache.Size == 0
                ? 0
                : Math.Round(FileProgress.Instance.GetProgress() / (double)FilesCache.Cache.Size, 3);
        }


    }
}
