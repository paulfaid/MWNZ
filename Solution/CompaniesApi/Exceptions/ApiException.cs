namespace CompaniesApi.Exceptions
{
    public class ApiException : Exception
    {
        public string Description { get; }

        public ApiException(string message, string description) : base(message)
        {
            Description = description;
        }
    }
}
