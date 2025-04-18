using System.Collections.Generic;
using NetworkInterfaceAnalyzer.Models;

namespace NetworkInterfaceAnalyzer.Services
{
    public interface IHistoryService
    {
        void Add(UrlHistoryItem item);
        IEnumerable<UrlHistoryItem> GetAll();
    }
}