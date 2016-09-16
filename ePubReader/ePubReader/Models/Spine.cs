using System.Collections.Generic;
using Newtonsoft.Json;

namespace ePubReader.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Spine : BaseNotify
    {
        private string _tableOfContents;
        public string TableOfContents
        {
            get { return _tableOfContents; }
            set { _tableOfContents = value;  RaisePropertyChanged("TableOfContents"); }
        }

        private List<SpineItem> _items = new List<SpineItem>();
        public List<SpineItem> Items
        {
            get { return _items; }
            set { _items = value; RaisePropertyChanged("Items"); }
        }
    }
}
