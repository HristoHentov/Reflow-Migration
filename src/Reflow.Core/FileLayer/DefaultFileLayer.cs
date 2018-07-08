using System;
using System.Collections.Generic;
using System.IO;
using HLog.Contract;
using Reflow.Contract.Files;


namespace Reflow.Core.FileLayer
{
    public class DefaultFileLayer : IFileLayer
    {

        public bool Copy(string originalDirectory, string copyDirectory, string fileName, string fileType)
        {
            try
            {
                var fullName = $"{fileName}.{fileType}";
                File.Copy(Path.Combine(originalDirectory, fullName), Path.Combine(copyDirectory, fullName));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed moving file {fileName}.{fileType} to {copyDirectory}. Origin directory {originalDirectory}. Exception Message: {e.Message}", e);
            }
            return true;
        }

        public bool Exists(string directory, string fileName, string fileType)
        {
            return File.Exists(Path.Combine(directory, fileName, fileType));
        }

        public IEnumerable<string> GetFilesInDirectory(string directoryPath)
        {
            return Directory.EnumerateFiles(directoryPath);
        }

        public bool Move(string originalDirectory, string newDirectory, string fileName, string fileType)
        {
            try
            {
                var fullName = $"{fileName}.{fileType}";
                File.Move(Path.Combine(originalDirectory, fullName), Path.Combine(newDirectory, fullName));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed moving file {fileName}.{fileType} to {newDirectory}. Origin directory {originalDirectory}. Exception Message: {e.Message}", e);
            }
            return true;
        }

        public bool Rename(string originalFullName, string newFullName)
        {
            try
            {
                File.Move(originalFullName, newFullName);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed renaming file {originalFullName} to {newFullName}. Exception Message: {e.Message}", e);
            }
            return true;
        }

        public bool Rename(string directory, string originalName, string newName, string fileType)
        {
            try
            {
                File.Move(Path.Combine(directory, originalName + "." + fileType),
                          Path.Combine(directory, newName + "." + fileType));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed renaming file {originalName} to {newName}. FileType {fileType}. Exception Message: {e.Message}");
            }
            return true;
        }
    }
}
