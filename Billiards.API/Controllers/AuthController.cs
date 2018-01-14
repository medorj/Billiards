using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Billiards.API.Models;

namespace Billiards.API.Controllers
{
    public class AuthController : ApiController
    {
        private BilliardsModel _db;
        public AuthController()
        {
            _db = new BilliardsModel();
        }
        [HttpPost]
        public IHttpActionResult Login(Login login)
        {
            try
            {
                Login efLogin = _db.Logins.FirstOrDefault(l => l.UserName == login.UserName);
                if (efLogin == null)
                    return BadRequest("Unable to find the user.");
                if (login.Password != efLogin.Password)
                    return BadRequest("You have entered invalid credentials.");
                return Ok(efLogin);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult GetLogin(Login login)
        {
            try
            {
                Login efLogin = _db.Logins.FirstOrDefault(l => l.LoginId == login.LoginId);
                if (efLogin == null)
                    return BadRequest("Unable to find login.");
                return Ok(new Login
                {
                    LoginId = efLogin.LoginId,
                    FirstName = efLogin.FirstName,
                    LastName = efLogin.LastName,
                    UserName = efLogin.UserName,
                    Password = efLogin.Password
                });
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Register(Login login)
        {
            try
            {
                Login existingLogin = _db.Logins.FirstOrDefault(l => l.UserName == login.UserName);
                if (existingLogin != null)
                    return BadRequest("User Name already exists.");
                Login efLogin = new Models.Login
                {
                    FirstName = login.FirstName,
                    LastName = login.LastName,
                    UserName = login.UserName,
                    Password = login.Password
                };
                _db.Logins.Add(efLogin);
                _db.SaveChanges();
                return Ok(efLogin);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult EditRegistration(Login login)
        {
            try
            {
                Login existingLogin = _db.Logins.FirstOrDefault(l => l.LoginId == login.LoginId);
                if (existingLogin == null)
                    return BadRequest("Unable to find user.");
                existingLogin.UserName = login.UserName;
                existingLogin.Password = login.Password;
                existingLogin.FirstName = login.FirstName;
                existingLogin.LastName = login.LastName;
                _db.SaveChanges();
                return Ok(existingLogin);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
