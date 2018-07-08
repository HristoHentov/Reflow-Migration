using Newtonsoft.Json;
using Reflow.Contract.DTO;
using Reflow.Contract.Enum;
using Reflow.Core.API;
using Reflow.Models.Internal;
using System.IO;
using Xunit;

namespace Reflow.Tests
{
    public class RenameFilesTests
    {
        private readonly ReflowController _reflow;
        public RenameFilesTests()
        {
            _reflow = new ReflowController();
        }

        [Fact]
        public void Reflow_RenameFiles_Should_Successfully_Rename_Files()
        {
            var filesLocation = @"..\..\..\RenameFilesSample\Files";
            var tagJson = @"..\..\..\RenameFilesSample\AddTag.json";

            var filesDirectory = _reflow.GetFilesInDirectory(filesLocation);
            var addTag = _reflow.AddTag(System.IO.File.ReadAllText(tagJson));
            var files = _reflow.GetFiles(null).Result;
            var res = ParseUtils.ParseCollection<ReflowFile>(files);

            var settings = new ReflowRenameOptionSet()
            {
                CreateBackup = false,
                BackupFolder = "",
                FileExistsStrategy = FileExistsStrategy.Skip,
                FileExistsFolder = ""
            };

            var rename = _reflow.RenameFiles(JsonConvert.SerializeObject(settings)).Result;
        }

        [Fact]
        public void Reflow_RenameFiles_SameName_DifferntTypes_CreateBackup()
        {
            var filesLocation = @"..\..\..\RenameFilesSample\02\FilesSet";
            var tagJson = @"..\..\..\RenameFilesSample\FindAndReplace.json";
            var assertFolderLocation = @"D:\Projects\Reflow\tests\Reflow.Tests\RenameFilesSample\02\AssertFolder";

            try
            {
                var filesDirectory = _reflow.GetFilesInDirectory(filesLocation);
                var addTag = _reflow.AddTag(System.IO.File.ReadAllText(tagJson));
                var files = _reflow.GetFiles(null).Result;
                var res = ParseUtils.ParseCollection<ReflowFile>(files);

                var settings = new ReflowRenameOptionSet()
                {
                    CreateBackup = true,
                    BackupFolder = assertFolderLocation,
                    FileExistsStrategy = FileExistsStrategy.None,
                    FileExistsFolder = ""
                };

                var rename = _reflow.RenameFiles(JsonConvert.SerializeObject(settings)).Result;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                var allFiles = Directory.EnumerateFiles(assertFolderLocation);
                foreach (var file in allFiles)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
