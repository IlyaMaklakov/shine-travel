#region Usings

using AmoCrm.Client.Http;

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Contacts
{
    public class ContactsResponse : BaseResponse
    {
        [JsonProperty("contacts")]
        public Contact[] Contacts { get; set; }
    }
}