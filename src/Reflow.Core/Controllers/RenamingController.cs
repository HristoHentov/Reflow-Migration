using System;
using System.Runtime.CompilerServices;
using HLog;
using HLog.Contract;
using Reflow.Core.Cache;
using Reflow.Core.Utility;
using Reflow.Core.Services;
using Reflow.Contract.Modules;
using Reflow.Contract.DataExchange;
using Reflow.Core.DataExchnage;

namespace Reflow.Core.Controllers
{
    internal class RenamingController : IReflowController
    {
        private readonly RenamingService _service;
        private readonly IDataExporter<string> _exporter;
        private readonly ILog _log;

        public RenamingController()
            : this(null, new JsonDataExporter(), new RenamingService())
        {
        }

        public RenamingController(ILog log)
           : this(log, new JsonDataExporter(), new RenamingService())
        {
        }

        public RenamingController(ILog log, IDataExporter<string> exporter, RenamingService service)
        {
            this._log = log ?? new ConsoleHLog(); // Until we get DI working and find a way to call ctors from electron.
            this._exporter = exporter;
            this._service = service;
            this._service.Log = _log; // Until we have DI and some Singleton/Factory
        }

        public void Initialize()
        {
        }


        public string GetTags() => this.SafeExecute(_service.GetTags);

        public string GetFilters() => this.SafeExecute(_service.GetFilters);

        public string GetFileCount() => this.SafeExecute(_service.GetFileCount);

        public string RenameFiles(string json) => this.SafeExecute(_service.RenameFiles, json);

        public string UpdateTagsStructure(string json) => this.SafeExecute(_service.UpdateTagsStructureInternal, json);

        public string UpdateTagsData(string json) => this.SafeExecute(_service.UpdateTagsDataInternal, json);

        public string AddTag(string json) => this.SafeExecute(_service.AddTag, json);

        public string GetFiles() => this.SafeExecute(_service.GetFiles);

        public string GetSettings() => this.SafeExecute(_service.GetSettings);

        public string GetProgress() => this.SafeExecute(_service.GetProgress);

        public string GetFiles(string directoryPath) => this.SafeExecute((path) =>
        {
            var files = _service.GetFileNamesByDir(path);
            _service.FillCache(files);
            ReflowStateCache.WorkingDirectory = path;
            return files.Values;

        }, directoryPath);

        public string SyncFiles(string filesJson) => this.SafeExecute((json) =>
        {
            var files = _service.DeserializeFiles(filesJson);
            return _service.FillCache(files);
        }, filesJson);


        private string SafeExecute<T>(Func<T> method, [CallerMemberName] string callerName = "")
        {
            try
            {
                return _exporter.ExportSuccess(method.Invoke());

            }
            catch (Exception e)
            {
                return _exporter.ExportError(String.Format(Consts.APIError, callerName, null, e.Message, e.StackTrace));
            }
        }

        private string SafeExecute<T>(Func<string, T> method, string param, [CallerMemberName] string callerName = "")
        {
            try
            {
                return _exporter.ExportSuccess(method.Invoke(param));

            }
            catch (Exception e)
            {
                return _exporter.ExportError(String.Format(Consts.APIError, callerName, null, e.Message, e.StackTrace));
            }
        }
    }
}
