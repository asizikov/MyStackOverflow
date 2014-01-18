using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyStackOverflow.Model;

namespace MyStackOverflow.ViewModels
{
    public class UserActivityItem
    {
        private readonly IEnumerable<string> _tags;

        public UserActivityItem(Question question)
        {
            _tags = question.Tags;
            Title = question.Title;
            Score = question.Score;
            ShowCheckMark = question.AcceptedAnswerID != 0;
        }

        public UserActivityItem(Answer answer)
        {
            _tags = Enumerable.Empty<string>();
            Title = answer.Title;
            Score = answer.Score;
            ShowCheckMark = answer.Accepted;
        }

        public string Title { get; private set; }

        public int Score { get; private set; }

        public string TagsList
        {
            get
            {
                var count = _tags.Count();
                if (_tags == null || !_tags.Any()) return string.Empty;

                var sb = new StringBuilder(count);
                var index = 0;
                foreach (var tag in _tags)
                {
                    sb.Append(string.Format("{0}", tag));
                    if (index != count - 1)
                    {
                        sb.Append(", ");
                    }
                    index++;
                }
                return sb.ToString();
            }
        }

        public bool ShowCheckMark { get; private set; }
    }
}