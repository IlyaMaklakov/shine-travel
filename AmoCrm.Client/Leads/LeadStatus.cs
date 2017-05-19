#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Leads
{
    public class LeadStatus
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("editable")]
        public string Editable { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}