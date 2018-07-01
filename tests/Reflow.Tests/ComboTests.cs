using System.Collections.Generic;
using FluentAssertions;
using Reflow.Core.API;
using Reflow.Models.Internal;
using Xunit;
using Reflow.Contract.DTO;

namespace Reflow.Tests
{
    public class ComboTests
    {
        private readonly ReflowController _reflow;

        public ComboTests()
        {
            this._reflow = new ReflowController();
        }

        [Fact]
        public void Reflow_WorkflowTest_01()
        {
            var filesLocation = @"..\..\..\ComboSamples\01\Files";
            var tagJson = @"..\..\..\ComboSamples\01\AddTag.json";
            var updateJson = @"..\..\..\ComboSamples\01\UpdateTag.json";

            var filesDirectory = _reflow.GetFilesInDirectory(filesLocation);
            var addTag = _reflow.AddTag(System.IO.File.ReadAllText(tagJson));
            var update = _reflow.UpdateTagsData(System.IO.File.ReadAllText(updateJson));
            var files = _reflow.GetFiles(null).Result;

            var res = ParseUtils.ParseCollection<ReflowFile>(files);

            res.Count.Should().Be(6);
            res[0].NewName.Should().Be("05");
            res[1].NewName.Should().Be("10");
            res[2].NewName.Should().Be("15");
            res[3].NewName.Should().Be("20");
            res[4].NewName.Should().Be("25");
            res[5].NewName.Should().Be("30");
        }
        [Fact]
        public void Reflow_WorkFlowTest_02()
        {
            var filesLocation = @"..\..\..\ComboSamples\02\Files";
            var tagJson = @"..\..\..\ComboSamples\02\AddTag.json";
            var anotherTagJson = @"..\..\..\ComboSamples\02\AnotherTag.json";
            var updateJson = @"..\..\..\ComboSamples\02\UpdateTag.json";

            var filesDirectory = _reflow.GetFilesInDirectory(filesLocation);

            var addTag = _reflow.AddTag(System.IO.File.ReadAllText(tagJson));
            var files1 = _reflow.GetFiles(null).Result;
            var res1 = ParseUtils.ParseCollection<ReflowFile>(files1);

            var addTag2 = _reflow.AddTag(System.IO.File.ReadAllText(anotherTagJson));
            var files2 = _reflow.GetFiles(null).Result;
            var res2 = ParseUtils.ParseCollection<ReflowFile>(files2);

            var update = _reflow.UpdateTagsData(System.IO.File.ReadAllText(updateJson));
            var files = _reflow.GetFiles(null).Result;

            var res = ParseUtils.ParseCollection<ReflowFile>(files);

            res.Count.Should().Be(6);
            res[0].NewName.Should().Be("0Testing");
            res[1].NewName.Should().Be("1Testing");
            res[2].NewName.Should().Be("2Testing");
            res[3].NewName.Should().Be("3Testing");
            res[4].NewName.Should().Be("4Testing");
            res[5].NewName.Should().Be("5Testing");
        }
    }
}
