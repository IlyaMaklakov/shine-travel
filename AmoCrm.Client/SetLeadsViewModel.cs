using Newtonsoft.Json;

namespace AmoCrm.Client
{
    public class SetLeadsViewModel
    {
        [JsonProperty("update")]
        public LeadViewModel[] Update { get; set; }
    }
}