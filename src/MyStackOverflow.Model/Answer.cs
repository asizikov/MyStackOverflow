using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class Answer
    {
        [JsonProperty("answer_id")]
        public int AnswerID { get; set; }

        [JsonProperty("accepted")]
        public bool Accepted { get; set; }

        [JsonProperty("answer_comments_url")]
        public string AnswerCommentsUrl { get; set; }

        [JsonProperty("question_id")]
        public int QuestionID { get; set; }

        [JsonProperty("creation_date")]
        public int CreationDate { get; set; }

        [JsonProperty("last_activity_date")]
        public int LastActivityDate { get; set; }

        [JsonProperty("up_vote_count")]
        public int UpVoteCount { get; set; }

        [JsonProperty("down_vote_count")]
        public int DownVoteCount { get; set; }

        [JsonProperty("view_count")]
        public int ViewCount { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("community_owned")]
        public bool CommunityOwned { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("last_edit_date")]
        public int? LastEditDate { get; set; }
    }
}