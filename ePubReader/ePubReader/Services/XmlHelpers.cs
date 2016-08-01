using System.Linq;
using System.Xml.Linq;

namespace ePubReader.Services
{
    public static class XmlHelpers
    {
        public static string GetInnerXml(this XElement parent, XNamespace xNamespace, string elementName)
        {
            var element = (from xml in parent.Descendants(xNamespace + elementName)
                           select xml).FirstOrDefault();

            if (element == null)
                return string.Empty;
            else
                return element.ReadInnerXml();
        }

        public static string ReadInnerXml(this XElement element)
        {
            var reader = element.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }
    }
}
