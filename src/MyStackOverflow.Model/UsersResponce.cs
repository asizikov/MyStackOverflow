using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class UsersResponce
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pagesize")]
        public int Pagesize { get; set; }

        [JsonProperty("users")]
        public List<User> Users { get; set; }
    }
}