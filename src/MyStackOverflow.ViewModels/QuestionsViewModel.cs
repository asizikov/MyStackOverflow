using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.Model;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class ListItem
    {

        private IEnumerable<string> _tags;

        public ListItem(Question question)
        {
            _tags = question.Tags;
            Title = question.Title;
            Score = question.Score;
        }

        public ListItem(Answer answer)
        {
            _tags = Enumerable.Empty<string>();
            Title = answer.Title;
            Score = answer.Score;

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

    }

    public class QuestionsViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly StatisticsService _statistics;
        private readonly int _userId;
        private readonly DetailsType _detailsType;
        private ObservableCollection<ListItem> _questions;

        public QuestionsViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] StatisticsService statistics,
            [NotNull] AsyncDataProvider dataProvider, int userId, DetailsType detailsType)
            : base(dispatcher)
        {
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            _statistics = statistics;
            _dataProvider = dataProvider;
            _userId = userId;
            _detailsType = detailsType;

            Init();
        }

        private void Init()
        {
            IsLoading = true;
            if (_detailsType == DetailsType.Questions)
            {
                _dataProvider.GetUserQuestionsList(_userId)
                    .Subscribe(responce =>
                    {
                        if (responce != null && responce.Questions.Count != 0)
                        {
                            Questions = new ObservableCollection<ListItem>(responce.Questions.Select(q => new ListItem(q)));
                        }
                        IsLoading = false;
                    }, ex => { IsLoading = false; });
            }
            else
            {
                _dataProvider.GetUserAnswersList(_userId)
                    .Subscribe(responce =>
                    {
                        if (responce != null && responce.Answers.Count != 0)
                        {
                            Questions = new ObservableCollection<ListItem>(responce.Answers.Select(a => new ListItem(a)));
                        }
                        IsLoading = false;
                    }, ex => { IsLoading = false; });
            }
        }

        public ObservableCollection<ListItem> Questions
        {
            get { return _questions; }
            private set
            {
                if (Equals(value, _questions)) return;
                _questions = value;
                OnPropertyChanged("Questions");
            }
        }
    }
}