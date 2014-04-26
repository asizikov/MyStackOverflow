using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Curacao.Mvvm.Abstractions.Services;
using Curacao.Mvvm.Commands;
using Curacao.Mvvm.ViewModel;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class UserActivityViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly DetailsType _detailsType;
        private readonly StatisticsService _statistics;
        private readonly IStringsProvider _stringsProvider;
        private readonly IPhoneTasks _tasks;
        private readonly int _userId;
        private ObservableCollection<UserActivityItem> _activityItems;
        private int _currentPage = 1;
        private bool _hasMoreItems;
        private RelayCommand _loadMoreCommand;

        public UserActivityViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] StatisticsService statistics,
            [NotNull] AsyncDataProvider dataProvider, [NotNull] IStringsProvider stringsProvider,
            [NotNull] IPhoneTasks tasks ,int userId,
            DetailsType detailsType)
            : base(dispatcher)
        {
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            if (tasks == null) throw new ArgumentNullException("tasks");
            _statistics = statistics;
            _dataProvider = dataProvider;
            _stringsProvider = stringsProvider;
            _tasks = tasks;
            _userId = userId;
            _detailsType = detailsType;
            _statistics.PublishActivityPageLoaded(_detailsType == DetailsType.Questions);
            LoadMoreCommand = new RelayCommand(_ => LoadNext());
            LoadNext();
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

        public RelayCommand LoadMoreCommand
        {
            get { return _loadMoreCommand; }
            private set
            {
                if (Equals(value, _loadMoreCommand)) return;
                _loadMoreCommand = value;
                OnPropertyChanged("LoadMoreCommand");
            }
        }

        [CanBeNull]
        public ObservableCollection<UserActivityItem> Questions
        {
            get { return _activityItems; }
            private set
            {
                if (Equals(value, _activityItems)) return;
                _activityItems = value;
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
                                        responce.Questions.Select(q =>
                                        {
                                            var item = new UserActivityItem(q);
                                            item.OnOpen += Open;
                                            return item;
                                        }));
                                _currentPage++;
                                HasMoreItems = responce.Total > Questions.Count;
                            }
                            else
                            {
                                if (responce.Page == _currentPage)
                                {
                                    var list =
                                        new List<UserActivityItem>(
                                            responce.Questions.Select(a =>
                                            {
                                                var item = new UserActivityItem(a);
                                                item.OnOpen += Open;
                                                return item;
                                            }));
                                    AppendListAndUpdateProperties(list, responce.Total);
                                }
                            }
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
                                        responce.Answers.Select(a =>
                                        {
                                            var item = new UserActivityItem(a);
                                            item.OnOpen += Open;
                                            return item;
                                        }));
                                _currentPage++;
                                HasMoreItems = responce.Total > Questions.Count;
                            }
                            else
                            {
                                if (responce.Page == _currentPage)
                                {
                                    var list =
                                        new List<UserActivityItem>(responce.Answers.Select(a =>
                                        {
                                            var item = new UserActivityItem(a);
                                            item.OnOpen += Open;
                                            return item;
                                        }));
                                    AppendListAndUpdateProperties(list, responce.Total);
                                }
                            }
                        }
                        IsLoading = false;
                    }, ex => { IsLoading = false; });
            }
        }

        private void Open(int id)
        {
            var url = "http://stackoverflow.com/questions/";
            url += id.ToString(CultureInfo.InvariantCulture);
            _tasks.OpenUrl(url);
        }

        private void AppendListAndUpdateProperties(IEnumerable<UserActivityItem> list, int total)
        {
            Dispatcher.InvokeOnUIifNeeded(() =>
            {
                if (Questions == null)
                {
                    Questions = new ObservableCollection<UserActivityItem>();
                }
                foreach (UserActivityItem userActivityItem in list)
                {
                    Questions.Add(userActivityItem);
                }
                HasMoreItems = total > Questions.Count;
            });
            _currentPage++;
        }
    }
}