using System.Collections.Generic;

namespace ePubReader.Models
{
    public class Spine
    {
        public string TableOfContents { get; set; }
        public List<SpineItem> Items { get; set; } = new List<SpineItem>();
    }
}
