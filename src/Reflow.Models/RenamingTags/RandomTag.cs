using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Reflow.Contract.Entity;
using Reflow.Contract.Attributes;
using Reflow.Contract.DTO;

namespace Reflow.Models.RenamingTags
{
    [ReflowTag(Name = "Random")]
    public class RandomTag : BaseTag
    {
        private static readonly Random _rng = new Random();

        public RandomTag(string name) : base(nameof(RandomTag))
        {
        }

        [JsonConstructor]
        public RandomTag(int randomFrom, int randomTo, bool encodeAsChar) : base(nameof(RandomTag))
        {
            this.RandomFrom = randomFrom;
            this.RandomTo = randomTo;
            this.EncodeAsChar = encodeAsChar;
        }

        [ReflowOption]
        [JsonProperty("Random From")]
        public int RandomFrom { get; set; }

        [ReflowOption]
        [JsonProperty("Random To")]
        public int RandomTo { get; set; }

        [ReflowOption]
        [JsonProperty("Encode As Char")]
        public bool EncodeAsChar { get; set; }


        public override string Render(string fileName, IDictionary<string, ReflowFile> files)
        {
            var rand = _rng.Next(RandomFrom, RandomTo);
            return EncodeAsChar 
                ? ((char)((int) 'A' + (rand % 26))).ToString() 
                : rand.ToString();
        }
    }
}
