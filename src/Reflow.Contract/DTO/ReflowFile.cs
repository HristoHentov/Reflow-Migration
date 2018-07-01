using System;
using System.Diagnostics;

namespace Reflow.Contract.DTO
{
    [DebuggerDisplay("[O: {OriginalName} T: {Type}] -> N: {NewName}")]
    public class ReflowFile : IComparable<ReflowFile>, IEquatable<ReflowFile>
    {
        public ReflowFile(int id, string originalName, string type, string size, bool selected)
        {
            this.Id = id;
            this.OriginalName = originalName;
            this.NewName = NewName;
            this.Type = type;
            this.Size = Size.Parse(size);
            this.Selected = selected;
            this.FullName = originalName + type;
        }

        public int Id { get; }

        public string OriginalName { get; }

        public string NewName { get; set; }

        public string Type { get; }

        public Size Size { get; }

        public bool Selected { get; set; }

        public string FullName { get; }

        public int CompareTo(ReflowFile other)
        {
            return this.FullName.CompareTo(other.FullName);
        }

        public bool Equals(ReflowFile other)
        {
            return this.FullName.Equals(other.FullName);
        }
    }
}
