using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        #region Users
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            return Ok(_db.Users.Where(u => u.IsActive).ToViewModel());
        }

        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return BadRequest("Unable to find user.");
            }
            return Ok(user.ToViewModel());
        }

        [HttpPost]
        public IHttpActionResult SaveUser(UserViewModel user)
        {
            try
            {
                var efUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = true
                };
                _db.Users.Add(efUser);
                _db.SaveChanges();
                return Ok(efUser.ToViewModel());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteUser(UserViewModel user)
        {
            try
            {
                var efUser = _db.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (efUser == null)
                    return BadRequest("Unable to find user.");
                efUser.IsActive = false;
                _db.SaveChanges();
                return Ok(efUser.ToViewModel());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Matches

        [HttpGet]
        public IHttpActionResult GetMatches()
        {
            return Ok(_db.Matches.Where(m => m.IsActive).OrderByDescending(m => m.Date).ToViewModel(true));
        }

        [HttpGet]
        public IHttpActionResult GetMatch(int id)
        {
            var match = _db.Matches.FirstOrDefault(m => m.MatchId == id);
            if (match != null)
            {
                return Ok(match.ToViewModel(true));
            }
            return BadRequest("Match not found");
        }

        [HttpPost]
        public IHttpActionResult SaveMatch(MatchViewModel match)
        {
            try
            {
                var efMatch = new Match();
                efMatch.Date = match.Date;
                efMatch.User1Id = match.User1Id;
                efMatch.User2Id = match.User2Id;
                efMatch.IsActive = true;
                _db.Matches.Add(efMatch);
                _db.SaveChanges();
                return Ok(efMatch.ToViewModel(true));
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
                foreach(Game game in efMatch.Games)
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

                int maxGameNumber = 0;
                if (efMatch.Games.Any())
                {
                    maxGameNumber = efMatch.Games.Where(g => g.IsActive).Max(g => g.Number);
                }
                Game efGame = new Game
                {
                    Number = maxGameNumber + 1,
                    IsActive = true,
                    Innings = 0
                };
                efGame.GameUsers.Add(new GameUser
                {
                    UserId = efMatch.User1Id,
                    IsActive = true,
                    Timeouts = 0,
                    DefensiveShots = 0,
                });
                efGame.GameUsers.Add(new GameUser
                {
                    UserId = efMatch.User2Id,
                    IsActive = true,
                    Timeouts = 0,
                    DefensiveShots = 0,
                });
                efMatch.Games.Add(efGame);
                _db.SaveChanges();
                return Ok(efMatch.ToViewModel());
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult SaveGame(GameViewModel game)
        {
            try
            {
                var efGame = _db.Games.FirstOrDefault(g => g.GameId == game.GameId);
                if (efGame == null)
                    return BadRequest("Unable to find game.");
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
                _db.SaveChanges();
                return Ok(efGame.ToViewModel());
            }
            catch(Exception ex)
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
    }
}
