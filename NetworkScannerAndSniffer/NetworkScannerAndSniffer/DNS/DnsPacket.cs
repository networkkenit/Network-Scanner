using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Net;

namespace NetworkScannerAndSniffer
{
    public class DnsPacket
    {

        byte[] Data { get; set; }
        public DnsPacket(byte[] data)
        {
            Data = data;
        }


        public string ID
        {

            get
            {

                byte[] data = new byte[2];
                data[0] = Data[0];
                data[1] = Data[1];


                return BitConverter.ToString(data).Replace("-", string.Empty);
            }

        }

        public DnsFieldsEnums.QR_Type QR
        {

            get
            {


                string QR_Type_str = new BitMask(DnsFields.QR_bit_Index, DnsFields.QR_Length_bits).Apply(Data[DnsFields.QR_Byte_Index]);

                return QR_Type_str == "1" ? DnsFieldsEnums.QR_Type.request : DnsFieldsEnums.QR_Type.response;



            }

        }

        public DnsFieldsEnums.OP_Code_Type OP_Code
        {
            get
            {


                string OP_Code_str = new BitMask(DnsFields.OPCODE_bit_Index, DnsFields.OPCODE_Length_bits).Apply(Data[DnsFields.OPCODE_Byte_Index]);

                if (OP_Code_str == "0")
                    return DnsFieldsEnums.OP_Code_Type.Standard_Query;

                else if (OP_Code_str == "1")
                    return DnsFieldsEnums.OP_Code_Type.Inverse_Query;

                else if (OP_Code_str == "2")
                    return DnsFieldsEnums.OP_Code_Type.Server_Status_Request;

                else return DnsFieldsEnums.OP_Code_Type.Reserved_for_future_use;
            }

        }

        public DnsFieldsEnums.Authoritative_Answer_Type Authoritative_Answer
        {
            get
            {


                string Authoritative_Answer_str = new BitMask(DnsFields.Authoritative_Answer_bit_Index, DnsFields.Authoritative_Answer_Length_bits).Apply(Data[DnsFields.Authoritative_Answer_Byte_Index]);

                if (Authoritative_Answer_str == "0")
                    return DnsFieldsEnums.Authoritative_Answer_Type.non_authoritative;
                else
                    return DnsFieldsEnums.Authoritative_Answer_Type.authoritative;


            }
        }

        public DnsFieldsEnums.Truncation_Type Truncation
        {
            get
            {

                string Truncation_str = new BitMask(DnsFields.TrunCation_bit_Index, DnsFields.TrunCation_Length_bits).Apply(Data[DnsFields.TrunCation_Byte_Index]);

                if (Truncation_str == "0")
                    return DnsFieldsEnums.Truncation_Type.non_exceeds;
                else
                    return DnsFieldsEnums.Truncation_Type.exceeds_the_allowed_length_of_512_bytes;


            }
        }

        public string Recursion_Desired
        {

            get
            {

                return new BitMask(DnsFields.Recursion_Desired_bit_Index, DnsFields.Recursion_Desired_Length_bits).Apply(Data[DnsFields.Recursion_Desired_Byte_Index]);
           
            }

        }

        public string Recursion_Available
        {

            get
            {

                return new BitMask(DnsFields.Recursion_Available_bit_Index, DnsFields.Recursion_Available_Length_bits).Apply(Data[DnsFields.Recursion_Available_Byte_Index]);

            }

        }


        public string Zero
        {

            get
            {

                return new BitMask(DnsFields.Zero_bit_Index, DnsFields.Zero_Length_bits).Apply(Data[DnsFields.Zero_Byte_Index]);

            }

        }

        public DnsFieldsEnums.Response_Code_Type Response_Code
        {
            get
            {


                string Response_Code_str = new BitMask(DnsFields.Response_Code_bit_Index, DnsFields.Response_Code_Length_bits).Apply(Data[DnsFields.Response_Code_Byte_Index]);

                if (Response_Code_str == "0")
                {

                    return DnsFieldsEnums.Response_Code_Type.No_Error;

                }
                else if (Response_Code_str == "1")
                {

                    return DnsFieldsEnums.Response_Code_Type.Format_Error;

                }
                else if (Response_Code_str == "2")

                    return DnsFieldsEnums.Response_Code_Type.Server_Failure;

                else if (Response_Code_str == "3")
                    return DnsFieldsEnums.Response_Code_Type.Name_Error;

                else if (Response_Code_str == "4")
                    return DnsFieldsEnums.Response_Code_Type.Not_Emplemented;


                else return DnsFieldsEnums.Response_Code_Type.Refused;


            }
        }

