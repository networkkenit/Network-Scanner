using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{
    public class DnsFields
    {

        #region ID
       public static int ID_Byte_Index = 0;
       public static int ID_Length_bits = 16;
        #endregion

        #region QR
        public static int QR_Byte_Index = 2;

        public static int QR_bit_Index = 0;

        public static int QR_Length_bits = 1;
        #endregion

        #region OPCODE
        public static int OPCODE_Byte_Index = 2;

        public static int OPCODE_bit_Index = 1;

        public static int OPCODE_Length_bits = 4;
        #endregion

        #region Authoritative_Answer
        public static int Authoritative_Answer_Byte_Index = 2;

        public static int Authoritative_Answer_bit_Index = 5;

        public static int Authoritative_Answer_Length_bits = 1;
        #endregion

        #region TrunCation 
        public static int TrunCation_Byte_Index = 2;

        public static int TrunCation_bit_Index = 6;

        public static int TrunCation_Length_bits = 1;
        #endregion

        #region Recursion_Desired
        public static int Recursion_Desired_Byte_Index = 2;

        public static int Recursion_Desired_bit_Index = 7;

        public static int Recursion_Desired_Length_bits = 1;
        #endregion

        #region Recursion_Available
        public static int Recursion_Available_Byte_Index = 3;

        public static int Recursion_Available_bit_Index = 0;

        public static int Recursion_Available_Length_bits = 1;
        #endregion

        #region Zero
        public static int Zero_Byte_Index = 3;

        public static int Zero_bit_Index = 1;

        public static int Zero_Length_bits = 3;
        #endregion

        #region Response_Code
        public static int Response_Code_Byte_Index = 3;

        public static int Response_Code_bit_Index = 4;

        public static int Response_Code_Length_bits = 4;
        #endregion

        #region Number_of_Questions
        public static int Number_of_Questions_Byte_Index = 4;

        public static int Number_of_Questions_Length_bits = 16;
        #endregion

        #region Number_of_Answer
        public static int Number_of_Answer_Byte_Index = 6;

        public static int Number_of_Answer_Length_bits = 16;
        #endregion

        #region Number_of_Authority
        public static int Number_of_Authority_Byte_Index = 8;

        public static int Number_of_Authority_Length_bits = 16;
        #endregion

        #region Number_of_Additional
        public static int Number_of_Additional_Byte_Index = 10;

        public static int Number_of_Additional_Length_bits = 16;
        #endregion

    }
}
