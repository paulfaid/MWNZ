using CompaniesApi.Exceptions;
using CompaniesApi.Models;
using CompaniesApi.Services;
using System.Xml;

namespace CompaniesApi.RequestHandlers
{
    public interface ICompaniesRequestHandler
    {
        Task<CompanyResponse> GetCompany(int requestId);
    }

    public class CompaniesRequestHandler : ICompaniesRequestHandler
    {
        private readonly ILogger<CompaniesRequestHandler> _logger;
        private readonly IBackendService _backendService;

        public CompaniesRequestHandler(ILogger<CompaniesRequestHandler> logger, IBackendService backendService)
        {
            _logger = logger;
            _backendService = backendService;
        }

        public async Task<CompanyResponse> GetCompany(int requestId)
        {
            string companyXml = await _backendService.GetCompanyXml(requestId);

            var companyResponse = ParseXml(requestId, companyXml);

            return companyResponse;
        }

        private CompanyResponse ParseXml(int requestId, string companyXml)
        {
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(companyXml);
            }
            catch (XmlException ex)
            {
                _logger.LogError($"Invalid response from the backend, invalid xml, requestId: {requestId}, response: {companyXml}", ex);
                throw new ApiException("invalid_response", "The response from the backend service was invalid");
            }

            var root = doc.DocumentElement;
            if (root == null)
            {
                _logger.LogError($"Invalid response from the backend, xml root is null, requestId: {requestId}, response: {companyXml}");
                throw new ApiException("invalid_response", "The response from the backend service was invalid");
            }

            return new CompanyResponse
            {
                Id = ParseInt(requestId, root, "id"),
                Name = ParseString(requestId, root, "name"),
                Description = ParseString(requestId, root, "description")
            };
        }

        private int ParseInt(int requestId, XmlElement root, string xpath)
        {
            var value = ParseString(requestId, root, xpath);
            if (int.TryParse(value, out var intValue))
            {
                return intValue;
            }
            _logger.LogError($"Invalid response from the backend, requestId: {requestId}, element: {xpath}, value: {value}");
            throw new ApiException("invalid_response", "The response from the backend service was invalid");
        }


        private string ParseString(int requestId, XmlElement root, string xpath)
        {
            var value = root.SelectSingleNode(xpath)?.InnerText;
            if (value != null)
            {
                return value;
            }
            _logger.LogError($"Invalid response from the backend, requestId: {requestId}, element: {xpath}, value: null");
            throw new ApiException("invalid_response", "The response from the backend service was invalid");
        }
    }
}
