using System.ComponentModel.DataAnnotations;

namespace Student_13WebApiProject.Data
{
    public class ResponseCustomerDto
    {
        [Key]
        public int CustomerId { get; set; }
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
