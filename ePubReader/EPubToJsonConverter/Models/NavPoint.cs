using Newtonsoft.Json;

namespace EPubToJsonConverter.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NavPoint
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("play_order")]
        public int PlayOrder { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }
}
