using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class QuestionsResponce
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("pagesize")]
        public int Pagesize { get; set; }
        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }
    }
}