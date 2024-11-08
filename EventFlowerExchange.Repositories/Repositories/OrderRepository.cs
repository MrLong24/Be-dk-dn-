using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EventFlowerExchangeContext _context;

        public OrderRepository(EventFlowerExchangeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving orders", ex);
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            try
            {
                return await _context.Orders.FindAsync(id) ?? throw new KeyNotFoundException("Order not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving order by ID", ex);
            }
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding order", ex);
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating order", ex);
            }
        }

        public async Task ConfirmOrderAsync(int id)
        {
            try
            {
                var order = await GetOrderByIdAsync(id);
                if (order == null) throw new KeyNotFoundException("Order not found");

                order.Status = "Confirmed"; // Assuming "Confirmed" is a valid status
                await UpdateOrderAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Error confirming order", ex);
            }
        }
    }
}
