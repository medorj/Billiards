using Billiards.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        #endregion
    }
}
