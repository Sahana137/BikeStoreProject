namespace BikeStoreProject.Dto
{
    public class UpdateStockDto
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
