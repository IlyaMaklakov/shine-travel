#region Usings

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using AmoCrm.Client;

using Shine.Core;

using ShineMvc.Models;

#endregion

namespace ShineMvc.Controllers
{
    public class WelcomeController : Controller
    {
        private AmoCrmClient _amoCrmClient;
        public WelcomeController()
        {
            this._amoCrmClient = new AmoCrmClient();
        }

        [HttpGet]
        public ActionResult Index(string email, string version, string videoFriendlyUrl)
        {
            Trace.TraceError($"email: {email} videoFriendly: {videoFriendlyUrl}");            
            var welcomeViewModel =
                new WelcomeViewModel { Version = version, UserEmail = email, VideoFriendlyUrl = videoFriendlyUrl };
            return this.View(welcomeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(WelcomeViewModel welcomeViewModel)
        {
            var r = await _amoCrmClient.Auth("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");
            var u = await this._amoCrmClient.UpdateVisitorIdForUserLeads(
                        welcomeViewModel.UserEmail,
                        welcomeViewModel.VisitorId);


            var cookie =
                new HttpCookie("TestAmoCrm")
                {
                    ["visitor_uid"] =
                        welcomeViewModel.VisitorId,
                    Expires = DateTime.Now.AddYears(1)
                };

            this.Response.Cookies.Add(cookie);
            return RedirectToAction(
                "Video",
                "Home",
                new { version = welcomeViewModel.Version, friendlyUrl = welcomeViewModel.VideoFriendlyUrl });
        }
    }
}