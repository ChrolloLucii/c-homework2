namespace NetworkInterfaceAnalyzer.Models{
    public class DnsInfo{
        public string HostName {get;set;}
        public List<string> Aliases{get;set;}
        public List<string> AddressList{get;set;}
    }
}