using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using XMLSerializable.Models.Faults;
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

            XmlSerializer xmlSerializer = new XmlSerializer(env.GetType());

            string requestString = null;

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, env, ns);
                requestString = textWriter.ToString();
            }
            Console.WriteLine(requestString);

            Fault fault = null;

            // sending WS-Security request
            using (WebClient webClient = new WebClient())
            {
                //webClient.Proxy = new System.Net.WebProxy("http://localhost:8888");
                webClient.Encoding = Encoding.UTF8;
                if (!string.IsNullOrEmpty(env.Body.RequestCode.SOAPAction))
                {
                    webClient.Headers.Add("SOAPAction", env.Body.RequestCode.SOAPAction);
                }
                webClient.Headers[HttpRequestHeader.ContentType] = "text/xml";

                string response = null;
                try
                {
                    //wybierz gdzie wysłać
                    response = webClient.UploadString("https://5487.requestcatcher.com/send", requestString);
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

                            }
                        }
                    }

                }

                if (!string.IsNullOrEmpty(response))
                {
                }
                else
                {
                }
            }


        }

    }
}
