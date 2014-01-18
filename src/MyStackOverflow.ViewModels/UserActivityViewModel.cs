using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels.Commands;
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
        private RelayCommand _loadMore;

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

            LoadMore = new RelayCommand(_ => LoadNext());
            LoadNext();
        }

        private void LoadNext()
        {
            IsLoading = true;
            if (_detailsType == DetailsType.Questions)
            {
                _dataProvider.GetUserQuestionsList(_userId, _currentPage)
                    .Subscribe(responce =>
                    {
                        if (responce != null && responce.Questions.Count != 0)
                        {
                            if (Questions == null)
                            {
                                Questions =
                                    new ObservableCollection<UserActivityItem>(
                                        responce.Questions.Select(q => new UserActivityItem(q)));
                            }
                            else
                            {
                                var list =
                                    new List<UserActivityItem>(responce.Questions.Select(a => new UserActivityItem(a)));
                                foreach (var userActivityItem in list)
                                {
                                    Questions.Add(userActivityItem);
                                }
                            }
                            HasMoreItems = responce.Total > Questions.Count;
                            _currentPage++;
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
                            if (Questions == null)
                            {
                                Questions =
                                    new ObservableCollection<UserActivityItem>(
                                        responce.Answers.Select(a => new UserActivityItem(a)));
                            }
                            else
                            {
                                var list =
                                    new List<UserActivityItem>(responce.Answers.Select(a => new UserActivityItem(a)));
                                foreach (var userActivityItem in list)
                                {
                                    Questions.Add(userActivityItem);
                                }
                            }
                            HasMoreItems = responce.Total > Questions.Count;
                            _currentPage++;
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

        public RelayCommand LoadMore
        {
            get { return _loadMore; }
            private set
            {
                if (Equals(value, _loadMore)) return;
                _loadMore = value;
                OnPropertyChanged("LoadMore");
            }
        }

        [CanBeNull]
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