using Newtonsoft.Json;
using System.Collections.Generic;

namespace EPubToJsonConverter.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TableOfContents
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("nav_map")]
        public List<NavPoint> NavMap { get; set; } = new List<NavPoint>();
    }
}
