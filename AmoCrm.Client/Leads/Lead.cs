#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Leads
{
    public class Lead : BaseAmoEntity
    {
        [JsonProperty("visitor_uid")]
        public string VisitorId { get; set; }

        [JsonProperty("status_id")]
        public string StatusId { get; set; }
    }
}