using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyStackOverflow.Model
{
    public class Question
    {
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        [JsonProperty("answer_count")]
        public int AnswerCount { get; set; }
        [JsonProperty("accepted_answer_id")]
        public int AcceptedAnswerID { get; set; }
        [JsonProperty("favorite_count")]
        public int FavoriteCount { get; set; }
        [JsonProperty("question_timeline_url")]
        public string QuestionTimelineUrl { get; set; }
        [JsonProperty("question_comments_url")]
        public string QuestionCommentsUrl { get; set; }
        [JsonProperty("question_answers_url")]
        public string QuestionAnswersUrl { get; set; }
        [JsonProperty("question_id")]
        public int QuestionID { get; set; }
        [JsonProperty("creation_date")]
        public int CreationDate { get; set; }
        [JsonProperty("last_edit_date")]
        public int LastEditDate { get; set; }
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
    }
}