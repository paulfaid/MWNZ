using CompaniesApi.Exceptions;

namespace CompaniesApi.Services
{
    public interface IBackendService
    {
        Task<string> GetCompanyXml(int requestId);
    }

    public class BackendService : IBackendService
    {
        private readonly ILogger<BackendService> _logger;
        private readonly HttpClient _httpClient;

        public BackendService(ILogger<BackendService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<string> GetCompanyXml(int requestId)
        {
            var path = $"{requestId}.xml";
            try
            {
                return await _httpClient.GetStringAsync(path);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogInformation(ex.Message);  // We don't actually want to swamp the logs with lots of failed requests,
                                                     // but we do need to log something somewhere to catch if
                                                     // the httpclient is misconfigured or the backend service is down.
                                                     // Although we should add a health check to cover some of these scenarios.

                throw new ApiException("not_found", "The requested resource was not found on the backend service");
            }
        }
    }
}
