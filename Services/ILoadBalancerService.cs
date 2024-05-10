using SOA_Assignment.Services;
using Microsoft.Extensions.Options;
using SOA_Assignment.Models;

namespace SOA_Assignment.Services
{
    public interface ILoadBalancerService
    {
        Task<string> ForwardRequestAsync(HttpRequest request);
    }
    public class LoadBalancerService : ILoadBalancerService
    {
        private readonly List<BackendService> _backendServices;
        private readonly HttpClient _httpClient;
        private int _lastIndex = -1;

        public LoadBalancerService(IOptions<List<BackendService>> options)
        {
            _backendServices = options.Value;
            _httpClient = new HttpClient();
        }

        private BackendService GetNextBackendService()
        {
            lock (this)
            {
                _lastIndex = (_lastIndex + 1) % _backendServices.Count;
                return _backendServices[_lastIndex];
            }
        }

        public async Task<string> ForwardRequestAsync(HttpRequest request)
        {
            var backendService = GetNextBackendService();
            var url = $"{backendService.Url}{request.Path}{request.QueryString}";

            var message = new HttpRequestMessage(new HttpMethod(request.Method), url);

            foreach (var header in request.Headers)
            {
                if (!message.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                {
                    message.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }

            if (request.ContentLength > 0 || request.Method == "POST" || request.Method == "PUT")
            {
                message.Content = new StreamContent(request.Body);
            }

            var response = await _httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
