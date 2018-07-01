namespace Reflow.Models.Entity
{
    public class File
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string Path { get; set; }
        public double? Size { get; set; }
        public string SizeScale { get; set; }
    }
}
