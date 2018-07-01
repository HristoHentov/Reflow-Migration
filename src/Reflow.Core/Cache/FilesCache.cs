using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Reflow.Contract.Cache;
using Reflow.Contract.Entity;
using Reflow.Contract.DTO;

namespace Reflow.Core.Cache
{
    public sealed class FilesCache : IReflowCache<ReflowFile>, IEnumerable<ReflowFile>
    {
        private readonly IDictionary<string, ReflowFile> _filesNameIndex;
        private readonly IDictionary<int, ReflowFile> _files;

        private static readonly Lazy<FilesCache> _instance = new Lazy<FilesCache>(() => new FilesCache());

        public static FilesCache Cache => _instance.Value;

        public FilesCache()
        {
            this._files = new ConcurrentDictionary<int, ReflowFile>();
            this._filesNameIndex = new ConcurrentDictionary<string, ReflowFile>();
        }

        public IDictionary<int, ReflowFile> Files => _files;

        public IDictionary<string, ReflowFile> FilesByName => _filesNameIndex;

        public int Size => this._files.Count;

        public ReflowFile this[int index] => this._files[index];
        
        public ReflowFile Find(ReflowFile item)
        {
            return this._files?[item.Id];
        }

        public ReflowFile PopItem(ReflowFile item)
        {
            return this.PopItem(item.Id);
        }

        public ReflowFile PopItem(int id)
        {
            var itemToBeRemoved = this._files?[id];

            if (itemToBeRemoved != null)
                this.TryRemove(itemToBeRemoved);

            return itemToBeRemoved;
        }

        public bool TryAdd(ReflowFile item)
        {
            return this._files.TryAdd(item.Id, item) && this._filesNameIndex.TryAdd(item.FullName, item);
        }

        public bool TryAddRange(IEnumerable<ReflowFile> items)
        {
            var addedItems = new List<ReflowFile>();
            bool fail = false;

            foreach (var item in items)
            {
                if (this.TryAdd(item))
                    addedItems.Add(item);
                else
                {
                    fail = true;
                    break;
                }
            }

            if (fail)
            {
                foreach (var addedItem in addedItems)
                    this.TryRemove(addedItem);

                return false;
            }

            return true;
        }

        public bool TryRemove(ReflowFile item)
        {
            if (item?.Id != null && !string.IsNullOrWhiteSpace(item.FullName))
                return this.TryRemove(item.Id) && this.TryRemove(item.FullName);

            return false;
        }

        public bool TryRemove(int id)
        {
            return this._files.Remove(id);
        }

        public bool TryRemove(string name)
        {
            return this._filesNameIndex.Remove(name);
        }

        public IEnumerator<ReflowFile> GetEnumerator()
        {
            return this._files.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._files.Values.GetEnumerator();
        }

        public bool Flush()
        {
            try
            {
                _files.Clear();
                _filesNameIndex.Clear();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
