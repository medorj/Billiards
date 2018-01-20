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
        public int User1WinsRemaining { get; set; }
        public int User1Points { get; set; }
        public int User2Id { get; set; }
        public string User2Name { get; set; }
        public int User2Wins { get; set; }
        public int User2Points { get; set; }
        public int User2WinsRemaining { get; set; }
        public int MatchTypeId { get; set; }
        public List<GameViewModel> Games { get; set; }
        public int GameCount { get; set; }
        public string MatchType
        {
            get
            {
                switch(MatchTypeId)
                {
                    case 2:
                        return "APA";
                    default:
                        return "Casual";
                }
            }
        }
        public decimal User1WinPercentage { get; set; }
        public decimal User2WinPercentage { get; set; }
    }

    public static class MatchMapper
    {
        public static MatchViewModel ToViewModel(this Match match, HandicapMatrix matrix = null, bool loadGames = false, string gameSort = "DESC")
        {
            var vm = new MatchViewModel
            {
                MatchId = match.MatchId,
                Date = match.Date,
                User1Id = match.User1Id,
                User1Points = match.User1Points,
                User2Id = match.User2Id,
                User2Points = match.User2Points,
                MatchTypeId = match.MatchType
            };
            
            vm.User1Wins = match.Games.Count(g => g.IsActive && g.WinnerUserId == vm.User1Id);
            vm.User2Wins = match.Games.Count(g => g.IsActive && g.WinnerUserId == vm.User2Id);
            vm.GameCount = match.Games.Count(g => g.IsActive);

            if (match.User != null)
            {
                vm.User1Name = match.User.FirstName + " " + match.User.LastName;
                vm.User1WinPercentage = matrix != null ? (vm.User1Wins / Convert.ToDecimal(matrix.Player1Wins) * 100) : 0;
                vm.User1WinsRemaining = matrix != null ? matrix.Player1Wins - vm.User1Wins : 0;
            }
            else
            {
                vm.User1Name = string.Empty;
            }
            if (match.User1 != null)
            {
                vm.User2Name = match.User1.FirstName + " " + match.User1.LastName;
                vm.User2WinPercentage = matrix != null ? (vm.User2Wins / Convert.ToDecimal(matrix.Player2Wins) * 100) : 0;
                vm.User2WinsRemaining = matrix != null ? matrix.Player2Wins - vm.User2Wins : 0;
            }
            else
            {
                vm.User2Name = string.Empty;
            }
            vm.Title = vm.User1Name + " vs. " + vm.User2Name;

            if (loadGames == true)
            {
                if(gameSort == "ASC")
                    vm.Games = match.Games.Where(g => g.IsActive).OrderBy(g => g.Number).ToViewModel().ToList();
                else
                    vm.Games = match.Games.Where(g => g.IsActive).OrderByDescending(g => g.Number).ToViewModel().ToList();
            }

            return vm;
        }

        public static IEnumerable<MatchViewModel> ToViewModel(this IEnumerable<Match> matches, HandicapMatrix matrix, bool loadGames = false)
        {
            return matches.Select(m => m.ToViewModel(matrix, loadGames));
        }
    }
}