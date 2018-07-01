using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Reflow.Contract.Data;

namespace Reflow.Data
{
    public class DBRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> set;

        public DBRepository(DbSet<T> set)
        {
            this.set = set;
        }

        public IEnumerable<T> Entities => this.set;

        public void Add(T entity)
        {
            this.set.Add(entity);
        }

        public void Add(int key, T entity)
        {
            this.set.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this.set.AddRange(entities);
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return this.set.Any(expression);
        }

        public bool Contains(Expression<Func<T, bool>> expression)
        {
            return this.set.Any(expression);
        }

        public int Count()
        {
            return this.set.Count();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return this.set.Count(expression);
        }

        public T Find(int id)
        {
            return this.set.Find(id);
        }

        public T FirstOrDefault()
        {
            return this.set.FirstOrDefault();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return this.set.FirstOrDefault(expression);
        }

        public void Remove(int id)
        {
            this.set.Remove(this.set.Find(id));
        }

        public void Remove(T entity)
        {
            this.set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            this.set.RemoveRange(entities);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return this.set.Where(expression);
        }
    }
}
