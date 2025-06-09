using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{
    public enum RRsType { Answer, Authority, Additional }

    public interface IDNSINFO{

        public DNSType DNSType { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string Class { get; set; }


    }
    public class Question : IDNSINFO
    {
        public DNSType DNSType { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Class { get; set; }

        public Question(DNSType dNSType,string _Name, string _Type, string _Class)
        {

            Name = _Name;
            Type = _Type;
            Class = _Class;
            this.DNSType = dNSType;


        }


    }

    public class ResourceRecords : IDNSINFO
    {
        public DNSType DNSType { get; set; }
        public RRsType RRsType { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public Int64 TTL { get; set; }
        public Int64 ResourceDataLength { get; set; }
        public IResourceData RData { get; set; }

        public ResourceRecords(DNSType dNSType, RRsType _RRsType, string _Name, string _Type, string _Class, Int64 _TTl, Int64 _ResourceDataLength, IResourceData _RData)
        {

            this.RRsType = _RRsType;
            this.Name = _Name;
            this.Type = _Type;
            this.Class = _Class;
            this.TTL = _TTl;
            this.ResourceDataLength = _ResourceDataLength;
            this.RData = _RData;
            this.DNSType = dNSType;

        }

    }

    public enum DNSType {Question, ResourceRecords }
    public enum ResourceDataType
    {
        
        A = 1,
        NS =2,
        MD = 3,
        MF = 4,
        CNAME = 5,
        SOA = 6,
        MB = 7,
        MG = 8,
        MR = 9,
        NULL = 10,
        WKS = 11,
        PTR = 12,
        HINFO = 13,
        MINFO = 14,
        MX = 15,
        TXT = 16,
        AAAA = 28,

    }
 
    public enum ClassType { IN = 1 , CS = 2, CH = 3 ,HS = 4,ANY =255 }
    public interface IResourceData
    {
        public ResourceDataType Type { get; set; }
    }

    public class ResourceDataTypeA : IResourceData
    {
        public ResourceDataType Type { get; set; }
       public string IPV4 { get; set; }

        public ResourceDataTypeA(string _IPV4)
        {
            this.IPV4 = _IPV4;
            Type = ResourceDataType.A;
        }

    }

    public class ResourceDataTypeAAAA : IResourceData
    {
        public ResourceDataType Type { get; set; }

        public string IPV6 { get; set; }

        public ResourceDataTypeAAAA(string _IPV6)
        {
            this.IPV6 = _IPV6;
            Type = ResourceDataType.AAAA;

        }

    }

    public class ResourceDataTypeNS : IResourceData
    {
        public ResourceDataType Type { get; set; }

        public string DomainName { get; set; }

        public ResourceDataTypeNS(string _DomainName)
        {
            this.DomainName = _DomainName;
            Type = ResourceDataType.NS;
        }

    }
    public class ResourceDataTypeCNAME : IResourceData
    {
        public ResourceDataType Type { get; set; }

        public string DomainName { get; set; }

        public ResourceDataTypeCNAME(string _DomainName)
        {
            this.DomainName = _DomainName;
            Type = ResourceDataType.CNAME;

        }

    }

    public class ResourceDataTypePTR : IResourceData
    {
        public ResourceDataType Type { get; set; }

        public string DomainName { get; set; }

        public ResourceDataTypePTR(string _DomainName)
        {
            this.DomainName = _DomainName;
            Type = ResourceDataType.PTR;

        }

    }
    public class ResourceDataTypeSOA : IResourceData
    {
        public ResourceDataType Type { get; set; }
        public string DomainName { get; set; }
        public string mail { get; set; }
        public Int64 serial { get; set; }
        public Int64 Refresh { get; set; }
        public Int64 Retry  { get; set; }
        public Int64 Expire  { get; set; }
        public Int64 TTL { get; set; }


        public ResourceDataTypeSOA(string _DomainName,string _mail, Int64 _serial, Int64 _Refresh, Int64 _Retry, Int64 _Expire, Int64 _TTL)
        {
            this.DomainName = _DomainName;
            this.mail = _mail;
            this.serial = _serial;
            this.Refresh = _Refresh;
            this.Retry = _Retry;
            this.Expire = _Expire;
            this.TTL = _TTL;
            Type = ResourceDataType.SOA;


        }

    }

    public class ResourceDataTypeMX : IResourceData
    {
        public ResourceDataType Type { get; set; }

        public Int64 Preference { get; set; }
        public string mail { get; set; }

        public ResourceDataTypeMX(Int64 _Preference, string _mail)
        {
            this.mail = _mail;
            Preference = _Preference;
            Type = ResourceDataType.MX;

        }

    }

    public static class ResourceDataFactory
    {
        
        public static IResourceData BuildResourceData(ResourceDataType resourceData,params object[] Parameters)
        {
         
            switch (resourceData)
            {
                case ResourceDataType.A:return new ResourceDataTypeA((string)Parameters[0]);
                case ResourceDataType.NS:return new ResourceDataTypeNS((string)Parameters[0]);
                case ResourceDataType.MX:return new ResourceDataTypeMX((Int64)Parameters[0], (string)Parameters[1]);
                case ResourceDataType.CNAME:return new ResourceDataTypeCNAME((string)Parameters[0]);
                case ResourceDataType.PTR: return new ResourceDataTypePTR((string)Parameters[0]);
                case ResourceDataType.SOA:return new ResourceDataTypeSOA((string)Parameters[0], (string)Parameters[1],(Int64)Parameters[2],(Int64)Parameters[3],(Int64)Parameters[4],(Int64)Parameters[5], (Int64)Parameters[5]);
                case ResourceDataType.AAAA:return new ResourceDataTypeAAAA((string)Parameters[0]);
                default: return null;
            }
            
        }

    }

}
