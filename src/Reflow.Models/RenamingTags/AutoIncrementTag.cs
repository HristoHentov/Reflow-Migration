using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Reflow.Contract.Entity;
using Reflow.Models.Internal;
using Reflow.Contract.Attributes;
using Reflow.Contract.DTO;

namespace Reflow.Models.RenamingTags
{ 
    public class AutoIncrementTag : BaseTag
    {
        private int _lastValue;

        public AutoIncrementTag() : this(0, 0, false)
        {
        }

        [JsonConstructor]
        public AutoIncrementTag(int startFrom, int skip, bool hasTrailingZero) : base(nameof(AutoIncrementTag))
        {
            this.StartFrom = startFrom;
            this.Skip = skip;

            this.HasTrailingZero = hasTrailingZero;
            this._lastValue = this.StartFrom - Skip - 1; // Adding skip later in the beginning of ToString()
        }

        [JsonProperty("Start From")]
        [ReflowOption]
        public int StartFrom { get; set; }

        [ReflowOption]
        public int Skip { get; set; }

        [JsonProperty("Has Leading Zero")]
        [ReflowOption]
        public bool HasTrailingZero { get; set; }



        public override string Render(string fileName, IDictionary<string, ReflowFile> files)
        {
            if (fileName == files.First().Key) // temp hack until new version of api
                _lastValue = this.StartFrom - Skip - 1;

            _lastValue += Skip + 1;


            return HasTrailingZero
                ? _lastValue.ToString().PadLeft(GetTrailingZeroes(files), '0')
                : _lastValue.ToString();
        }

        private int GetTrailingZeroes(IDictionary<string, ReflowFile> files)
        {
            if (files == null)
                return 0;

            var count = files.Count;
            var result = Math.Max(StartFrom, count * (Skip - 1) + StartFrom); // Not a good formula, but will do for now.

            return result.ToString().Length;
        }
    }
}
