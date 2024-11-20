using System.ComponentModel.DataAnnotations;

namespace BikeStoreApp.DTOs
{
    public class UpdateCustomerDto
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? ZipCode { get; set; }
    }
}
