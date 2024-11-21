namespace BikeStoreProject.Dto
{
    public class SalesResponseDto
    {
        public int OrderId { get; set; }
        public DateOnly OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public byte OrderStatus { get; set; }
    }
}
