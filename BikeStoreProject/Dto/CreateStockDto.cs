using System.ComponentModel.DataAnnotations;

namespace BikeStoreProject.Dto
{
    public class CreateStockDto
    {
        
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
