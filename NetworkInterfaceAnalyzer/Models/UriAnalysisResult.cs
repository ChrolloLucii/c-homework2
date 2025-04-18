namespace NetworkInterfaceAnalyzer.Models
{
    public class UriAnalysisResult
    {
        public string OriginalUrl { get; set; }
        public string Scheme { get; set; }
        public string Host { get; set; }
        public int? Port { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string Fragment { get; set; }
        public string AddressType { get; set; }
    }
}