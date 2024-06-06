using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using static SWIFT_MT_MX.Data_Models;

namespace SWIFT_MT_MX
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string mtContent = @":20:123456789
:32A:210101USD1000
";
            // Step 1: Parse MT message
            var mtMessage = ParseMTMessage(mtContent);

            // Step 2: Map to MX message
            var mxMessage = MapToMX(mtMessage);

            // Step 3: Serialize to MX XML
            string mxXml = SerializeToMXXml(mxMessage);

            // Output the MX XML
            Console.WriteLine(mxXml);
            Console.ReadLine();
        }


        public static MTMessage ParseMTMessage(string mtContent)
        {
            var mtMessage = new MTMessage();
            var lines = mtContent.Split('\n');

            foreach (var line in lines)
            {
                if (line.StartsWith(":20:")) mtMessage.TransactionReferenceNumber = line.Substring(4);
                else if (line.StartsWith(":32A:"))
                {
                    mtMessage.ValueDate = line.Substring(5, 6);
                    mtMessage.Currency = line.Substring(11, 3);
                    mtMessage.Amount = decimal.Parse(line.Substring(14));
                }
                // Parse other fields as needed
            }
            return mtMessage;
        }
        public static MXMessage MapToMX(MTMessage mtMessage)
        {
            var mxMessage = new MXMessage
            {
                MessageIdentification = mtMessage.TransactionReferenceNumber,
                ValueDateTime = DateTime.ParseExact(mtMessage.ValueDate, "yyMMdd", null),
                CurrencyCode = mtMessage.Currency,
                Amount = mtMessage.Amount
                // Map other fields as needed
            };
            return mxMessage;
        }
        public static string SerializeToMXXml(MXMessage mxMessage)
        {
            var xmlSerializer = new XmlSerializer(typeof(MXMessage));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, mxMessage);
                return stringWriter.ToString();
            }
        }

    }
}
