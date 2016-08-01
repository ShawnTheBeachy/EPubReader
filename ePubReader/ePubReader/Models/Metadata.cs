using System;
using System.Collections.Generic;

namespace ePubReader.Models
{
    //From http://dublincore.org/documents/2012/06/14/dcmi-terms/?v=elements
    public class Metadata
    {
        /// <summary>
        /// An entity responsible for making contributions to the resource. 
        /// </summary>
        public string Contributor { get; set; }

        /// <summary>
        /// The spatial or temporal topic of the resource, the spatial applicability of the resource, or the jurisdiction under which the resource is relevant.
        /// </summary>
        public string Coverage { get; set; }

        /// <summary>
        /// An entity primarily responsible for making the resource.
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// A point or period of time associated with an event in the lifecycle of the resource.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The file format, physical medium, or dimensions of the resource.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// An unambiguous reference to the resource within a given context.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// A language of the resource.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// An entity responsible for making the resource available.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// A related resource.
        /// </summary>
        public string Relation { get; set; }

        /// <summary>
        /// Information about rights held in and over the resource.
        /// </summary>
        public string Rights { get; set; }

        /// <summary>
        /// A related resource from which the described resource is derived.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The topics of the resource. (Multiple "subject" XML elements.)
        /// </summary>
        public List<string> Subjects { get; set; } = new List<string>();

        /// <summary>
        /// A name given to the resource.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The nature or genre of the resource.
        /// </summary>
        public string Type { get; set; }
    }
}
