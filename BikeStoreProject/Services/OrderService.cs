using AutoMapper;
using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly BikeStoreContext _context;
        private IMapper _mapper;
        public OrderService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseOrderDto> CreateOrder(CreateOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<ResponseOrderDto>(order);
            //throw new NotImplementedException();
        }

        public async Task<List<ResponseOrderDto>> GetAllOrders()
        {
            var orders = await _context.Orders
           .Include(o => o.Customer)
           .Include(o => o.Store)
           .Include(o => o.Staff)
           .Include(o => o.OrderItems)
           .ThenInclude(oi => oi.Product)
           .ToListAsync();

            return _mapper.Map<List<ResponseOrderDto>>(orders);
            //throw new NotImplementedException();
        }

        public async Task<DateOnly?> GetDateWithMaximumOrders()
        {
            return await _context.Orders
            .GroupBy(o => o.OrderDate)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefaultAsync();
            //throw new NotImplementedException();
        }

        public async Task<Dictionary<DateOnly, int>> GetOrderCountsByDate()
        {
            return await _context.Orders
           .GroupBy(o => o.OrderDate)
           .Select(g => new { Date = g.Key, Count = g.Count() })
           .ToDictionaryAsync(g => g.Date, g => g.Count);
            //throw new NotImplementedException();
        }

        public async Task<List<ResponseOrderDto>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.Customer)
            .Include(o => o.Store)
            .Include(o => o.Staff)
            .ToListAsync();

            return _mapper.Map<List<ResponseOrderDto>>(orders);
            //throw new NotImplementedException();
        }

        public async Task<List<ResponseOrderDto>> GetOrdersByCustomerName(string customerFirstName)
        {
            var orders = await _context.Orders
            .Where(o => o.Customer.FirstName.Contains(customerFirstName))
            .Include(o => o.Customer)
            .Include(o => o.Store)
            .Include(o => o.Staff)
            .ToListAsync();

            return _mapper.Map<List<ResponseOrderDto>>(orders);
            //throw new NotImplementedException();
        }

        public async Task<List<ResponseOrderDto>> GetOrdersByOrderDate(string orderDate)
        {
            // Parse the string to DateOnly
            var parsedOrderDate = DateOnly.Parse(orderDate);

            // Fetch orders matching the parsed date
            var orders = await _context.Orders
                .Where(o => o.OrderDate == parsedOrderDate)
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .Include(o => o.Staff)
                .ToListAsync();

            // Map and return the response
            return _mapper.Map<List<ResponseOrderDto>>(orders);
        }

        public async Task<List<ResponseOrderDto>> GetOrdersByStatus(byte status)
        {
            var orders = await _context.Orders
            .Where(o => o.OrderStatus == status)
            .Include(o => o.Customer)
            .Include(o => o.Store)
            .Include(o => o.Staff)
            .ToListAsync();

            return _mapper.Map<List<ResponseOrderDto>>(orders);
            //throw new NotImplementedException();
        }

        public async Task<bool> PatchOrder(int orderId, UpdateOrderDto orderDto)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            _mapper.Map(orderDto, order);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return true;
            //throw new NotImplementedException();
        }

        public async Task<bool> UpdateOrder(int orderId, UpdateOrderDto orderDto)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            _mapper.Map(orderDto, order);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return true;
            //throw new NotImplementedException();
        }
    }
}

