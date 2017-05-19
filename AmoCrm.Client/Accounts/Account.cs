using AmoCrm.Client.Leads;
using AmoCrm.Client.Users;

using Newtonsoft.Json;

namespace AmoCrm.Client.Accounts
{
    public class Account : BaseAmoEntity
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("subdomain")]
        public string SubDomain { get; set; }

        [JsonProperty("users")]
        public User[] Users { get; set; }

        [JsonProperty("leads_statuses")]
        public LeadStatus[] LeadStatuses { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }
    }
}