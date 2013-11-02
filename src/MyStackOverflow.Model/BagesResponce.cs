using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class BagesResponce
    {
        [JsonProperty("badges")]
        public List<Badge> Badges { get; set; }
    }
}