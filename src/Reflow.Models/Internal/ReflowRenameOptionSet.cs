using Reflow.Contract.Enum;
using Reflow.Contract.Entity;

namespace Reflow.Models.Internal
{
    public class ReflowRenameOptionSet : IRenameOptionsSet
    {
        public bool CreateBackup { get; set; }

        public string BackupFolder { get; set; }

        public FileExistsStrategy FileExistsStrategy { get; set; }

        public string FileExistsFolder { get; set; }
    }

}
