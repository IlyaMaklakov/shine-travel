#region Usings

using AmoCrm.Client.Http;

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Leads
{
    public class SetLeadsResponse : BaseResponse
    {
        [JsonProperty("leads")]
        public Lead[] Leads { get; set; }
    }
}