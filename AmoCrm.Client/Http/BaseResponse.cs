#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Http
{
    public class BaseResponse
    {
        [JsonProperty("server_time")]
        public string ServerTime { get; set; }
    }
}