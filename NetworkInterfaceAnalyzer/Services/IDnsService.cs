using System.Threading.Tasks;
using NetworkInterfaceAnalyzer.Models;

namespace NetworkInterfaceAnalyzer.Services
{
    public interface IDnsService
    {
        Task<DnsInfo> GetDnsInfoAsync(string host);
    }
}