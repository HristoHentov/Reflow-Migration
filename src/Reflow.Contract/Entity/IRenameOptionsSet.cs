using Reflow.Contract.Enum;

namespace Reflow.Contract.Entity
{
    public interface IRenameOptionsSet
    {
        bool CreateBackup { get; set; }

        string BackupFolder { get; set; }

        FileExistsStrategy FileExistsStrategy { get; set; }

        string FileExistsFolder { get; set; }
    }
}
