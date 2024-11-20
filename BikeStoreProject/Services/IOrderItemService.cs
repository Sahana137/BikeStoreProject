using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BikeStoreProject.Dto;

namespace BikeStoreApp.Services
{
    public interface IOrderItemService
    {
        Task<ResponseOrderItemDto> CreateOrderItem(CreateOrderItemDto orderItemDto);

        // GET /api/orderitems
        Task<List<ResponseOrderItemDto>> GetAllOrderItems();

        // PUT /api/orderitems/{orderid}
        Task<bool> UpdateOrderItem(int orderItemId, UpdateOrderItemDto orderItemDto);

        // GET /api/orderitems/{orderid}
        Task<ResponseOrderItemDto> GetOrderItemById(int orderItemId);

        // GET /api/orderdetails/{orderid}
        Task<List<ResponseOrderItemDto>> GetOrderItemsByOrderId(int orderId);
    }


}
