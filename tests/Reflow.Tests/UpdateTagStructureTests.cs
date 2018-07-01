using System.IO;
using FluentAssertions;
using Reflow.Core.API;
using Xunit;

namespace Reflow.Tests
{
    public class UpdateTagStructureTests
    {
        private readonly ReflowController _reflow;

        public UpdateTagStructureTests()
        {
            this._reflow = new ReflowController();
        }

        [Fact]
        public void Reflow_UpdateTagStructure_Should_Correctly_Update_Tag()
        {
            string update = File.ReadAllText(@"..\..\..\UpdateTagStructureUpdates\01.json");

            var apiResponse = this._reflow.UpdateTagsStructure(update).Result;

            var res = ParseUtils.ParseJson<bool>(apiResponse);

            res.Should().BeTrue();

        }
    }
}
