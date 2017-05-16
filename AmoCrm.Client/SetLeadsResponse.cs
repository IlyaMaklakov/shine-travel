#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client
{
    public class SetLeadsResponse
    {
        public LeadViewModel[] Leads { get; set; }

        [JsonProperty("server_time")]
        public string ServerTime { get; set; }
    }
}