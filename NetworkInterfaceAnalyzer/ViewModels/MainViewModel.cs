using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using NetworkInterfaceAnalyzer.Models;
using NetworkInterfaceAnalyzer.Services;
using System.Windows; 
namespace NetworkInterfaceAnalyzer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NetworkInterfaceInfo> Interfaces { get; } = new();
        public ObservableCollection<UrlHistoryItem> UrlHistory    { get; } = new();

        private NetworkInterfaceInfo? _selectedInterface;
        public NetworkInterfaceInfo? SelectedInterface
        {
            get => _selectedInterface;
            set { _selectedInterface = value; OnPropertyChanged(nameof(SelectedInterface)); }
        }

        private string _urlInput = "";
        public string UrlInput
        {
            get => _urlInput;
            set { _urlInput = value; OnPropertyChanged(nameof(UrlInput)); AnalyzeCommand.RaiseCanExecuteChanged(); }
        }

        private UriAnalysisResult? _uriResult;
        public UriAnalysisResult? UriResult
        {
            get => _uriResult;
            set { _uriResult = value; OnPropertyChanged(nameof(UriResult)); }
        }

        public RelayCommand RefreshCommand { get; }
        public RelayCommand AnalyzeCommand { get; }

        private readonly INetworkService _net;
        private readonly IUriService     _uri;
        private readonly IPingService    _ping;
        private readonly IDnsService     _dns;
        private readonly IHistoryService _hist;

        public MainViewModel(
            INetworkService net,
            IUriService     uri,
            IPingService    ping,
            IDnsService     dns,
            IHistoryService hist)
        {
            _net  = net;  _uri  = uri;
            _ping = ping; _dns  = dns;
            _hist = hist;

            RefreshCommand = new RelayCommand(async _ => await LoadInterfaces());
            AnalyzeCommand = new RelayCommand(async _ => await Analyze(), _ => !string.IsNullOrWhiteSpace(UrlInput));

            _ = LoadInterfaces();
        }

        private async Task LoadInterfaces()
        {
            Interfaces.Clear();
            foreach (var ni in await _net.GetNetworkInterfacesAsync())
                Interfaces.Add(ni);
        }

        private async Task Analyze()
        {
            // 1) Разбор URL
            try
            {
                UriResult = _uri.Analyze(UrlInput.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при разборе URL:\n{ex.Message}",
                    "URL Analysis",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Пинг и DNS
            PingResultInfo ping;
            DnsInfo dns;
            try
            {
                ping = await _ping.PingAsync(UriResult.Host);
                dns  = await _dns.GetDnsInfoAsync(UriResult.Host);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Сетевая ошибка:\n{ex.Message}",
                    "Network Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            // 4) История
            var item = new UrlHistoryItem
            {
                CheckedAt   = DateTime.Now,
                Url         = UrlInput,
                IsReachable = ping.Success
            };
            _hist.Add(item);
            UrlHistory.Add(item);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string n) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    
    }
    
}