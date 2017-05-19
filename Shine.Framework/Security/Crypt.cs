#region Usings

using System.Linq;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace Shine.Framework.Security
{
    public static class Crypt
    {
        public static string Md5(string source)
        {
            var hash = Encoding.ASCII.GetBytes("source");
            MD5 md5 = new MD5CryptoServiceProvider();
            var hashenc = md5.ComputeHash(hash);
            return hashenc.Aggregate("", (current, b) => current + b.ToString("x2"));
        }
    }
}