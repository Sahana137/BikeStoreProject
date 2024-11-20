
using BikeStoreProject.Dto;

namespace BikeStoreApp.Services
{
    public interface IOrderService
    {
        // POST /api/orders
        Task<ResponseOrderDto> CreateOrder(CreateOrderDto orderDto);

        // GET /api/orders
        Task<List<ResponseOrderDto>> GetAllOrders();

        // PUT /api/orders/edit/{orderid}
        Task<bool> UpdateOrder(int orderId, UpdateOrderDto orderDto);

        // PATCH /api/orders/edit/{orderid}
        Task<bool> PatchOrder(int orderId, UpdateOrderDto orderDto);

        // GET /api/orders/customerid/{customerid}
        Task<List<ResponseOrderDto>> GetOrdersByCustomerId(int customerId);

        // GET /api/orders/custometname/{customername}
        Task<List<ResponseOrderDto>> GetOrdersByCustomerName(string customerName);

        // GET /api/orders/orderdate/{orderdate}
        Task<List<ResponseOrderDto>> GetOrdersByOrderDate(string orderDate);

        // GET /api/orders/status/{status}
        Task<List<ResponseOrderDto>> GetOrdersByStatus(byte status);

        // GET /api/orders/numberoforderbydate
        Task<Dictionary<DateOnly, int>> GetOrderCountsByDate();

        // GET /api/orders/maxiumorderplaceonparticulardate
        Task<DateOnly?> GetDateWithMaximumOrders();

    }
}

