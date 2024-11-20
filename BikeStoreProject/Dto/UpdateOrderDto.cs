using System.Text.Json.Serialization;

namespace BikeStoreProject.Dto
{
    public class UpdateOrderDto
    {
        public byte? OrderStatus { get; set; }
        [JsonPropertyName("orderDate")]
        public string OrderDateString
        {
            get => OrderDate.ToString("yyyy-MM-dd");
            set => OrderDate = DateOnly.Parse(value);
        }

        [JsonIgnore]
        public DateOnly OrderDate { get; private set; }

        [JsonPropertyName("requiredDate")]
        public string RequiredDateString
        {
            get => RequiredDate.ToString("yyyy-MM-dd");
            set => RequiredDate = DateOnly.Parse(value);
        }

        [JsonIgnore]
        public DateOnly RequiredDate { get; private set; }
        public int? StaffId { get; set; }
        public List<UpdateOrderItemDto>? OrderItems { get; set; }
    }
}
