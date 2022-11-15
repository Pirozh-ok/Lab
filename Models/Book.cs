using BookAPI.Commons;

namespace BookAPI.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Title { get; set; }
        public string Author { get; set; }
        public int CountPages { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }

        public ValidationResult Validate()
        {
            if (string.IsNullOrEmpty(Title))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Error = "Incorrect title!"
                };
            }

            if (string.IsNullOrEmpty(Author))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Error = "Incorrect author!"
                };
            }

            if (string.IsNullOrEmpty(Publisher))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Error = "Incorrect publisher!"
                };
            }

            if (CountPages < 1)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Error = "Incorrect count pages!"
                };
            }

            if (Price < 1)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Error = "Incorrect price!"
                };
            }

            return new ValidationResult
            {
                IsValid = true,
                Error = null
            };
        }
    }
}
