using System.Collections.Generic;
using Windows.Storage.Streams;

namespace ePubReader.Models
{
    public class ePub
    {
        public string Id { get; set; }
        public string MimeType { get; set; }
        public IRandomAccessStream CoverStream { get; set; }
        public bool IsValid { get { return MimeType == "application/epub+zip"; } }
        public Metadata Metadata { get; set; }
        public List<ManifestItem> Manifest { get; set; } = new List<ManifestItem>();
        public Spine Spine { get; set; }
        public string RootFolderPath { get; set; }
        public string CoverId { get; set; }
    }
}
