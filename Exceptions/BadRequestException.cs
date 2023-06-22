using FluentValidation.Results;

namespace ApiPersonasDoc.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException(ValidationResult validationResult)
        {
            ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
            }

            if (ValidationErrors.Any())
            {
                var errorMessage = string.Join(", ", ValidationErrors);
                Data.Add("ValidationErrors", errorMessage);
            }
        }

        public List<string> ValidationErrors { get; set; }
    }
}
