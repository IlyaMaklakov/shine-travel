using Newtonsoft.Json;

namespace AmoCrm.Client.Pipelines
{
    public class PipeLineStatus
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pipeline_id")]
        public string PipeLineId { get; set; }

        [JsonProperty("sort")]
        public int Order { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("editable")]
        public string Editable { get; set; }
    }
}