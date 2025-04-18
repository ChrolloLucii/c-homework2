using System;
using NetworkInterfaceAnalyzer.Models;
using System.Net;

namespace NetworkInterfaceAnalyzer.Services
{
    public class UriService : IUriService
    {
        public UriAnalysisResult Analyze(string url)
{
    if (string.IsNullOrWhiteSpace(url))
        throw new ArgumentException("URL не может быть пустым", nameof(url));

    if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
    {
        if (!Uri.TryCreate("http://" + url, UriKind.Absolute, out uri))
            throw new UriFormatException($"Неправильный формат URL: «{url}»");
    }

    string addressType = "Unknown";
    try
    {
        var host = uri.DnsSafeHost;
        IPAddress ip = null;
         if (IPAddress.TryParse(host, out ip))
        {
            }
        else
        {
            // получаем IP через DNS
            var addresses = Dns.GetHostAddresses(host);
            ip = addresses.FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            if (ip != null)
            {
                if (IPAddress.IsLoopback(ip))
                    addressType = "Loopback";
                else
                {
                    var bytes = ip.GetAddressBytes();
                    if (bytes[0] == 10 ||
                        (bytes[0] == 192 && bytes[1] == 168) ||
                        (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31))
                        addressType = "Local";
                    else
                        addressType = "Public";
                }
            }
        }
    }
    catch {}

    return new UriAnalysisResult
    {
        OriginalUrl = url,
        Scheme      = uri.Scheme,
        Host        = uri.DnsSafeHost,
        Port        = uri.IsDefaultPort ? (uri.Scheme == "http" ? 80 : uri.Scheme == "https" ? 443 : (int?)null) : uri.Port,
        Path        = uri.AbsolutePath,
        Query       = uri.Query,
        Fragment    = uri.Fragment,
        AddressType = addressType
    };
}
    }
}