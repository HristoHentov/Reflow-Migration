using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reflow.Contract.Data;
using System.Collections.Concurrent;
using System.Linq;

namespace Reflow.Data
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        private IList<T> _entries = new List<T>();

        public IEnumerable<T> Entities => _entries;

        public InMemoryRepository(Func<IEnumerable<T>> loader)
        {
            this._entries = new List<T>(loader.Invoke());
        }

        public void Add(T entity)
        {
            this._entries.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                this.Add(item);
            }
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return false;
        }

        public bool Contains(Expression<Func<T, bool>> expression)
        {
            return false;
        }

        public int Count()
        {
            return 0;
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return 0;
        }

        public T Find(int id)
        {
            return default(T);
        }

        public T FirstOrDefault()
        {
            return default(T);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return default(T);
        }

        public void Remove(T entity)
        {
        }

        public void Remove(int id)
        {
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Enumerable.Empty<T>();
        }
    }
}
