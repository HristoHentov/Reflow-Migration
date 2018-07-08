using System.Collections;
using System.Collections.Generic;

namespace Reflow.Contract.Files
{
    public interface IFileLayer
    {
        bool Rename(string directory, string originalName, string newName, string fileType);

        bool Rename(string originalFullName, string newFullName);

        bool Move(string originalDirectory, string newDirectory, string fileName, string fileType);

        bool Copy(string originalDirectory, string copyDirectory, string fileName, string fileType);

        bool Exists(string directory, string fileName, string fileType);

        IEnumerable<string> GetFilesInDirectory(string directoryPath);
    }
}
