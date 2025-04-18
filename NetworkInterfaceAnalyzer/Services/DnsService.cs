using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NetworkInterfaceAnalyzer.Models;

namespace NetworkInterfaceAnalyzer.Services
{
    public class DnsService : IDnsService
    {
        public async Task<DnsInfo> GetDnsInfoAsync(string host)
        {
            var entry = await Dns.GetHostEntryAsync(host);
            return new DnsInfo
            {
                HostName = entry.HostName,
                Aliases = entry.Aliases.ToList(),
                AddressList = entry.AddressList.Select(a => a.ToString()).ToList()
            };
        }
    }
}