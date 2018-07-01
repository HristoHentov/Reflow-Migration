using Reflow.Contract.Entity;

namespace Reflow.Models.Options.Base
{
    public class CollectionItem : IListItem
    {
        public CollectionItem(int id, string value, bool @checked)
        {
            Id = id;
            Name = value;
            Default = @checked;
        }

        public int Id { get; }
        public string Name { get; }
        public bool Default { get; set; }
    }
}
