using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billiards.API.Models
{
    public class GameViewModel
    {
        public int GameId { get; set; }
        public int MatchId { get; set; }
        public int Number { get; set; }
        public int? WinnerUserId { get; set; }
        public string WinnerName { get; set; }
        public int Innings { get; set; }
        public int DefensiveShots { get; set; }
        public List<UserViewModel> Participants { get; set; }
        public MatchViewModel Match { get; set; }
        public List<GameUserViewModel> GameUsers { get; set; }
    }

    public static class GameMapper
    {
        public static GameViewModel ToViewModel(this Game game)
        {
            var vm = new GameViewModel
            {
                GameId = game.GameId,
                MatchId = game.MatchId,
                Number = game.Number,
                Innings = game.Innings,
                WinnerUserId = game.WinnerUserId
            };
            vm.Match = game.Match.ToViewModel(false);
            vm.Participants = new List<UserViewModel>();
            if(game.Match != null && game.Match.User != null && game.Match.User1 != null)
            {
                vm.Participants.Add(game.Match.User.ToViewModel());
                vm.Participants.Add(game.Match.User1.ToViewModel());
            }
            vm.WinnerName = game.User != null ? game.User.FirstName + " " + game.User.LastName : string.Empty;
            vm.GameUsers = game.GameUsers.ToViewModel().ToList();

            return vm;
        }

        public static IEnumerable<GameViewModel> ToViewModel(this IEnumerable<Game> games)
        {
            return games.Select(g => g.ToViewModel());
        }
    }
}