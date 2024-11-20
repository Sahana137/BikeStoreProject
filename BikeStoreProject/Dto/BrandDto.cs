namespace BikeStoreProject.Dto
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
    }

    public class BrandCreateDto
    {
        public string BrandName { get; set; } = null!;
    }
}
