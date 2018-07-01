using Reflow.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Reflow.Contract.Entity;

namespace Reflow.Data
{
    public partial class ReflowContext : DbContext
    {
        public virtual DbSet<IFile> Files { get; set; }
        public virtual DbSet<IOption> Options { get; set; }
        public virtual DbSet<ITag> Tags { get; set; }
        public virtual DbSet<IFilter> Filters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=reflow.db");
        }
    }
}
