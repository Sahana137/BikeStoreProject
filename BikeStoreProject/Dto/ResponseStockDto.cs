namespace BikeStoreProject.Dto
{
    public class ResponseStockDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int? Quantity { get; set; }
    }
}
