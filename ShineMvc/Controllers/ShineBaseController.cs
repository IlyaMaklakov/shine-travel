#region Usings

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using Shine.Framework.Json;

using ShineMvc.Models.Users;

#endregion

namespace ShineMvc.Controllers
{
    public class ShineBaseController : Controller
    {
        protected static User CurrentUser { get; set; }
    }
}