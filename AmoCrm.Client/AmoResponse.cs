using Newtonsoft.Json;

namespace AmoCrm.Client
{
    public class AmoResponse
    {
        public AmoAccount[] Accounts { get; set; }

        public bool Auth { get; set; }

        [JsonProperty("server_time")]
        public string ServerTime { get; set; }

        public AmoUser User { get; set; }
    }
}