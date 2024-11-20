using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BikeStoreProject.Dto
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public byte OrderStatus { get; set; }


        [Required]
        [JsonPropertyName("orderDate")]
        public string OrderDateString
        {
            get => OrderDate.ToString("yyyy-MM-dd");
            set => OrderDate = DateOnly.Parse(value);
        }

        [JsonIgnore]
        public DateOnly OrderDate { get; private set; }

        [Required]
        [JsonPropertyName("requiredDate")]
        public string RequiredDateString
        {
            get => RequiredDate.ToString("yyyy-MM-dd");
            set => RequiredDate = DateOnly.Parse(value);
        }

        [JsonIgnore]
        public DateOnly RequiredDate { get; private set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        public int StaffId { get; set; }

        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
