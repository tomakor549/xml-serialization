using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMLSerializable.Models.RequestCode;

namespace XMLSerializable
{
    class Program
    {
        static void Main(string[] args)
        {

            Envelope env = new Envelope("id");

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            ns.Add("sig", "http://signing2.zp.epuap.gov.pl");

            XmlSerializer mySerializer = new XmlSerializer(typeof(Envelope));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter("myFileName.xml");
            mySerializer.Serialize(myWriter, env, ns);
            myWriter.Close();
         }

        void WebCLient(Envelope request)
        {
            using (WebClient webClient = new WebClient())
            {
                //webClient.Proxy = new System.Net.WebProxy("http://localhost:8888");
                webClient.Encoding = Encoding.UTF8;
                if (!string.IsNullOrEmpty(request.SOAPAction))
                {
                    webClient.Headers.Add("SOAPAction", request.SOAPAction);
                }
                webClient.Headers[HttpRequestHeader.ContentType] = "text/xml";

                string response = null;
                try
                {
                    // POST it
                    response = webClient.UploadString(serviceUrl, requestString);
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        var responseStream = ex.Response.GetResponseStream();
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                var responseFault = reader.ReadToEnd();

                                // log
                                //new LoggerFactory().For(this).Debug(Event.SignedMessage, responseFault);

                                fault = new FaultModelHandler().FromSOAP(responseFault);

                                return null;
                            }
                        }
                    }

                    // fallback
                    throw new ServiceException(string.Format("Client call failed for {0} at {1}", request.SOAPAction, serviceUrl), ex);
                }

                if (!string.IsNullOrEmpty(response))
                {
                    // log
                    //new LoggerFactory().For(this).Debug(Event.SignedMessage, response);

                    var responseHandler = new TResultResponseHandler();
                    return responseHandler.FromSOAP(response, out fault);
                }
                else
                {
                    throw new ServiceException(string.Format("Got en empty response from {0} at {1}", request.SOAPAction, serviceUrl));
                }
            }
        }
    }
}
