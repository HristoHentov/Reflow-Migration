using System.Collections.Generic;

namespace Reflow.Contract.Cache
{
    public interface IReflowCache<T>
    {
        bool TryAdd(T item);

        bool TryAddRange(IEnumerable<T> items);

        bool TryRemove(T item);

        bool TryRemove(int id);

        bool TryRemove(string name);

        T PopItem(T item);

        T PopItem(int id);

        T Find(T item);

        int Size { get; }

        T this[int index] { get; }

        bool Flush();
    }
}