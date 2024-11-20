using BikeStoreApp.Services;
using BikeStoreProject.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            var createdOrder = await _orderService.CreateOrder(orderDto);
            return CreatedAtAction(nameof(GetAllOrders), new { orderId = createdOrder.OrderId }, createdOrder);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpPut("edit/{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderDto orderDto)
        {
            var result = await _orderService.UpdateOrder(orderId, orderDto);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpPatch("edit/{orderId}")]
        public async Task<IActionResult> PatchOrder(int orderId, [FromBody] UpdateOrderDto orderDto)
        {
            var result = await _orderService.PatchOrder(orderId, orderDto);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpGet("customerid/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerId(customerId);
            return Ok(orders);
        }

        [HttpGet("customername/{customerName}")]
        public async Task<IActionResult> GetOrdersByCustomerName(string customerName)
        {
            var orders = await _orderService.GetOrdersByCustomerName(customerName);
            return Ok(orders);
        }

        [HttpGet("orderdate/{orderDate}")]
        public async Task<IActionResult> GetOrdersByOrderDate(string orderDate)
        {
            var orders = await _orderService.GetOrdersByOrderDate(orderDate);
            return Ok(orders);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(byte status)
        {
            var orders = await _orderService.GetOrdersByStatus(status);
            return Ok(orders);
        }

        [HttpGet("numberoforderbydate")]
        public async Task<IActionResult> GetOrderCountsByDate()
        {
            var orderCounts = await _orderService.GetOrderCountsByDate();
            return Ok(orderCounts);
        }

        [HttpGet("maximumorderplacedate")]
        public async Task<IActionResult> GetDateWithMaximumOrders()
        {
            var date = await _orderService.GetDateWithMaximumOrders();
            return Ok(date);
        }
    }
}

