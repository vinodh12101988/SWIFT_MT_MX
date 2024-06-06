using System;
using System.Collections.Generic;
using System.Text;

namespace SWIFT_MT_MX
{
    public class Data_Models
    {
        public class MTMessage
        {
            public string TransactionReferenceNumber { get; set; }
            public string ValueDate { get; set; }
            public string Currency { get; set; }
            public decimal Amount { get; set; }
            // Add other relevant fields
        }

        public class MXMessage
        {
            public string MessageIdentification { get; set; }
            public DateTime ValueDateTime { get; set; }
            public string CurrencyCode { get; set; }
            public decimal Amount { get; set; }
            // Add other relevant fields
        }

    }
}
