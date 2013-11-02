using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class User
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("user_type")]
        public string UserType { get; set; }

        [JsonProperty("creation_date")]
        public int CreationDate { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("email_hash")]
        public string EmailHash { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("last_access_date")]
        public int LastAccessDate { get; set; }

        [JsonProperty("website_url")]
        public string WebsiteUrl { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("about_me")]
        public string AboutMe { get; set; }

        [JsonProperty("question_count")]
        public int QuestionCount { get; set; }

        [JsonProperty("answer_count")]
        public int AnswerCount { get; set; }

        [JsonProperty("view_count")]
        public int ViewCount { get; set; }

        [JsonProperty("up_vote_count")]
        public int UpVoteCount { get; set; }

        [JsonProperty("down_vote_count")]
        public int DownVoteCount { get; set; }

        [JsonProperty("accept_rate")]
        public int AcceptRate { get; set; }

        [JsonProperty("association_id")]
        public string AssociationID { get; set; }

        [JsonProperty("user_questions_url")]
        public string UserQuestionsUrl { get; set; }

        [JsonProperty("user_answers_url")]
        public string UserAnswersUrl { get; set; }

        [JsonProperty("user_favorites_url")]
        public string UserFavoritesUrl { get; set; }

        [JsonProperty("user_tags_url")]
        public string UserTagsUrl { get; set; }

        [JsonProperty("user_badges_url")]
        public string UserBadgesUrl { get; set; }

        [JsonProperty("user_timeline_url")]
        public string UserTimelineUrl { get; set; }

        [JsonProperty("user_mentioned_url")]
        public string UserMentionedUrl { get; set; }

        [JsonProperty("user_comments_url")]
        public string UserCommentsUrl { get; set; }

        [JsonProperty("user_reputation_url")]
        public string UserReputationUrl { get; set; }

        [JsonProperty("badge_counts")]
        public BadgeCounts BadgeCounts { get; set; }
    }
}