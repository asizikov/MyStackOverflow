using Newtonsoft.Json;

namespace MyStackOverflow.Model.Internal
{
    public sealed class Me
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}