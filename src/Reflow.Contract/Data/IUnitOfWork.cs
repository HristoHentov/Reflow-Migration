using Reflow.Contract.Entity;

namespace Reflow.Contract.Data
{
    public interface IUnitOfWork
    {
        IRepository<ITagOption> Options { get; }

        IRepository<ITagModel> Tags { get; }

        IRepository<IFile> Files { get; }

        IRepository<IFilter> Filters { get; }

        int SaveChanges();
    }
}
