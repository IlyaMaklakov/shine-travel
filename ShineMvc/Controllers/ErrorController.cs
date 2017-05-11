#region Usings

using System.Web.Mvc;

#endregion

namespace ShineMvc.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return this.View("NotFound");
        }

        public ActionResult ServerError()
        {
            return this.View("ServerError");
        }
    }
}