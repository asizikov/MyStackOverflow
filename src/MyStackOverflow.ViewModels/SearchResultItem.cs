using MyStackOverflow.Model;

namespace MyStackOverflow.ViewModels
{
    public class SearchResultItem
    {
        private string _userPic;
        private const string URL_GRAVATAR = "http://www.gravatar.com/avatar/{0}?s=37&d=identicon&r=PG";
        public SearchResultItem(User user)
        {
            Name = user.DisplayName;
            Id = user.UserId;
            UserPic = user.EmailHash;
            Reputation = user.Reputation;
        }

        public string Name { get; private set; }
        public int Id { get; private set; }
        public int Reputation { get; private set; }

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
            }
        }

    }
}