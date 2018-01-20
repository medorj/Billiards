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
        public int GamesWon { get; set; }
        public int GamesPlayed { get; set; }
        public decimal WinPercentage
        {
            get
            {
                if (GamesPlayed == 0)
                    return 0;
                return GamesWon / (decimal)GamesPlayed;
            }
        }
    }

    public static class UserMapper
    {
        public static UserViewModel ToViewModel(this User user, bool loadStatistics = false)
        {
            var vm = new UserViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Handicap = user.Handicap
            };
            if(loadStatistics)
            {
                vm.GamesWon = user.Games.Count(g => g.IsActive);
                vm.GamesPlayed = 0;
                foreach (var match in user.Matches)
                {
                    vm.GamesPlayed += match.Games.Count(g => g.IsActive);
                }
                foreach (var match in user.Matches1)
                {
                    vm.GamesPlayed += match.Games.Count(g => g.IsActive);
                }
            }

            return vm;
        }

        public static IEnumerable<UserViewModel> ToViewModel(this IEnumerable<User> users, bool loadStatistics = false)
        {
            return users.Select(u => u.ToViewModel(loadStatistics));
        }
    }
}