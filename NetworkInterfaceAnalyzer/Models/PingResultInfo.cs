namespace NetworkInterfaceAnalyzer.Models{
    public class PingResultInfo{
        public string Address {get;set;}
        public bool Success{get;set;}
        public long RoundtripTime{get;set;}
        public string Status{get;set;}
    }
}