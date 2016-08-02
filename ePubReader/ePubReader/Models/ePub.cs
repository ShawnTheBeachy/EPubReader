using System.Collections.Generic;
using Windows.Storage.Streams;

namespace ePubReader.Models
{
    public class ePub : BaseNotify
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        private string _mimeType;
        public string MimeType
        {
            get { return _mimeType; }
            set { _mimeType = value; RaisePropertyChanged("MimeType"); }
        }

        private IRandomAccessStream _coverStream;
        public IRandomAccessStream CoverStream
        {
            get { return _coverStream; }
            set { _coverStream = value; RaisePropertyChanged("CoverStream"); }
        }
        
        public bool IsValid { get { return MimeType == "application/epub+zip"; } }

        private Metadata _metadata;
        public Metadata Metadata
        {
            get { return _metadata; }
            set { _metadata = value; RaisePropertyChanged("Metadata"); }
        }

        private List<ManifestItem> _manifest = new List<ManifestItem>();
        public List<ManifestItem> Manifest
        {
            get { return _manifest; }
            set { _manifest = value; RaisePropertyChanged("Manifest"); }
        }

        private Spine _spine;
        public Spine Spine
        {
            get { return _spine; }
            set { _spine = value; RaisePropertyChanged("Spine"); }
        }

        private string _rootFolderPath;
        public string RootFolderPath
        {
            get { return _rootFolderPath; }
            set { _rootFolderPath = value; RaisePropertyChanged("RootFolderPath"); }
        }

        private string _coverId;
        public string CoverId
        {
            get { return _coverId; }
            set { _coverId = value; RaisePropertyChanged("CoverId"); }
        }
    }
}
