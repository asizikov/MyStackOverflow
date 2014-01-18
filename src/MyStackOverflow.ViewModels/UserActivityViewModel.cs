using System;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class UserActivityViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly IStringsProvider _stringsProvider;
        private readonly StatisticsService _statistics;
        private readonly int _userId;
        private readonly DetailsType _detailsType;
        private ObservableCollection<UserActivityItem> _questions;
        private bool _hasMoreItems;
        private int _currentPage = 1;

        public UserActivityViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] StatisticsService statistics,
            [NotNull] AsyncDataProvider dataProvider, [NotNull] IStringsProvider stringsProvider, int userId,
            DetailsType detailsType)
            : base(dispatcher)
        {
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            _statistics = statistics;
            _dataProvider = dataProvider;
            _stringsProvider = stringsProvider;
            _userId = userId;
            _detailsType = detailsType;

            Init();
        }

        private void Init()
        {
            IsLoading = true;
            if (_detailsType == DetailsType.Questions)
            {
                _dataProvider.GetUserQuestionsList(_userId, _currentPage)
                    .Subscribe(responce =>
                    {
                        if (responce != null && responce.Questions.Count != 0)
                        {
                            Questions =
                                new ObservableCollection<UserActivityItem>(
                                    responce.Questions.Select(q => new UserActivityItem(q)));
                            HasMoreItems = responce.Total > Questions.Count;
                        }
                        IsLoading = false;
                    }, ex => { IsLoading = false; });
            }
            else
            {
                _dataProvider.GetUserAnswersList(_userId, _currentPage)
                    .Subscribe(responce =>
                    {
                        if (responce != null && responce.Answers.Count != 0)
                        {
                            Questions =
                                new ObservableCollection<UserActivityItem>(
                                    responce.Answers.Select(a => new UserActivityItem(a)));
                            HasMoreItems = responce.Total > Questions.Count;
                        }
                        IsLoading = false;
                    }, ex => { IsLoading = false; });
            }
        }

        public bool HasMoreItems
        {
            get { return _hasMoreItems; }
            private set
            {
                if (value.Equals(_hasMoreItems)) return;
                _hasMoreItems = value;
                OnPropertyChanged("HasMoreItems");
            }
        }

        public ObservableCollection<UserActivityItem> Questions
        {
            get { return _questions; }
            private set
            {
                if (Equals(value, _questions)) return;
                _questions = value;
                OnPropertyChanged("Questions");
            }
        }

        public string Title
        {
            get
            {
                return _detailsType == DetailsType.Questions
                    ? _stringsProvider.QuestionsTitle
                    : _stringsProvider.AnswersTitle;
            }
        }
    }
}