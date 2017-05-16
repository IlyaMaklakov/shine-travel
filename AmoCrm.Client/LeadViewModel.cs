#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client
{
    public class LeadViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("visitor_uid")]
        public string VisitorId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("last_modified")]
        public string LastModifiedTime { get; set; }

        [JsonProperty("status_id")]
        public string StatusId { get; set; }
    }
}