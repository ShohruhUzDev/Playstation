using Microsoft.EntityFrameworkCore;
using Playstation.WPF.Context;
using Playstation.WPF.Interfaces;
using Playstation.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.Services
{
    public class OrderService : IOrderService
    {
        private readonly PlaystationDbContext _playstationDbContext;
        public OrderService(PlaystationDbContext playstationDbContext)
        {
            _playstationDbContext = playstationDbContext;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            _playstationDbContext.Add(order);
            await _playstationDbContext.SaveChangesAsync();
            return order;

        }

        public async Task DeleteOrder(int id)
        {
            var order = _playstationDbContext.Orders.FindAsync(id);
            _playstationDbContext.Remove(order);
            await _playstationDbContext.SaveChangesAsync();

        }

        public Task<Order> GetByIdOrder(int id)
        {
            var order = _playstationDbContext.Orders.FirstOrDefaultAsync(x=>x.Id==id);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orderes = await _playstationDbContext.Orders.ToListAsync();
            return orderes;
        }

        public async Task UpdateOrder(Order order)
        {
            _playstationDbContext.Update(order);
            await _playstationDbContext.SaveChangesAsync();
        }
    }
}
