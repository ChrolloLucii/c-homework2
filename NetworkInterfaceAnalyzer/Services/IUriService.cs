using NetworkInterfaceAnalyzer.Models;

namespace NetworkInterfaceAnalyzer.Services
{
    public interface IUriService
    {
        UriAnalysisResult Analyze(string url);
    }
}