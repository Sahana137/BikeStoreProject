using BikeStoreApp.Services;
using BikeStoreProject.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        // POST /api/orderitems
        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemDto orderItemDto)
        {
            var createdOrderItem = await _orderItemService.CreateOrderItem(orderItemDto);
            return CreatedAtAction(nameof(GetOrderItemById), new { orderItemId = createdOrderItem.OrderId }, createdOrderItem);
        }

        // GET /api/orderitems
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrderItems();
            return Ok(orderItems);
        }

        // PUT /api/orderitems/{orderid}
        [HttpPut("{orderItemId}")]
        public async Task<IActionResult> UpdateOrderItem(int orderItemId, [FromBody] UpdateOrderItemDto orderItemDto)
        {
            var result = await _orderItemService.UpdateOrderItem(orderItemId, orderItemDto);
            if (result) return NoContent();
            return NotFound();
        }

        // GET /api/orderitems/{orderid}
        [HttpGet("{orderItemId}")]
        public async Task<IActionResult> GetOrderItemById(int orderItemId)
        {
            var orderItem = await _orderItemService.GetOrderItemById(orderItemId);
            if (orderItem == null) return NotFound();
            return Ok(orderItem);
        }

        // GET /api/orderdetails/{orderid}
        [HttpGet("orderdetails/{orderId}")]
        public async Task<IActionResult> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = await _orderItemService.GetOrderItemsByOrderId(orderId);
            return Ok(orderItems);
        }
    }

}
