using System.Threading.Tasks;
using NetworkInterfaceAnalyzer.Models;

namespace NetworkInterfaceAnalyzer.Services
{
    public interface IPingService
    {
        Task<PingResultInfo> PingAsync(string address);
    }
}