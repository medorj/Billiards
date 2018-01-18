using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billiards.API.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Handicap { get; set; }
    }

    public static class UserMapper
    {
        public static UserViewModel ToViewModel(this User user)
        {
            return new UserViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Handicap = user.Handicap
            };
        }

        public static IEnumerable<UserViewModel> ToViewModel(this IEnumerable<User> users)
        {
            return users.Select(u => u.ToViewModel());
        }
    }
}