using System;

namespace DKRDesktopUI.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public DateTime CreatedDate { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Id { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        public void ResetUserModel()
        {
            CreatedDate = DateTime.MinValue;
            EmailAddress = string.Empty;
            FirstName = string.Empty;
            Id = string.Empty;
            LastName = string.Empty;
            Token = string.Empty;
        }
    }
}