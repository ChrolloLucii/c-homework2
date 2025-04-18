namespace NetworkInterfaceAnalyzer.Models{
    public class UrlHistoryItem{
        public DateTime CheckedAt {get;set;}
        public string Url {get;set;}
        public bool IsReachable{get;set;}
    }
}