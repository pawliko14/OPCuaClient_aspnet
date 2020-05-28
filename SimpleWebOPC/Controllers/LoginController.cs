using SimpleWebOPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleWebOPC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(SimpleWebOPC.Models.OpcUaUser opcUaUser)
        {
            using (Models.LoginDatabeEntity db = new LoginDatabeEntity())
            {
                var userDetail = db.OpcUaUsers.Where(x => x.Login.Equals(opcUaUser.Login) && x.Password.Equals(opcUaUser.Password)).FirstOrDefault();

                if (userDetail == null)
                {
                    opcUaUser.LoginErrorMessage = "Wrong username or password";
                    return View("Index", opcUaUser);
                }
            }
            return View();
        }
    }
}