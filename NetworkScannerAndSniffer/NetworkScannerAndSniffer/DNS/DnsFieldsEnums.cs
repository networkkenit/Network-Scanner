using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{
    public class DnsFieldsEnums
    {

        public enum QR_Type { request, response }

        public enum OP_Code_Type { Standard_Query, Inverse_Query, Server_Status_Request ,Reserved_for_future_use }

        public enum Authoritative_Answer_Type { authoritative, non_authoritative }

        public enum Truncation_Type { exceeds_the_allowed_length_of_512_bytes, non_exceeds }

        public enum Response_Code_Type { No_Error, Format_Error, Server_Failure, Name_Error, Not_Emplemented, Refused }

    }
}
