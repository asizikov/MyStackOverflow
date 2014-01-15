using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class AnswersResponce
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("pagesize")]
        public int Pagesize { get; set; }
        [JsonProperty("answers")]
        public List<Answer> Answers { get; set; }
    }
}