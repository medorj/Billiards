using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billiards.API.Models
{
    public class GameUserViewModel
    {
        public int GameUserId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int DefensiveShots { get; set; }
        public int Timeouts { get; set; }
    }

    public static class GameUserMapper
    {
        public static GameUserViewModel ToViewModel(this GameUser gameUser)
        {
            return new GameUserViewModel
            {
                GameUserId = gameUser.GameUserId,
                GameId = gameUser.GameId,
                UserId = gameUser.UserId,
                UserName = gameUser.User.FirstName + " " + gameUser.User.LastName,
                DefensiveShots = gameUser.DefensiveShots,
                Timeouts = gameUser.Timeouts
            };
        }

        public static IEnumerable<GameUserViewModel> ToViewModel(this IEnumerable<GameUser> gameUsers)
        {
            return gameUsers.Select(g => g.ToViewModel());
        }
    }
}