using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reflow.Models.Internal;

namespace Reflow.Tests
{
    internal static class ParseUtils
    {
        internal static IList<T> ParseCollection<T>(object reflowResponse)
        {
            var res = ParseApiResponse(reflowResponse);

            var tokens = JArray.Parse(res.ToString());
            return tokens.Select(ParseToken<T>).ToList();
        }

        internal static JArray ParseArray(object json)
        {
            var res = ParseApiResponse(json);

            return JArray.Parse(res.ToString());
        }

        internal static T ParseJson<T>(object apiResponse)
        {
            var res = ParseApiResponse(apiResponse);
            try
            {
                return JsonConvert.DeserializeObject<T>(res.ToString());

            }
            catch (Exception e)
            {
                return (T) res;
            }
        }

        private static T ParseToken<T>(JToken token)
        {
            return JsonConvert.DeserializeObject<T>(token.ToString());
        }

        private static object ParseApiResponse(object json)
        {
            var resAsString = (string)json;
            var reflowResultObj = JsonConvert.DeserializeObject<APIResponse>(resAsString);

            if (reflowResultObj.Error != null)
                throw new Exception($"API returned Error: {reflowResultObj.Error}");

            return reflowResultObj.Response;
        }


    }
}
