using System.IO;
using FluentAssertions;
using Reflow.Core.API;
using Xunit;

namespace Reflow.Tests
{
    public class UpdateTagDataTests
    {
        private readonly ReflowController _reflow;

        public UpdateTagDataTests()
        {
            this._reflow = new ReflowController();
        }

        [Fact]
        public void Reflow_UpdateTagData_Should_Correctly_Update_Tag()
        {
            string update = File.ReadAllText(@"..\..\..\UpdateTagDataUpdates\01.json");

            var apiResponse = this._reflow.UpdateTagsData(update).Result;

            var result = ParseUtils.ParseJson<bool>(apiResponse);

            result.Should().BeFalse("We do not have an initialized NameBuilder collection, so we exepect false.");
        }
    }
}
