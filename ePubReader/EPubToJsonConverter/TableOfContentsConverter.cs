using EPubToJsonConverter.Models;
using System.Linq;
using System.Xml.Linq;

namespace EPubToJsonConverter
{
    public static class TableOfContentsConverter
    {
        public static TableOfContents TableOfContentsToJson(string xml)
        {
            var newToc = new TableOfContents();
            var doc = XDocument.Parse(xml);
            XNamespace tocNamespace = "http://www.daisy.org/z3986/2005/ncx/";

            #region Version
            var version = (from el in doc.Elements(tocNamespace + "ncx")
                           select el).FirstOrDefault()?.Attribute("version").Value;

            newToc.Version = version;
            #endregion Version

            #region Title
            var title = (from el in doc.Descendants(tocNamespace + "docTitle")
                         select el).FirstOrDefault()?.GetInnerXml(tocNamespace, "text");
            newToc.Title = title;
            #endregion Title

            #region Nav
            var navMapElement = (from el in doc.Descendants(tocNamespace + "navMap")
                                 select el).FirstOrDefault();
            var navPoints = from el in doc.Descendants(tocNamespace + "navPoint")
                            select el;

            foreach (var navPoint in navPoints)
            {
                var id = navPoint.Attribute("id").Value;
                var playOrder = int.Parse(navPoint.Attribute("playOrder").Value);
                var text = (from el in navPoint.Elements(tocNamespace + "navLabel")
                            select el).FirstOrDefault()?.GetInnerXml(tocNamespace, "text");
                var source = (from el in navPoint.Elements(tocNamespace + "content")
                              select el).FirstOrDefault()?.Attribute("src")?.Value;

                var newNavPoint = new NavPoint
                {
                    Id = id,
                    PlayOrder = playOrder,
                    Source = source,
                    Text = text
                };

                newToc.NavMap.Add(newNavPoint);
            }
            #endregion Nav

            return newToc;
        }
    }
}
