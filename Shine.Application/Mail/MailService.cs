#region Usings

using System;
using System.Net;
using System.Net.Mail;

#endregion

namespace Shine.Application.Mail
{
    public static class MailService
    {
        public static bool SendContactUsMessage(string email, string name, string message)
        {
            var msg = string.Format(
                "Name: {0}{1}Email: {2}{1}Message: {3}{1}",
                name,
                Environment.NewLine,
                email,
                message);

            return SendEmail("Форма обратной связи", msg);
        }

        private static bool SendEmail(string title, string message)
        {
            try
            {
                var smtpClient =
                    new SmtpClient("smtp.yandex.ru", 25)
                        {
                            Credentials = new NetworkCredential(
                                "shinecitysv@yandex.ru",
                                "abhvf456"),
                            EnableSsl = true
                        };
                smtpClient.Send("shinecitysv@yandex.ru", "60@shine.city", title, message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }

        public static object SendBookingMessage(string email, string name, string phone)
        {
            var message = string.Format(
                "Name: {0}{1}Email: {2}{1}Phone: {3}{1}",
                name,
                Environment.NewLine,
                email,
                phone);

            return SendEmail("Бронирование", message);
        }
    }
}