#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client
{
    public class AuthRequestModel
    {
        [JsonProperty("USER_HASH")]
        public string Hash { get; set; }

        [JsonProperty("USER_LOGIN")]
        public string UserLogin { get; set; }
    }
}