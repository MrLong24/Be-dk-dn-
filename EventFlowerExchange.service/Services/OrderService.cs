using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _orderRepository.GetAllOrdersAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in OrderService while retrieving all orders", ex);
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            try
            {
                return await _orderRepository.GetOrderByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in OrderService while retrieving order by ID", ex);
            }
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            try
            {
                await _orderRepository.AddOrderAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in OrderService while creating order", ex);
            }
        }


        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.UpdateOrderAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in OrderService while updating order", ex);
            }
        }

        public async Task ConfirmOrderAsync(int id)
        {
            try
            {
                await _orderRepository.ConfirmOrderAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in OrderService while confirming order", ex);
            }
        }
    }
}
