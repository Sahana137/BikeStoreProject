namespace BikeStoreProject.Dto
{
    public class CreateStaffDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public int? ManagerId { get; set; }
    }
}
