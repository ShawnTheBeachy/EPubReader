using System;
using System.Collections.Generic;

namespace ePubReader.Models
{
    //From http://dublincore.org/documents/2012/06/14/dcmi-terms/?v=elements
    public class Metadata : BaseNotify
    {
        /// <summary>
        /// An entity responsible for making contributions to the resource. 
        /// </summary>
        private string _contributor;
        public string Contributor
        {
            get { return _contributor; }
            set { _contributor = value; RaisePropertyChanged("Contributor"); }
        }

        /// <summary>
        /// The spatial or temporal topic of the resource, the spatial applicability of the resource, or the jurisdiction under which the resource is relevant.
        /// </summary>
        private string _coverage;
        public string Coverage
        {
            get { return _coverage; }
            set { _coverage = value; RaisePropertyChanged("Coverage"); }
        }

        /// <summary>
        /// An entity primarily responsible for making the resource.
        /// </summary>
        private string _creator;
        public string Creator
        {
            get { return _creator; }
            set { _creator = value; RaisePropertyChanged("Creator"); }
        }

        /// <summary>
        /// A point or period of time associated with an event in the lifecycle of the resource.
        /// </summary>
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; RaisePropertyChanged("Date"); }
        }

        /// <summary>
        /// The file format, physical medium, or dimensions of the resource.
        /// </summary>
        private string _format;
        public string Format
        {
            get { return _format; }
            set { _format = value; RaisePropertyChanged("Format"); }
        }

        /// <summary>
        /// An unambiguous reference to the resource within a given context.
        /// </summary>
        private string _identifier;
        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; RaisePropertyChanged("Identifier"); }
        }

        /// <summary>
        /// A language of the resource.
        /// </summary>
        private string _language;
        public string Language
        {
            get { return _language; }
            set { _language = value; RaisePropertyChanged("Language"); }
        }

        /// <summary>
        /// An entity responsible for making the resource available.
        /// </summary>
        private string _publisher;
        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; RaisePropertyChanged("Publisher"); }
        }

        /// <summary>
        /// A related resource.
        /// </summary>
        private string _relation;
        public string Relation
        {
            get { return _relation; }
            set { _publisher = value; RaisePropertyChanged("Relation"); }
        }

        /// <summary>
        /// Information about rights held in and over the resource.
        /// </summary>
        private string _rights;
        public string Rights
        {
            get { return _rights; }
            set { _rights = value; RaisePropertyChanged("Rights"); }
        }

        /// <summary>
        /// A related resource from which the described resource is derived.
        /// </summary>
        private string _source;
        public string Source
        {
            get { return _source; }
            set { _source = value; RaisePropertyChanged("Source"); }
        }

        /// <summary>
        /// The topics of the resource. (Multiple "subject" XML elements.)
        /// </summary>
        private List<string> _subjects = new List<string>();
        public List<string> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; RaisePropertyChanged("Subjects"); }
        }

        /// <summary>
        /// A name given to the resource.
        /// </summary>
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged("Title"); }
        }

        /// <summary>
        /// The nature or genre of the resource.
        /// </summary>
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; RaisePropertyChanged("Type"); }
        }
    }
}
