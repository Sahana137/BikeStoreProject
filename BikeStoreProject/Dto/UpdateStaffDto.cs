namespace BikeStoreProject.Dto
{
    public class UpdateStaffDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public byte? Active { get; set; }
        public int? StoreId { get; set; }
        public int? ManagerId { get; set; }
    }
}
