using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkInterfaceAnalyzer.Models;
namespace NetworkInterfaceAnalyzer.Services{
    public interface INetworkService{
        Task<IEnumerable<NetworkInterfaceInfo>> GetNetworkInterfacesAsync();
    }
}