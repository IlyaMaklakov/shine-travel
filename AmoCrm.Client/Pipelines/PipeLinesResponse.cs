using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AmoCrm.Client.Http;

using Newtonsoft.Json;

namespace AmoCrm.Client.Pipelines
{
    public class PipeLinesResponse : BaseResponse
    {

        [JsonProperty("pipelines")]
        public Dictionary<string, PipeLine> PipeLines { get; set; }

        [JsonProperty("colors")]
        public string[] Colors { get; set; }
    }
}
