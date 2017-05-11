#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

using Shine.Core;
using Shine.Core.User;
using Shine.Core.Video;

#endregion

namespace ShineMvc.Controllers
{
    public abstract class ShineBaseController : Controller
    {
        protected static User CurrentUser { get; private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            CurrentUser = this.GetCurrentUserInfo() ?? new User();
        }

        private User GetCurrentUserInfo()
        {
            User user = null;
            var cookieReq = this.Request.Cookies[ShineConsts.CookieId];
            var cookieValue = cookieReq?[ShineConsts.CurrentUserCookieId];
            if (!string.IsNullOrWhiteSpace(cookieValue))
            {
                var currentUserJson = HttpUtility.UrlDecode(cookieValue);

                if (!string.IsNullOrWhiteSpace(currentUserJson))
                {
                    var serializer = new JavaScriptSerializer();
                    user = serializer.Deserialize<User>(currentUserJson);
                }
            }

            return user;
        }

        public void UpdateUserInfoCookie()
        {
            var cookie =
                new HttpCookie(ShineConsts.CookieId)
                    {
                        [ShineConsts.CurrentUserCookieId] =
                        new JavaScriptSerializer().Serialize(CurrentUser),
                        Expires = DateTime.Now.AddYears(1)
                    };

            this.Response.Cookies.Add(cookie);
        }
    }
}