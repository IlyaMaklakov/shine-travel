#region Usings

using System.Collections.Generic;

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Pipelines
{
    public class PipeLine
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("last_modified")]
        public string LastModifiedTime { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sort")]
        public int Order { get; set; }

        [JsonProperty("statuses")]
        public Dictionary<string, PipeLineStatus> Statuses { get; set; }

        [JsonProperty("is_main")]
        public bool IsMain { get; set; }
    }
}