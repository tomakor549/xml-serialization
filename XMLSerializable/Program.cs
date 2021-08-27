using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLSerializable
{
    class Program
    {
        static void Main(string[] args)
        {
            Envelope env = new Envelope("id");

            XmlSerializer mySerializer = new XmlSerializer(typeof(Envelope));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter("myFileName.xml");
            mySerializer.Serialize(myWriter, env);
            myWriter.Close();
        }
    }
}
