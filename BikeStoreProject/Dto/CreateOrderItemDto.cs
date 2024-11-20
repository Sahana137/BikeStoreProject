using System.ComponentModel.DataAnnotations;

namespace BikeStoreProject.Dto
{
    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

        [Required]
        public float Discount { get; set; }
    }
}