        public Int64 Number_of_Questions
        {

            get
            {

                int lastindex;
               return ExtracktIntFromField(DnsFields.Number_of_Questions_Byte_Index, DnsFields.Number_of_Questions_Length_bits / 8,out lastindex);

            }

        }

        public Int64 Number_of_Answer
        {

            get
            {

                int lastindex;
                return ExtracktIntFromField(DnsFields.Number_of_Answer_Byte_Index, DnsFields.Number_of_Answer_Length_bits / 8, out lastindex);

            }

        }

        public Int64 Number_of_Authority
        {

            get
            {

                int lastindex;
                return ExtracktIntFromField(DnsFields.Number_of_Authority_Byte_Index, DnsFields.Number_of_Authority_Length_bits / 8, out lastindex);

            }

        }

        public Int64 Number_of_Additional
        {

            get
            {

                int lastindex;
                return ExtracktIntFromField(DnsFields.Number_of_Additional_Byte_Index, DnsFields.Number_of_Additional_Length_bits / 8, out lastindex);

            }

        }

      

        public List<IDNSINFO> DNS_INFO
        {
            get
            {

                List<IDNSINFO> result = new List<IDNSINFO>();


                if (Data.Length > 12)
                {
                    int lastindex;
                    int CurrentIndex = 12;

                    for (int i = 0; i < this.Number_of_Questions; i++)
                    {
                        string name = GetDomainName(CurrentIndex, out lastindex);
                        string Type = ExtracktIntFromField(lastindex,2,out lastindex).ToString();
                        string Class = ExtracktIntFromField(lastindex, 2, out lastindex).ToString();
                        result.Add(new Question(DNSType.Question, name,Type, Class));
                        CurrentIndex = lastindex;

                    }
                    for (int i = 0; i < Number_of_Answer; i++)
                    {
                        string name = GetDomainName(CurrentIndex, out lastindex);
                        string Type = ExtracktIntFromField(lastindex,2,out lastindex).ToString();
                        string Class = ExtracktIntFromField(lastindex, 2, out lastindex).ToString();
                        Int64 TTL = ExtracktIntFromField(lastindex, 4, out lastindex);
                        Int64 Length = ExtracktIntFromField(lastindex, 2, out lastindex);
                        CurrentIndex =(int) (lastindex + Length);
                        IResourceData data = GetResourceData(lastindex, int.Parse(Type));
                        result.Add(new ResourceRecords(DNSType.ResourceRecords,RRsType.Answer, name, Type, Class,TTL,Length, data));
                    }
                    for (int i = 0; i < Number_of_Authority; i++)
                    {

                        string name = GetDomainName(CurrentIndex, out lastindex);
                        string Type = ExtracktIntFromField(lastindex, 2, out lastindex).ToString();
                        string Class = ExtracktIntFromField(lastindex, 2, out lastindex).ToString();
                        Int64 TTL = ExtracktIntFromField(lastindex, 4, out lastindex);
                        Int64 Length = ExtracktIntFromField(lastindex, 2, out lastindex);
                        CurrentIndex = (int)(lastindex + Length);
                        IResourceData data = GetResourceData(lastindex, int.Parse(Type));
                        result.Add(new ResourceRecords(DNSType.ResourceRecords,RRsType.Authority, name, Type, Class, TTL, Length, data));

                    }

                    for (int i = 0; i < Number_of_Additional; i++)
                    {

                        string name = GetDomainName(CurrentIndex, out lastindex);
                        string Type = ExtracktIntFromField(lastindex, 2, out lastindex).ToString();
                        string Class = ExtracktIntFromField(lastindex, 2, out lastindex).ToString();
                        Int64 TTL = ExtracktIntFromField(lastindex, 4, out lastindex);
                        Int64 Length = ExtracktIntFromField(lastindex, 2, out lastindex);
                        CurrentIndex = (int)(lastindex + Length);
                        IResourceData data = GetResourceData(lastindex, int.Parse(Type));
                        result.Add(new ResourceRecords(DNSType.ResourceRecords,RRsType.Additional, name, Type, Class, TTL, Length, data));

                    }


                }


                return result;

            }


        }

