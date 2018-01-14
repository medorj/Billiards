using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billiards.API.Models
{
    public class MatchViewModel
    {
        public int MatchId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int User1Id { get; set; }
        public string User1Name { get; set; }
        public int User1Wins { get; set; }
        public int User2Id { get; set; }
        public string User2Name { get; set; }
        public int User2Wins { get; set; }
        public List<GameViewModel> Games { get; set; }
        public int GameCount { get; set; }
    }

    public static class MatchMapper
    {
        public static MatchViewModel ToViewModel(this Match match, bool loadGames = false)
        {
            var vm = new MatchViewModel
            {
                MatchId = match.MatchId,
                Date = match.Date,
                User1Id = match.User1Id,
                User2Id = match.User2Id
            };
            vm.User1Name = match.User != null ? match.User.FirstName + " " + match.User.LastName : string.Empty;
            vm.User2Name = match.User1 != null ? match.User1.FirstName + " " + match.User1.LastName : string.Empty;
            vm.Title = vm.User1Name + " vs. " + vm.User2Name;
            vm.User1Wins = match.Games.Count(g => g.IsActive && g.WinnerUserId == vm.User1Id);
            vm.User2Wins = match.Games.Count(g => g.IsActive && g.WinnerUserId == vm.User2Id);
            vm.GameCount = match.Games.Count(g => g.IsActive);
            if (loadGames == true)
            {
                vm.Games = match.Games.Where(g => g.IsActive).OrderByDescending(g => g.Number).ToViewModel().ToList();
            }

            return vm;
        }

        public static IEnumerable<MatchViewModel> ToViewModel(this IEnumerable<Match> matches, bool loadGames = false)
        {
            return matches.Select(m => m.ToViewModel(loadGames));
        }
    }
}