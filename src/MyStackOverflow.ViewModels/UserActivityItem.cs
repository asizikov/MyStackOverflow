using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Curacao.MVVM.Commands;
using MyStackOverflow.Model;

namespace MyStackOverflow.ViewModels
{
    public class UserActivityItem
    {
        private readonly IEnumerable<string> _tags;
        private RelayCommand _openCommand;
        private int _id;

        public UserActivityItem(Question question)
        {
            _tags = question.Tags;
            Title = question.Title;
            Score = question.Score;
            ShowCheckMark = question.AcceptedAnswerID != 0;
            _id = question.QuestionID;
            InitCommand();
        }

        public UserActivityItem(Answer answer)
        {
            _tags = Enumerable.Empty<string>();
            Title = answer.Title;
            Score = answer.Score;
            ShowCheckMark = answer.Accepted;
            _id = answer.QuestionID;
            InitCommand();
        }

        private void InitCommand()
        {
            _openCommand = new RelayCommand(_ =>
            {
                if (OnOpen != null)
                {
                    OnOpen(_id);
                }
            });
        }

        public event Action<int> OnOpen;

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

        public RelayCommand OpenCommand
        {
            get { return _openCommand; }
        }
    }
}