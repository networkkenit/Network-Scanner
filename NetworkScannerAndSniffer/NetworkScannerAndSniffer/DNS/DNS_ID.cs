using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{
    public class DNS_ID
    {

        public string Id { get; set; }  

        public Color color { get; set; }

        public DNS_ID(string _id,Color colo)
        { 
        
            Id = _id;
            color = colo;

        }

    }
}
