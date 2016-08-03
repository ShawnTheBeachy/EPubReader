namespace ePubReader.Models
{
    public class SpineItem : BaseNotify
    {
        private string _idRef;
        public string IdRef
        {
            get { return _idRef; }
            set { _idRef = value;  RaisePropertyChanged("IdRef"); }
        }
    }
}
