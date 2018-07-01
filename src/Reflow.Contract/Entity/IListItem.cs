namespace Reflow.Contract.Entity
{
    public interface IListItem
    {
        int Id { get; }

        string Name { get; }

        bool Default { get; set; }
    }
}
