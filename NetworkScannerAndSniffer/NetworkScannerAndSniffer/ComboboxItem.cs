using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public override string ToString()
        {
            return Text;
        }


    }
}
