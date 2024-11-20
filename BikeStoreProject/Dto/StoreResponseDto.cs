namespace BikeStoreProject.Dto
{
    public class StoreResponseDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public int CustomerCount { get; set; } // Added CustomerCount property

    }
}
