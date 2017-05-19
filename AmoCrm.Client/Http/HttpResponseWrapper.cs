using Newtonsoft.Json;

namespace AmoCrm.Client.Http
{
    public class HttpResponseWrapper<T>
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}