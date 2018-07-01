using System.Collections.Generic;

namespace Reflow.Contract.Cache
{
    public interface IInMemoryRepository<TItem>
    {
        IEnumerable<TItem> Load();
    }
}