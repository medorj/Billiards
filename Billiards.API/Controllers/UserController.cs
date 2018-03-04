using Billiards.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Billiards.API.Controllers
{
    public class UserController : ApiController
    {
        private BilliardsModel _db;

        public UserController()
        {
            _db = new BilliardsModel();
        }

        #region Users
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            return Ok(_db.Users
                .Where(u => u.IsActive)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToViewModel());
        }

        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return BadRequest("Unable to find user.");
            }
            return Ok(user.ToViewModel(true));
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
                    IsActive = true,
                    Handicap = 4
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

        [HttpGet]
        public IHttpActionResult GetStatistics(int userId)
        {
            return Ok(true);
        }

        [HttpGet]
        public IHttpActionResult GetUnlinkedAccounts()
        {
            try
            {
                var logins = _db.Logins.Where(l => l.UserId == null).ToViewModel();
                var users = _db.Users.Where(u => u.IsActive && !u.Logins.Any()).ToViewModel();
                var data = new
                {
                    logins = logins,
                    users = users
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult SyncAccounts(UnlinkedUserViewModel unlinkedUser)
        {
            try
            {
                var login = _db.Logins.Find(unlinkedUser.LoginId);
                login.UserId = unlinkedUser.UserId;
                _db.SaveChanges();
                return Ok(login.ToViewModel());
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        #endregion
    }
}
