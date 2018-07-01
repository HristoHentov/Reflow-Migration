using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reflow.Contract.Entity;
using Reflow.Core.API;
using Reflow.Models.Internal;
using Xunit;
using FluentAssertions;
using Reflow.Contract.DTO;

namespace Reflow.Tests
{
    public class FilesInDirectoryTests
    {
        private readonly ReflowController _reflow;

        public FilesInDirectoryTests()
        {
            _reflow = new ReflowController();
        }

        [Fact]
        public void Reflow_GetFilesInDirectory_Returns_All_Files_InDirectory()
        {
            var filesDirectory = @"..\..\..\GetFilesInDirSample01";
            var reflowResult = _reflow.GetFilesInDirectory(filesDirectory).Result;

            var files = ParseUtils.ParseCollection<ReflowFile>(reflowResult);

            files.Should().NotBeNull();
            files.Count.Should().Be(6);


            FileIsCorrect(files, 0, "ExampleDocxFile", "docx");
            FileIsCorrect(files, 1, "ExampleExeFile", "exe");
            FileIsCorrect(files, 2, "ExampleFileNoExtension", "NONE");
            FileIsCorrect(files, 3, "ExampleSvgFile", "svg");
            FileIsCorrect(files, 4, "ExampleTextDocument", "txt");
            FileIsCorrect(files, 5, "ExampleZipFile", "zip");
        }

        [Fact]
        public void Reflow_GetFilesInDirectory_Does_Not_Crash_On_Empty_Dir()
        {
            var filesDirectory = @"..\..\..\GetFilesInDirSample02";
            var reflowResult = _reflow.GetFilesInDirectory(filesDirectory).Result;

            var files = ParseUtils.ParseCollection<ReflowFile>(reflowResult);

            files.Should().NotBeNull();
            files.Count.Should().Be(0);
        }



        private void FileIsCorrect(IList<ReflowFile> files, int i, string name, string type)
        {
            files[i].OriginalName.Should().Be(name);
            files[i].NewName.Should().Be(name);
            files[i].Selected.Should().Be(true);
            files[i].Id.Should().Be(i);
            files[i].Type.Should().Be(type);
        }

    }
}
