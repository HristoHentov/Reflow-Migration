using FluentAssertions;
using Reflow.Contract.Entity;
using Xunit;
using Reflow.Core.API;

namespace Reflow.Tests
{
    public class GetTagsTests
    {
        private readonly ReflowController _reflow;

        public GetTagsTests()
        {
            this._reflow = new ReflowController();
        }

        [Fact]
        public void Reflow_GetTags_Returns_All_Tags()
        {
            var apiResult = _reflow.GetTags(null).Result;

            var tagsJson = ParseUtils.ParseArray(apiResult);
            tagsJson.Count.Should().Be(4);
        }
    }
}
