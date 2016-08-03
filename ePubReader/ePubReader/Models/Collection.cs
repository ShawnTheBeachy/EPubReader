using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage.Streams;

namespace ePubReader.Models
{
    public class Collection : BaseNotify
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private IRandomAccessStream _coverStream;
        public IRandomAccessStream CoverStream
        {
            get { return _coverStream; }
            set { _coverStream = value; RaisePropertyChanged("CoverStream"); }
        }

        private List<string> _includedIds = new List<string>();
        public List<string> IncludedIds
        {
            get { return _includedIds; }
            set { _includedIds = value; RaisePropertyChanged("IncludedIds"); }
        }

        public IEnumerable<ePub> IncludedEPubs { get { return ViewModel.EPubs.Where(a => IncludedIds.Contains(a.Id)); } }
    }
}
