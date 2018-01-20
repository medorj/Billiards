using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billiards.API.Models
{
    public class LoginViewModel
    {
        public int LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? UserId { get; set; }
    }

    public static class LoginMapper
    {
        public static LoginViewModel ToViewModel(this Login login)
        {
            return new LoginViewModel
            {
                LoginId = login.LoginId,
                FirstName = login.FirstName,
                LastName = login.LastName,
                UserName = login.UserName,
                Password = login.Password,
                UserId = login.UserId
            };
        }

        public static IEnumerable<LoginViewModel> ToViewModel(this IEnumerable<Login> logins)
        {
            return logins.Select(l => l.ToViewModel());
        }
    }
}