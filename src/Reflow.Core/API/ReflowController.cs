using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HLog.Contract;
using Reflow.Contract.Modules;
using Reflow.Core.Controllers;
using Reflow.Core.Utility;
using Reflow.Models.Entity;
using Reflow.Models.Entity.Tag;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Reflow.Core.API
{
#pragma warning disable 1998 // API Exposed methods are async to comply with Edge.js requirment for binding V8 <-> CLR

    /// <summary>
    /// Serves as an interface for the front end JS.
    /// </summary>
    public class ReflowController
    {
        #region Ctors

        private ICollection<IReflowController> _components;
        private readonly RenamingController _renamingController;
        private readonly ILog _log;

        public ReflowController()
            : this(null, new DirectoryStructureController(), new RenamingController())
        {
        }

        public ReflowController(ILog log)
            : this(log, new RenamingController(log))
        {
        }

        internal ReflowController(ILog log, params IReflowController[] reflowControllers)
        {
            PokeSingletons();
            Load(reflowControllers);
            foreach (var component in _components)
            {
                component.Initialize();
            }
            _renamingController = (RenamingController)this._components.FirstOrDefault(c => c.GetType() == typeof(RenamingController));
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Gets all files in a given directory (Excluding folders)
        /// </summary>
        /// <param name="directoryPath">Path to the directory to get the files from.</param>
        /// <returns>JSON: CustomFile (Check ReflowFile class to see fields)</returns>
        public async Task<object> GetFilesInDirectory(object directoryPath)
        {
            return _renamingController.GetFiles(directoryPath.ToString());
        }

        /// <summary>
        /// Returns all tags that the app contains.
        /// </summary>
        /// <returns>JSON: TagType, Options - OptionType, OptionName.</returns>
        public async Task<object> GetTags(object optional = null)
        {
            return _renamingController.GetTags();
        }

        /// <summary>
        /// Returns all filters for selecting files.        
        /// </summary>
        /// <returns>JSON: FilterName, FilterOptions </returns>
        public async Task<object> GetFilters(object optional = null)
        {
            return _renamingController.GetFilters();
        }

        /// <summary>
        /// Testing Purposes
        /// </summary>
        /// <param name="optional"></param>
        /// <returns></returns>
        public async Task<object> GetDir(object optional = null)
        {
            try
            {
                return AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            }
            catch (Exception e)
            {
                return String.Format(Consts.APIError, nameof(GetFilesInDirectory), optional, e.Message, e.StackTrace);
            }

        }

        /// <summary>
        /// Returns the amount of files currently loaded.
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetFilesCount(object optional = null)
        {
            return _renamingController.GetFileCount();
        }

        /// <summary>
        /// Returns the currently loaded files in the latest order, names and filtering
        /// </summary>
        /// <param name="optional"></param>
        /// <returns>A collection of ReflowFile objects</returns>
        public async Task<object> GetFiles(object optional = null)
        {
            return _renamingController.GetFiles();
        }

        /// <summary>
        /// Updates the stucture of the renaming tags. Use this to add/remove/swap tag.
        /// </summary>
        /// <param name="json">A json, describing the order of the tags.</param>
        /// <returns>Nothing</returns>
        public async Task<object> UpdateTagsStructure(object json)
        {
            return _renamingController.UpdateTagsStructure(json.ToString());
        }

        /// <summary>
        /// Updates one or more parameters for any given tag.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<object> UpdateTagsData(object json)
        {
            return _renamingController.UpdateTagsData((string)json);
        }

        /// <summary>
        /// Adds a tag to the internal tag collection.
        /// </summary>
        /// <param name="json">The json for the tag</param>
        /// <returns></returns>
        public async Task<object> AddTag(object json)
        {
            return _renamingController.AddTag((string) json);
        }

        /// <summary>
        /// Returns the Renaming options used to set flags for the on-disk renaming.
        /// </summary>
        /// <param name="optional"></param>
        /// <returns>Json describing the options</returns>
        public async Task<object> GetSettings(object optional = null)
        {
            try
            {
                return _renamingController.GetSettings();
            }
            catch (Exception e)
            {
                return String.Format(Consts.APIError, nameof(GetFilesInDirectory), optional, e.Message, e.StackTrace);
            }

        }

        /// <summary>
        /// Performes the actual on-disk renaming of files, taking into account all user preferences.
        /// This overload is for when all user options are saved into variables on the backend.
        /// </summary>
        /// <returns>Return success or error</returns>
        public async Task<object> RenameFiles(object settingsJson)
        {
            return _renamingController.RenameFiles((string)settingsJson);
        }

        public async Task<object> GetProgress(object optional = null)
        {
            return _renamingController.GetProgress();
        }

        public async Task<object> SyncFiles(object files)
        {
            return _renamingController.SyncFiles((string)files);
        }

        #endregion

        #region Private helpers

        private void Load(params IReflowController[] reflowControllers)
        {
            this._components = new HashSet<IReflowController>();
            foreach (var component in reflowControllers)
            {
                this._components.Add(component);
            }
        }

        private static void PokeSingletons()
        {
            var a = NameBuilder.Instance.GetType();
        }

        #endregion
    }
#pragma warning restore 1998
}