using CompaniesApi.Exceptions;
using CompaniesApi.Models;
using CompaniesApi.RequestHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesApi.Controllers
{
    [ApiController]
    [Route("v1/companies")]
    public class CompaniesController : Controller
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly ICompaniesRequestHandler _requestHandler;

        public CompaniesController(ILogger<CompaniesController> logger, ICompaniesRequestHandler requestHandler)
        {
            _logger = logger;
            _requestHandler = requestHandler;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var response = await _requestHandler.GetCompany(id);
                return Ok(response);
            }
            catch (ApiException ex)
            {
                return NotFound(new ErrorResponse(ex.Message, ex.Description ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(new ErrorResponse("unexpected_error", "An unexpected error occured in the Api gateway")); // don't return ex.message to the user, it may contain data we don't want to expose.
            }
        }
    }
}
