using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using NetworkInterfaceAnalyzer.Models;
using System.Diagnostics;
namespace NetworkInterfaceAnalyzer.Services
{
    public class PingService : IPingService
    {
        public async Task<PingResultInfo> PingAsync(string address)
{
    //полный url -> только host
     if (Uri.TryCreate(address, UriKind.Absolute, out var u))
    {
        if (string.IsNullOrWhiteSpace(u.Host))
            throw new ArgumentException("URL не содержит хоста для пинга.");
        address = u.Host;
    }

    if (string.IsNullOrWhiteSpace(address))
        throw new ArgumentException("пустой адрес для пинга.");

    try
    {
        Debug.WriteLine($"Пингую: {address}");
        
        //чистый host -> IP
        IPAddress ip;
        if (!IPAddress.TryParse(address, out ip))
{
    var addrs = await Dns.GetHostAddressesAsync(address);
    if (addrs == null || addrs.Length == 0)
        throw new Exception("не удалось получить IP-адрес для хоста.");
    ip = addrs.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
         ?? addrs.First();
    if (ip == null)
        throw new Exception("не удалось определить подходящий IP-адрес для хоста.");
}

        using var ping = new Ping();
        var reply = await ping.SendPingAsync(ip, 4000);
        Debug.WriteLine($"Пинг завершён: {address}, статус: {reply.Status}");
        return new PingResultInfo
        {
            Address = address,
            Success = reply.Status == IPStatus.Success,
            RoundtripTime = reply.RoundtripTime,
            Status = reply.Status.ToString()
        };
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Ошибка пинга: {address}, {ex.Message}");
        return new PingResultInfo
        {
            Address = address,
            Success = false,
            RoundtripTime = -1,
            Status = ex.Message
        };
    }
}
    }
}