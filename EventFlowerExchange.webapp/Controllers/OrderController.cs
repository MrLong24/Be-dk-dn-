using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowerExchange.WebApp.Controllers
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

        // Lấy danh sách tất cả đơn hàng
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving orders.", error = ex.Message });
            }
        }

        // Lấy thông tin của một đơn hàng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                    return NotFound(new { message = "Order not found" });

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the order.", error = ex.Message });
            }
        }

        // Tạo đơn hàng mới
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                    return BadRequest(new { message = "Invalid order data." });

                await _orderService.AddOrderAsync(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the order.", error = ex.Message });
            }
        }

        // Cập nhật thông tin đơn hàng
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            try
            {
                if (order == null || order.Id != id)
                    return BadRequest(new { message = "Invalid order data or ID mismatch." });

                await _orderService.UpdateOrderAsync(order);
                return Ok(new { message = "Order updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the order.", error = ex.Message });
            }
        }

        // Xác nhận đơn hàng
        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                    return NotFound(new { message = "Order not found" });

                await _orderService.ConfirmOrderAsync(id);
                return Ok(new { message = "Order confirmed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while confirming the order.", error = ex.Message });
            }
        }
    }
}
