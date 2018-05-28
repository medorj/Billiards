using System;
using System.Linq;
using System.Web.Http;
using Billiards.API.Models;

namespace Billiards.API.Controllers
{
    public class BilliardsController : ApiController
    {
        private BilliardsModel _db;

        public BilliardsController()
        {
            _db = new BilliardsModel();
        }
        
        #region Matches

        [HttpGet]
        public IHttpActionResult GetMatches()
        {
            return Ok(_db.Matches.Where(m => m.IsActive).OrderByDescending(m => m.Date).ToViewModel(null, true));
        }

        [HttpGet]
        public IHttpActionResult GetMatch(int id, string orderBy)
        {
            var match = _db.Matches.FirstOrDefault(m => m.MatchId == id);
            if (match != null)
            {
                var handicapMatrix = _db.HandicapMatrices.FirstOrDefault(h => h.Player1 == match.User.Handicap && h.Player2 == match.User1.Handicap);
                return Ok(match.ToViewModel(handicapMatrix, true, orderBy));
            }
            return BadRequest("Match not found");
        }

        [HttpPost]
        public IHttpActionResult SaveMatch(MatchViewModel match)
        {
            try
            {
                var efMatch = new Match
                {
                    Date = match.Date,
                    User1Id = match.User1Id,
                    User1Points = 0,
                    User2Id = match.User2Id,
                    User2Points = 0,
                    IsActive = true,
                    MatchType = match.MatchTypeId
                };
                var matrix = GetHandicap(match.User1Id, match.User2Id);
                int minWins = matrix.Player1Wins < matrix.Player2Wins ? matrix.Player1Wins : matrix.Player2Wins;
                //AddNewGame(ref efMatch, 1);
                if (match.MatchTypeId == 2)
                {
                    SeedMatch(ref efMatch, matrix);
                }
                _db.Matches.Add(efMatch);
                _db.SaveChanges();
                return Ok(efMatch.ToViewModel(null, true));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteMatch(MatchViewModel match)
        {
            try
            {
                var efMatch = _db.Matches.FirstOrDefault(g => g.MatchId == match.MatchId);
                if (efMatch == null)
                    return BadRequest("Unable to find match.");
                efMatch.IsActive = false;
                foreach (Game game in efMatch.Games)
                {
                    game.IsActive = false;
                }
                _db.SaveChanges();
                return Ok(efMatch.ToViewModel());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Games

        [HttpGet]
        public IHttpActionResult GetGame(int id)
        {
            try
            {
                var efGame = _db.Games.FirstOrDefault(g => g.GameId == id);
                if (efGame == null)
                    return BadRequest("Unable to find game.");
                _db.Entry(efGame).Reference(g => g.Match).Load();
                return Ok(efGame.ToViewModel());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult AddGame(GameViewModel game)
        {
            try
            {
                var efMatch = _db.Matches.FirstOrDefault(m => m.MatchId == game.MatchId);
                if (efMatch == null)
                    return BadRequest("Unable to find match.");

                var efGame = AddNewGame(ref efMatch, GetNextGameNumber(efMatch));
                _db.SaveChanges();
                return Ok(efGame.ToViewModel());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult SaveGame(GameViewModel game)
        {
            try
            {
                bool shouldProcessWin = false;
                var efGame = _db.Games.FirstOrDefault(g => g.GameId == game.GameId);
                if (efGame == null)
                    return BadRequest("Unable to find game.");

                // if winner is identified for the first time, process afterwards
                if (efGame.WinnerUserId == null && game.WinnerUserId > 0)
                    shouldProcessWin = true;

                // set game properties
                efGame.WinnerUserId = game.WinnerUserId;
                efGame.IsActive = true;
                efGame.Innings = game.Innings;
                efGame.WinType = game.WinType;
                efGame.Badge = game.Badge;
                if (game.GameUsers.Any())
                {
                    foreach (var user in game.GameUsers)
                    {
                        var currentUser = efGame.GameUsers.FirstOrDefault(gu => gu.GameUserId == user.GameUserId);
                        currentUser.DefensiveShots = user.DefensiveShots;
                        currentUser.Timeouts = user.Timeouts;
                    }
                }

                // process win and automatically add games for APA matches
                if (shouldProcessWin)
                    ProcessWin(ref efGame);

                _db.SaveChanges();
                return Ok(efGame.ToViewModel());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteGame(GameViewModel game)
        {
            try
            {
                var efGame = _db.Games.FirstOrDefault(g => g.GameId == game.GameId);
                if (efGame == null)
                    return BadRequest("Unable to find match.");
                efGame.IsActive = false;
                _db.SaveChanges();
                return Ok(efGame.ToViewModel());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Statistics

        [HttpGet]
        public IHttpActionResult GetHeadToHead(int player1, int player2)
        {
            var user1 = _db.Users.FirstOrDefault(u => u.UserId == player1);
            var user2 = _db.Users.FirstOrDefault(u => u.UserId == player2);

            var matches = _db.Matches
                .Where(m => m.IsActive &&
                (m.User1Id == player1 || m.User1Id == player2) 
                && (m.User2Id == player1 || m.User2Id == player2));

            int player1Wins = 0;
            int player2Wins = 0;

            foreach(var match in matches)
            {
                player1Wins += match.Games.Count(g => g.IsActive && g.WinnerUserId == player1);
                player2Wins += match.Games.Count(g => g.IsActive && g.WinnerUserId == player2);
            }

            var data = new
            {
                Player1Name = user1.FirstName + " " + user1.LastName,
                Player2Name = user2.FirstName + " " + user2.LastName,
                Player1Wins = player1Wins,
                Player2Wins = player2Wins,
                TotalGames = player1Wins + player2Wins,
                Player1Percentage = (player1Wins + player2Wins) > 0 ? (double)player1Wins / (player1Wins + player2Wins) : 0,
                Player2Percentage = (player1Wins + player2Wins) > 0 ? (double)player2Wins / (player1Wins + player2Wins) : 0
            };

            return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult GetHeadToHeadGames(int player1, int player2)
        {
            try
            {
                var games = (from g in _db.Games
                             join m in _db.Matches on g.MatchId equals m.MatchId
                             where ((m.User1Id == player1 && m.User2Id == player2) || (m.User1Id == player2 && m.User2Id == player1)) && g.IsActive
                             select g)
                             .Distinct()
                             .OrderByDescending(g => g.Match.Date)
                             .ThenByDescending(g => g.Number)
                             .ToViewModel();
                return Ok(games);

            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Add a new game to a match
        /// </summary>
        /// <param name="match"></param>
        /// <param name="gameNumber"></param>
        /// <returns></returns>
        private Game AddNewGame(ref Match match, int gameNumber)
        {
            Game efGame = new Game
            {
                Number = gameNumber,
                IsActive = true,
                Innings = 0
            };
            efGame.GameUsers.Add(new GameUser
            {
                UserId = match.User1Id,
                IsActive = true,
                Timeouts = 0,
                DefensiveShots = 0,
            });
            efGame.GameUsers.Add(new GameUser
            {
                UserId = match.User2Id,
                IsActive = true,
                Timeouts = 0,
                DefensiveShots = 0,
            });
            match.Games.Add(efGame);
            return efGame;
        }

        private HandicapMatrix GetHandicap(int user1Id, int user2Id)
        {
            var user1 = _db.Users.FirstOrDefault(u => u.UserId == user1Id);
            var user2 = _db.Users.FirstOrDefault(u => u.UserId == user2Id);
            var matrix = _db.HandicapMatrices.FirstOrDefault(h => h.Player1 == user1.Handicap && h.Player2 == user2.Handicap);
            return matrix;
        }

        private int GetNextGameNumber(Match match)
        {
            int maxGameNumber = 0;
            if (match.Games.Any())
            {
                maxGameNumber = match.Games.Where(g => g.IsActive).Max(g => g.Number);
            }
            return maxGameNumber + 1;
        }

        /// <summary>
        /// Process a game win
        /// </summary>
        /// <param name="game"></param>
        private void ProcessWin(ref Game game)
        {
            int matchId = game.MatchId;

            // get the match
            var match = _db.Matches.FirstOrDefault(m => m.MatchId == matchId);

            // don't process for casual matches
            if (match.MatchType == 1) return;
            
            // get the handicap
            var matrix = GetHandicap(match.User1Id, match.User2Id);

            // wins obtained
            int user1Wins = match.Games.Count(g => g.WinnerUserId == match.User1Id);
            int user2Wins = match.Games.Count(g => g.WinnerUserId == match.User2Id);

            // wins remaining
            int user1WinsNeeded = matrix.Player1Wins - user1Wins;
            int user2WinsNeeded = matrix.Player2Wins - user2Wins;

            // see if we should add more games - match isn't over
            if (user1WinsNeeded > 0 && user2WinsNeeded > 0)
            {
                int totalGamesPlayed = user1Wins + user2Wins;
                int totalGames = match.Games.Count();
                int minGamesRemaining = user1WinsNeeded < user2WinsNeeded ? user1WinsNeeded : user2WinsNeeded;

                int minGames = totalGamesPlayed + minGamesRemaining;
                if(minGames > totalGames)
                    AddNewGame(ref match, GetNextGameNumber(match));
            }
            // process match completion
            else if (user1WinsNeeded == 0)
            {
                // shutout
                if (user2WinsNeeded == matrix.Player2Wins)
                {
                    match.User1Points = 3;
                    match.User2Points = 0;
                }
                else
                {
                    match.User1Points = 2;
                    match.User2Points = user2WinsNeeded == 1 ? 1 : 0;
                }
            }
            else if (user2WinsNeeded == 0)
            {
                // shutout
                if (user1WinsNeeded == matrix.Player1Wins)
                {
                    match.User2Points = 3;
                    match.User1Points = 0;
                }
                else
                {
                    match.User2Points = 2;
                    match.User1Points = user1WinsNeeded == 1 ? 1 : 0;
                }
            }
        }

        private void SeedMatch(ref Match match, HandicapMatrix handicap)
        {
            int minGames = handicap.Player1Wins < handicap.Player2Wins ? handicap.Player1Wins : handicap.Player2Wins;
            for (int i = 1; i <= minGames; i++)
            {
                AddNewGame(ref match, i);
            }
        }

        #endregion
    }
}