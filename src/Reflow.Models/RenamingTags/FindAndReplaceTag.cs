using System.Collections.Generic;
using Newtonsoft.Json;
using Reflow.Contract.Entity;
using Reflow.Contract.Attributes;
using Reflow.Contract.DTO;

namespace Reflow.Models.RenamingTags
{
    [ReflowTag(Name = "Find & Replace")]
    public class FindAndReplaceTag : BaseTag
    {
        public FindAndReplaceTag(string name)
            : base(nameof(FindAndReplaceTag))
        {

        }

        [JsonConstructor]
        public FindAndReplaceTag(string findWhat, string replaceWith, bool matchWord) : base(nameof(FindAndReplaceTag))
        {
            this.FindWhat = findWhat;
            this.ReplaceWith = replaceWith;
            this.MatchWord = matchWord;
        }

        [ReflowOption]
        [JsonProperty("Find What")]
        public string FindWhat { get; set; }

        [ReflowOption]
        [JsonProperty("Replace With")]
        public string ReplaceWith { get; set; }

        [ReflowOption]
        [JsonProperty("Match Word")]
        public bool MatchWord { get; set; }


        public override string Render(string fileName, IDictionary<string, ReflowFile> files)
        {
            if (string.IsNullOrEmpty(FindWhat))
                return fileName;


            if (MatchWord)
            {
                if (fileName == FindWhat)
                    return ReplaceWith;

                fileName.Replace(" " + FindWhat + " ", ReplaceWith);
            }

            return fileName.Replace(FindWhat, ReplaceWith);
        }
    }
}
