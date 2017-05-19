using Newtonsoft.Json;

namespace AmoCrm.Client.Users
{
    public class User : BaseAmoEntity
    {
        [JsonProperty("language")]
        public string Language { get; set; }
    }
}