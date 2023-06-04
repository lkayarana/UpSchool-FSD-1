using Domain.Services;

namespace Wasm.Services
{
    public class UrlHelperService : IUrlHelperService
    {
        public string SignalRUrl { get; }

        public UrlHelperService(string signalRUrl)
        {
            SignalRUrl = signalRUrl;
        }
    }
}
