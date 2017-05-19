using AmoCrm.Client.Crud;

using Newtonsoft.Json;

namespace AmoCrm.Client.Leads
{
    public class SetLeadsRequest
    {
        [JsonProperty("leads")]
        public AddOrUpdateWrapper<Lead> Leads { get; set; }
    }
}