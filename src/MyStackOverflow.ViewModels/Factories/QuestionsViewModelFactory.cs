﻿using System;
using Curacao.Mvvm.Abstractions.Services;
using Curacao.Mvvm.ViewModel;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels.Factories
{
    public class QuestionsViewModelFactory
    {
        private readonly ISystemDispatcher _systemDispatcher;
        private readonly StatisticsService _statistics;
        private readonly AsyncDataProvider _dataProvider;
        private readonly IStringsProvider _stringsProvider;
        private readonly IPhoneTasks _phoneTasks;

        public QuestionsViewModelFactory([NotNull] ISystemDispatcher systemDispatcher,
            [NotNull] StatisticsService statistics, [NotNull] AsyncDataProvider dataProvider,
            [NotNull] IStringsProvider stringsProvider, [NotNull] IPhoneTasks phoneTasks)
        {
            if (systemDispatcher == null) throw new ArgumentNullException("systemDispatcher");
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            if (phoneTasks == null) throw new ArgumentNullException("phoneTasks");
            _systemDispatcher = systemDispatcher;
            _statistics = statistics;
            _dataProvider = dataProvider;
            _stringsProvider = stringsProvider;
            _phoneTasks = phoneTasks;
        }

        public BaseViewModel Create(int id, DetailsType detailsType)
        {
            return new UserActivityViewModel(_systemDispatcher, _statistics, _dataProvider, _stringsProvider,
                _phoneTasks, id, detailsType);
        }
    }
}