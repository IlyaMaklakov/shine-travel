using ShineMvc.Models.Users;
using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace ShineMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user = GetCurrentUserInfo();
            if (user == null)
            {
                SetUserInfoToCookie();
            }

            return View();
        }

        private void SetUserInfoToCookie()
        {
            var user = new User
            {
                FirstOpenDate = DateTime.Now
            };

            var cookie = new HttpCookie("Shine-travel");
            cookie["openDate"] = user.FirstOpenDate.Ticks.ToString(); 
            Response.Cookies.Add(cookie);
        }

        private User GetCurrentUserInfo()
        {
            User user = null;
            var cookieReq = Request.Cookies["Shine-travel"];     
            if (cookieReq != null)
            {
                var openDate = cookieReq["openDate"];
                if (!string.IsNullOrWhiteSpace(openDate))
                {
                    user = new User
                    {
                        FirstOpenDate = new DateTime(Convert.ToInt64(openDate))
                    };
                }
            }

            return user;
        }
    }
}