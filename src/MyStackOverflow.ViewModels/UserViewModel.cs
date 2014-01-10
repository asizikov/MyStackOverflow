using System;
using JetBrains.Annotations;
using MyStackOverflow.Data.Utils;
using MyStackOverflow.Model;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private const string URL_GRAVATAR = "http://www.gravatar.com/avatar/{0}?s=128&d=identicon&r=PG";
        private readonly User _model;
        private readonly IStringsProvider _stringsProvider;
        private string _userPic;

        public UserViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] User model,
            [NotNull] IStringsProvider stringsProvider)
            : base(dispatcher)
        {
            if (model == null) throw new ArgumentNullException("model");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            _model = model;
            _stringsProvider = stringsProvider;
            UserPic = _model.EmailHash;
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

        public string AcceptRate
        {
            get { return string.Format("{0}%", _model.AcceptRate); }
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
    }
}