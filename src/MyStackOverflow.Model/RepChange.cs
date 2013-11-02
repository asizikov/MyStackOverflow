namespace MyStackOverflow.Model
{
    public class RepChange
    {
        public int user_id { get; set; }
        public int post_id { get; set; }
        public string post_type { get; set; }
        public string title { get; set; }
        public int positive_rep { get; set; }
        public int negative_rep { get; set; }
        public int on_date { get; set; }
    }
}