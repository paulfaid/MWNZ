using System.Text.Json.Serialization;

namespace CompaniesApi.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string error, string errorDescription)
        {
            Error = error;
            ErrorDescription = errorDescription;
        }

        public string Error { get; }

        [JsonPropertyName("error_description")]  //This should be done using a JsonSerializerOptions.PropertyNamingPolicy so it applies to all json serialization
        public string ErrorDescription { get; }

    }
}
