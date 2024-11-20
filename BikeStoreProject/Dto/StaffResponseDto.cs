namespace BikeStoreProject.Dto
{
    public class StaffResponseDto
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty; // Assuming this is joined from the store table
        public int? ManagerId { get; set; }
    }
}
