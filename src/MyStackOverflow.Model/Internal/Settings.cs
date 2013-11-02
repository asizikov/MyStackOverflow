using Newtonsoft.Json;

namespace MyStackOverflow.Model.Internal
{
    public sealed class Settings
    {
        [JsonProperty("me")]
        public Me Me { get; set; }
    }
}
