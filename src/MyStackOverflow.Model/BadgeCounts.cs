using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class BadgeCounts
    {
        [JsonProperty("gold")]
        public int Gold { get; set; }

        [JsonProperty("silver")]
        public int Silver { get; set; }

        [JsonProperty("bronze")]
        public int Bronze { get; set; }
    }
}