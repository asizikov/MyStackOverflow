using System;
using JetBrains.Annotations;
using MyStackOverflow.Data.Utils;
using MyStackOverflow.Model;
using MyStackOverflow.ViewModels.Commands;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private const string URL_GRAVATAR = "http://www.gravatar.com/avatar/{0}?s=128&d=identicon&r=PG";
        private readonly User _model;
        private readonly IStringsProvider _stringsProvider;
        private readonly INavigationService _navigation;
        private string _userPic;

        public UserViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] User model,
            [NotNull] IStringsProvider stringsProvider, [NotNull] INavigationService navigation)
            : base(dispatcher)
        {
            if (model == null) throw new ArgumentNullException("model");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            if (navigation == null) throw new ArgumentNullException("navigation");
            _model = model;
            _stringsProvider = stringsProvider;
            _navigation = navigation;
            UserPic = _model.EmailHash;
            InitCommand();
        }

        private void InitCommand()
        {
            ShowQuestionsCommand = new RelayCommand(NavigateToQuestions);
            ShowAnswersCommand = new RelayCommand(NavigateToAnswers);
        }

        private void NavigateToAnswers(object obj)
        {
            _navigation.GoToPage(Pages.UserActivityPage, new[]
            {
                new NavigationParameter
                {
                    Parameter = NavigationParameterName.Id,
                    Value = _model.UserId.ToString()
                },
                new NavigationParameter
                {
                    Parameter = NavigationParameterName.Answers,
                    Value = true.ToString()
                }
            });
        }

        private void NavigateToQuestions(object obj)
        {
            _navigation.GoToPage(Pages.UserActivityPage, new[]
            {
                new NavigationParameter
                {
                    Parameter = NavigationParameterName.Id,
                    Value = _model.UserId.ToString()
                },
                new NavigationParameter
                {
                    Parameter = NavigationParameterName.Questions,
                    Value = true.ToString()
                }
            });
        }

        public string DisplayName
        {
            get { return _model.DisplayName; }
        }

        public int Reputation
        {
            get { return _model.Reputation; }
        }

        public string Location
        {
            get { return _model.Location; }
        }

        public int Age
        {
            get { return _model.Age; }
        }

        public string QuestionCount
        {
            get { return string.Format(_stringsProvider.Questions, _model.QuestionCount); }
        }

        public string AnswerCount
        {
            get { return string.Format(_stringsProvider.Answers, _model.AnswerCount); }
        }

        public int AcceptRate
        {
            get { return _model.AcceptRate; }
        }

        public string LastAccessDate
        {
            get { return DateTimeUtils.DateTimeFromUnixTimestampSeconds(_model.LastAccessDate).ToReadableAgo(); }
        }

        public BadgeCounts BadgeCounts
        {
            get { return _model.BadgeCounts; }
        }

        public string MemberFor
        {
            get { return DateTimeUtils.DateTimeFromUnixTimestampSeconds(_model.CreationDate).ToReadableFor(); }
        }

        public string UserPic
        {
            get
            {
                _userPic = string.IsNullOrWhiteSpace(_userPic)
                    ? string.Empty
                    : string.Format(URL_GRAVATAR, _userPic);
                return _userPic;
            }
            private set
            {
                if (value == _userPic) return;
                _userPic = value;
                OnPropertyChanged("UserPic");
            }
        }

        public RelayCommand ShowQuestionsCommand { get; private set; }
        public RelayCommand ShowAnswersCommand { get; private set; }
    }
}