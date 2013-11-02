namespace MyStackOverflow.Model
{
    public class User
    {
        public int user_id { get; set; }
        public string user_type { get; set; }
        public int creation_date { get; set; }
        public string display_name { get; set; }
        public int reputation { get; set; }
        public string email_hash { get; set; }
        public int age { get; set; }
        public int last_access_date { get; set; }
        public string website_url { get; set; }
        public string location { get; set; }
        public string about_me { get; set; }
        public int question_count { get; set; }
        public int answer_count { get; set; }
        public int view_count { get; set; }
        public int up_vote_count { get; set; }
        public int down_vote_count { get; set; }
        public int accept_rate { get; set; }
        public string association_id { get; set; }
        public string user_questions_url { get; set; }
        public string user_answers_url { get; set; }
        public string user_favorites_url { get; set; }
        public string user_tags_url { get; set; }
        public string user_badges_url { get; set; }
        public string user_timeline_url { get; set; }
        public string user_mentioned_url { get; set; }
        public string user_comments_url { get; set; }
        public string user_reputation_url { get; set; }
        public BadgeCounts badge_counts { get; set; }
    }
}