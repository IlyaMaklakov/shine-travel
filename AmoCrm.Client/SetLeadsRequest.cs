using Newtonsoft.Json;

namespace AmoCrm.Client
{
    public class SetLeadsRequest
    {
        [JsonProperty("leads")]
        public SetLeadsViewModel Leads { get; set; }
    }
}