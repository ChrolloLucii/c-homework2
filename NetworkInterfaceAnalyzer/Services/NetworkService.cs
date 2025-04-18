using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using NetworkInterfaceAnalyzer.Models;

namespace NetworkInterfaceAnalyzer.Services
{
    public class NetworkService : INetworkService
    {
        public Task<IEnumerable<NetworkInterfaceInfo>> GetNetworkInterfacesAsync()
        {
            var list = NetworkInterface.GetAllNetworkInterfaces()
                .Select(ni =>
                {
                    var props = ni.GetIPProperties();
                    var uni = props.UnicastAddresses
                        .FirstOrDefault(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    return new NetworkInterfaceInfo
                    {
                        Name = ni.Name,
                        Description = ni.Description,
                        Status = ni.OperationalStatus.ToString(),
                        MacAddress = BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes()),
                        Speed = ni.Speed,
                        InterfaceType = ni.NetworkInterfaceType.ToString(),
                        IpAddress = uni?.Address.ToString(),
                        SubnetMask = uni?.IPv4Mask.ToString()
                    };
                });
            return Task.FromResult(list);
        }
    }
}