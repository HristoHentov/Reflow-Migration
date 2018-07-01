using System.IO;
using FluentAssertions;
using Reflow.Core.API;
using Xunit;

namespace Reflow.Tests
{
    public class AddTagTests
    {
        private readonly ReflowController _reflow;

        public AddTagTests()
        {
            this._reflow = new ReflowController();
        }

        [Fact]
        public void Reflow_UpdateTagData_Should_Correctly_Update_Tag()
        {
            string update = File.ReadAllText(@"..\..\..\AddTagUpdates\01.json");

            var apiResponse = this._reflow.AddTag(update).Result;

            var result = ParseUtils.ParseJson<bool>(apiResponse);

            result.Should().BeTrue();
        }
    }
}
