using System.Collections.Generic;
using NetworkInterfaceAnalyzer.Models;

namespace NetworkInterfaceAnalyzer.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly List<UrlHistoryItem> _history = new();
        public void Add(UrlHistoryItem item) => _history.Add(item);
        public IEnumerable<UrlHistoryItem> GetAll() => _history;
    }
}