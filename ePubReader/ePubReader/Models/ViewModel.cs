using MyToolkit.Collections;

namespace ePubReader.Models
{
    public static class ViewModel
    {
        public static MtObservableCollection<ePub> EPubs { get; set; } = new MtObservableCollection<ePub>();
        public static MtObservableCollection<Collection> Collections { get; set; } = new MtObservableCollection<Collection>();
    }
}
