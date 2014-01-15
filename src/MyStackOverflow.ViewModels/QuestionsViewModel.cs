using System;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.Model;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class QuestionsViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly StatisticsService _statistics;
        private readonly int _userId;
        private readonly DetailsType _detailsType;
        private ObservableCollection<Question> _questions;

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
                            Questions = new ObservableCollection<Question>(responce.Questions);
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
                            //Questions = new ObservableCollection<Question>(responce.Questions);
                        }
                        IsLoading = false;
                    }, ex => { IsLoading = false; });
            }
        }

        public ObservableCollection<Question> Questions
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