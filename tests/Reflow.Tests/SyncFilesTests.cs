using System.IO;
using FluentAssertions;
using Reflow.Core.API;
using Xunit;

namespace Reflow.Tests
{
    public class SyncFilesTests
    {
        private readonly ReflowController _reflow;

        public SyncFilesTests()
        {
            this._reflow = new ReflowController();
        }

        [Fact]
        public void Reflow_SyncFiles_Should_Correctly_Update_Files()
        {
            var filesDirectory = @"..\..\..\GetFilesInDirSample01";
            string update = File.ReadAllText(@"..\..\..\SyncFilesUpdates\SyncFiles.json");

            var initial = this._reflow.GetFilesInDirectory(filesDirectory).Result;

            var apiResponse = this._reflow.SyncFiles(update).Result;

            var res = ParseUtils.ParseJson<bool>(apiResponse);

            var flz = this._reflow.GetFiles(null).Result;

            res.Should().BeTrue();

        }
    }
}
