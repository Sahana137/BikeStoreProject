using System.Text.Json.Serialization;

namespace BikeStoreProject.Dto
{
    public class ResponseOrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } // Optional: Combine FirstName and LastName
        public byte OrderStatus { get; set; }
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
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public List<ResponseOrderItemDto> OrderItems { get; set; }
    }
}
