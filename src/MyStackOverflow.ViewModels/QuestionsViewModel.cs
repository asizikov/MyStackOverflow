using System;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.Model;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class QuestionsViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly StatisticsService _statistics;
        private readonly int _userId;

        public QuestionsViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] StatisticsService statistics,
            [NotNull] AsyncDataProvider dataProvider, int userId)
            : base(dispatcher)
        {
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            _statistics = statistics;
            _dataProvider = dataProvider;
            _userId = userId;

            Init();
        }

        private void Init()
        {
            IsLoading = true;
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

        public ObservableCollection<Question> Questions { get; private set; } 
    }
}