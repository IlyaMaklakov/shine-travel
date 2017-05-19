#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client
{
    public class BaseAmoEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("last_modified")]
        public string LastModifiedTime { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}