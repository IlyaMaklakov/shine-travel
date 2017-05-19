#region Usings

using AmoCrm.Client.Accounts;
using AmoCrm.Client.Http;
using AmoCrm.Client.Users;

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Auth
{
    public class AuthResponse : BaseResponse
    {
        [JsonProperty("accounts")]
        public Account[] Accounts { get; set; }

        [JsonProperty("auth")]
        public bool IsAuthenticated { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}