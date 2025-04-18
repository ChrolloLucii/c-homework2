namespace NetworkInterfaceAnalyzer.Models
{
    public class NetworkInterfaceInfo
    {
        public string Name {get;set;}
        public string Description{get;set;}
        public string Status{get;set;}
        public string MacAddress{get;set;}
        public long Speed{get;set;}
        public string InterfaceType{get;set;}
        public string IpAddress{get;set;}
        public string SubnetMask{get;set;}
    }
}