#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Contacts
{
    public class Contact : BaseAmoEntity
    {

        [JsonProperty("linked_leads_id")]
        public string[] LeadsIds { get; set; }

    }
}