        Int64 ExtracktIntFromField(int FieldIndex , int FieldLength,out int LastIndex)
        {

            byte[] Bytes = new byte[FieldLength];
            LastIndex = FieldIndex;
            for (int i = 0; i < Bytes.Length; i++)
            {
                LastIndex = LastIndex + 1;
                Bytes[i] = Data[FieldIndex+i];
            }
            
            return Int64.Parse(BitConverter.ToString(Bytes).Replace("-", string.Empty), System.Globalization.NumberStyles.HexNumber);

        }

        public string GetDomainName(int CurrentIndex, out int lastindex)
        {

            int index = CurrentIndex;

            int length = Data[CurrentIndex];

            string domsubstr = string.Empty;

            string domainnameresult = string.Empty;

            while (length > 0)
            {
             
                if(length < 64) { 

                    domsubstr = string.Empty;
                    for (int i = 1; i <= length; i++)
                        domsubstr += (char)Data[index + i];

                    domainnameresult += domsubstr + ".";

                    index = index + length + 1;

                    length = Data[index];
         
                }

              else if (length > 191 && length < 255)
                {
                    int domlasind = 0;
                    index = index + 1;
                    Int64 domindex = ExtracktIntFromField(index, 1, out index);
                    domainnameresult += GetDomainName((int)domindex, out domlasind);
                    lastindex = index;
                    return domainnameresult;
                }

            }
            

            lastindex = index +1;

            return domainnameresult.Remove(domainnameresult.Length - 1, 1).ToString();


        }

        IResourceData GetResourceData(int CurrentIndex, int type)
        {

            int index = CurrentIndex;
            int lastindex;
            ResourceDataType typeresult = (ResourceDataType)type;
            switch (typeresult)
            {
                           
                case ResourceDataType.A:
                    StringBuilder ip_v4_addr = new StringBuilder();    
                    for (int i = 0; i < 4; i++)
                        if (i == 3) ip_v4_addr.Append(Data[index + i]);
                        else ip_v4_addr.Append(Data[index + i] + ".");
                        
                    return ResourceDataFactory.BuildResourceData(typeresult, ip_v4_addr.ToString());
                case ResourceDataType.AAAA:
                    StringBuilder ip_v6_addr = new StringBuilder();    
                    for (int i = 0; i <15; i=i+2)
                    {
                        byte[] data = new byte[2];
                        data[0] = Data[index + i];
                        data[1] = Data[index + i + 1];
                        string hex = BitConverter.ToString(data).Replace("-", string.Empty);
                        if (i == 14)
                        {
                            ip_v6_addr.Append(hex);

                        }
                        else
                        {
                            ip_v6_addr.Append(hex + ":");

                        }

                    }
                    return ResourceDataFactory.BuildResourceData(typeresult, ip_v6_addr.ToString()); 
                case ResourceDataType.NS:
                    return ResourceDataFactory.BuildResourceData(typeresult, GetDomainName(index, out lastindex)); 
                case ResourceDataType.CNAME:
                    return ResourceDataFactory.BuildResourceData(typeresult, GetDomainName(index, out lastindex));
                case ResourceDataType.SOA:
                   string MName = GetDomainName(index, out lastindex);
                   string RName = GetDomainName(lastindex, out lastindex);
                   Int64 serial = ExtracktIntFromField(lastindex, 4,out lastindex);
                   Int64 Refresh = ExtracktIntFromField(lastindex, 4, out lastindex); 
                   Int64 Retry = ExtracktIntFromField(lastindex, 4, out lastindex); 
                   Int64 Expire = ExtracktIntFromField(lastindex, 4, out lastindex) ;
                   Int64 TTL = ExtracktIntFromField(lastindex, 4, out lastindex) ;
                    lastindex = lastindex + 1;
                    return ResourceDataFactory.BuildResourceData(typeresult, MName, RName, serial, Refresh, Retry, Expire, TTL);
                case ResourceDataType.PTR:
                    return ResourceDataFactory.BuildResourceData(typeresult, GetDomainName(index, out lastindex));
                case ResourceDataType.MX:

                  Int64 Preference = ExtracktIntFromField(index, 4, out lastindex);
                  string  Mail = GetDomainName(lastindex,out lastindex);
                    return ResourceDataFactory.BuildResourceData(typeresult, Preference, Mail);
                default: return null;
            }


        }

    }
}
