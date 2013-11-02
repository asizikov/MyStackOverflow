using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class Badge
    {
        public int badge_id { get; set; }
        public string rank { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public int award_count { get; set; }
        public bool tag_based { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        public string badges_recipients_url { get; set; }
    }
}