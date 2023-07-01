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


        public string ErrorDescription { get; }

    }
}
