#region Usings

using System;
using System.Net.Mail;
using System.Web.Mvc;

using Shine.Application.Mail;

using ShineMvc.Models;

#endregion

namespace ShineMvc.Controllers
{
    public class ContactUsController : ShineBaseController
    {
        [HttpPost]
        public ActionResult SendContactUsMessage(ContactUsModel model)
        {
            var result = MailService.SendContactUsMessage(model.Email, model.Name, model.Message);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Booking(ContactUsModel model)
        {
            var result = MailService.SendBookingMessage(model.Email, model.Name, model.Phone);
            return RedirectToAction("ThankYou");
        }

        public ActionResult ThankYou()
        {
            return View();
        }
    }
}