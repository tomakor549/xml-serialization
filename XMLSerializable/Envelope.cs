using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMLSerializable.Models.RequestCode;

namespace XMLSerializable
{
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        public Envelope()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        }
        public Envelope(string id)
        {
            Header = new Header();
            Body = new Body();
            Body.RequestCode.UserTgsid = id;
        }

        [XmlElement("Header", Namespace = "")]
        public Header Header { get; set; }

        [XmlElement("Body", Namespace = "")]
        public Body Body { get; set; }
    }

    public class Header
    {

    }

    public class Body
    {
        public Body()
        {
            RequestCode = new RequestCodeRequest();
        }

        [XmlElement("requestBody", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
        public RequestCodeRequest RequestCode { get; set; }
    }
}
