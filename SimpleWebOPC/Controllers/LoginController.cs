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
            using (OpcUaDatabseEntities1 db = new OpcUaDatabseEntities1())
            {
                var userDetail = db.OpcUaUsers.Where(x => x.Login == opcUaUser.Login && x.Password == opcUaUser.Password).FirstOrDefault();

               
                if (userDetail == null)
                {
                    opcUaUser.TestRow = "Wrong username or password";
                    return View("Index", opcUaUser);
                }
                else
                {
                    Session["UserID"] = userDetail.Login;
                    Session.Timeout = 1;
                    return RedirectToAction("Index", "Home");
                }
            }
         
        }
    }
}