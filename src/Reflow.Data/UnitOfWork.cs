using Reflow.Contract.Data;
using Reflow.Contract.Entity;
using Reflow.Models.Entity;

namespace Reflow.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReflowContext context;
        private IRepository<ITagModel> tags;
        private IRepository<ITagOption> options;
        private IRepository<IFile> files;
        private IRepository<IFilter> filters;

        public UnitOfWork()
        {
            //Poke lazy
            var options = this.Options;
            var tags = this.Tags;
        }

        public IRepository<ITagOption> Options
            => this.options ?? (this.options = new InMemoryRepository<ITagOption>(DynamicTagParser.GetOptions));
        public IRepository<ITagModel> Tags
            => this.tags ?? (this.tags = new InMemoryRepository<ITagModel>(DynamicTagParser.GetTags));
        public IRepository<IFile> Files
            => this.files ?? (this.files = new DBRepository<IFile>(this.context.Files));
        public IRepository<IFilter> Filters
            => this.filters ?? (this.filters = new DBRepository<IFilter>(this.context.Filters));

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
