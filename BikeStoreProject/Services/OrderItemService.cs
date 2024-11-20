using AutoMapper;
using BikeStoreApp.Models;
using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreApp.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly BikeStoreAppContext _context;
        private IMapper _mapper;
        public OrderItemService(BikeStoreAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseOrderItemDto> CreateOrderItem(CreateOrderItemDto orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<ResponseOrderItemDto>(orderItem);
            //throw new NotImplementedException();
        }

        public async Task<List<ResponseOrderItemDto>> GetAllOrderItems()
        {
            var orderItems = await _context.OrderItems
            .Include(oi => oi.Product) // Ensure we fetch related product data
            .ToListAsync();

            return _mapper.Map<List<ResponseOrderItemDto>>(orderItems);
            //throw new NotImplementedException();
        }

        public async Task<ResponseOrderItemDto> GetOrderItemById(int orderItemId)
        {
            var orderItem = await _context.OrderItems
            .Include(oi => oi.Product)
            .FirstOrDefaultAsync(oi => oi.OrderId == orderItemId);

            return _mapper.Map<ResponseOrderItemDto>(orderItem);
            //throw new NotImplementedException();
        }

        public async Task<List<ResponseOrderItemDto>> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = await _context.OrderItems
           .Where(oi => oi.OrderId == orderId)
            .Include(oi => oi.Product)
            .ToListAsync();

            return _mapper.Map<List<ResponseOrderItemDto>>(orderItems);
            //throw new NotImplementedException();
        }

        public async Task<bool> UpdateOrderItem(int orderItemId, UpdateOrderItemDto orderItemDto)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null) return false;

            _mapper.Map(orderItemDto, orderItem);
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();

            return true;
            //throw new NotImplementedException();
        }
    }
}

