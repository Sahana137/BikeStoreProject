namespace BikeStoreProject.Dto
{
    public class UpdateOrderItemDto
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? ListPrice { get; set; }
        public float? Discount { get; set; }
    }
}
