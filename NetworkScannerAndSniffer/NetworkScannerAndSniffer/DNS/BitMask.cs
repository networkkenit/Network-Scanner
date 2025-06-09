using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{
    public class BitMask
    {

        int First_Bit_Index { get; set; }

        int Bits_Count { get; set; }

        public int Mask { get; set; }

        public  BitMask(int _First_Bit_Index, int _Bits_Count)
        {
            this.First_Bit_Index = _First_Bit_Index;
            this.Bits_Count = _Bits_Count;
            Mask = Create();

        }


        int Create()
        {

            string zerosByte = "00000000";

            StringBuilder sb = new StringBuilder(zerosByte);

            for (int i = this.First_Bit_Index; i < First_Bit_Index + this.Bits_Count; i++)
            {

                sb[i] = '1';

            }

            return Convert.ToInt32(sb.ToString(), 2);
        }

        int CalculateShiftvalue(int index, int length) => 8 - (index + length);

      public string Apply(byte Byte)
        {

            int Shift_value = CalculateShiftvalue(this.First_Bit_Index, this.Bits_Count);
            if(Shift_value!= 0)
            return ((Byte & Mask) >> Shift_value).ToString();
            else return (Byte & Mask).ToString();

        }

    }
}
