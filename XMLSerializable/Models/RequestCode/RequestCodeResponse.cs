using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLSerializable.Models.RequestCode
{
    [XmlRoot("requestCodeResponse", Namespace = "http://signing2.zp.epuap.gov.pl")]
    public class RequestCodeResponse
    {
        [XmlElement("requestCodeReturn", Namespace = "")]
        public RequestCodeReturn Return { get; set; }
    }

   public class RequestCodeReturn
    {
        [XmlElement("feedbackInfo", Namespace = "")]
        public string FeedbackInfo { get; set; }

        [XmlElement("friendlyCodeId", Namespace = "")]
        public string FriendlyCodeId { get; set; }

        [XmlElement("sessionId", Namespace = "")]
        public string SessionId { get; set; }
    }
}
