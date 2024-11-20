namespace BikeStoreProject.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }

    public class CategoryCreateDto
    {
        public string CategoryName { get; set; } = null!;
    }
}
