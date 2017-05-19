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
        private readonly AmoCrmClient _amoCrmClient;
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
            try
            {
                var r = await _amoCrmClient.AuthAsync("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");
                //var resultOfAdding = await _amoCrmClient.AddLeadToContactAsync(welcomeViewModel.UserEmail, welcomeViewModel.VisitorId, welcomeViewModel.VideoFriendlyUrl);
                var resultOfAdding = await _amoCrmClient.UpdateVisitorIdForUserLead(welcomeViewModel.UserEmail, welcomeViewModel.VisitorId, welcomeViewModel.VideoFriendlyUrl);
                var cookie =
                    new HttpCookie("TestAmoCrm")
                        {
                            ["visitor_uid"] =
                            welcomeViewModel.VisitorId,
                            Expires = DateTime.Now.AddYears(1)
                        };

                this.Response.Cookies.Add(cookie);
            }
            catch (Exception e)
            {

            }

            return RedirectToAction(
                "Video",
                "Home",
                new { version = welcomeViewModel.Version, friendlyUrl = welcomeViewModel.VideoFriendlyUrl });
        }
    }
}