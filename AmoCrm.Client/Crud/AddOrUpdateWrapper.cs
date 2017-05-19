#region Usings

using Newtonsoft.Json;

#endregion

namespace AmoCrm.Client.Crud
{
    public class AddOrUpdateWrapper<T>
    {
        [JsonProperty("add")]
        public T[] Add { get; set; }

        [JsonProperty("update")]
        public T[] Update { get; set; }
    }
}