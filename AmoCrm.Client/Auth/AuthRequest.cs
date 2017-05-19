#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Auth
{
    public class AuthRequest
    {
        [JsonProperty("USER_HASH")]
        public string Hash { get; set; }

        [JsonProperty("USER_LOGIN")]
        public string UserLogin { get; set; }
    }
}