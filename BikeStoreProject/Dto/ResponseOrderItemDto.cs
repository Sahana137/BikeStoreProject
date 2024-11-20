namespace BikeStoreProject.Dto
{
    public class ResponseOrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public float Discount { get; set; }
        public object OrderId { get; internal set; }
    }
}
