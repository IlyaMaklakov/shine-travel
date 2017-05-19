using Newtonsoft.Json;

namespace AmoCrm.Client.Accounts
{
    public class AccountResponse
    {
        [JsonProperty("account")]
        public Account Account { get; set; }
    }
}