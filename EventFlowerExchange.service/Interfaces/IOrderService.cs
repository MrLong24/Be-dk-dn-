using EventFlowerExchange.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Interfaces
{
    public interface IOrderService
    {
        // Lấy danh sách tất cả đơn hàng
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        // Lấy thông tin của một đơn hàng theo ID
        Task<Order> GetOrderByIdAsync(int id);

        // Thêm mới một đơn hàng
        Task AddOrderAsync(Order order);

        // Cập nhật thông tin đơn hàng
        Task UpdateOrderAsync(Order order);

        // Xác nhận đơn hàng (thay đổi trạng thái đơn hàng)
        Task ConfirmOrderAsync(int id);
    }
}
