﻿using Microsoft.EntityFrameworkCore;
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
        public OrderService()
        {
            
        }
        public async Task<Order> CreateOrder(Order order)
        {
            using(var dbContext=new PlaystationDbContext())
            {
                dbContext.Add(order);
                await dbContext.SaveChangesAsync();
                return order;
            }
          

        }

        public async Task DeleteOrder(int id)
        {
            using(var dbContext=new PlaystationDbContext())
            {
                var order = dbContext.Orders.FindAsync(id);
                dbContext.Remove(order);
                await dbContext.SaveChangesAsync();
            }
           

        }

        public Task<Order> GetByIdOrder(int id)
        {
            using (var dbContext=new PlaystationDbContext())
            {
                var order = dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
                return order;
            }
           
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            using(var dbContext=new PlaystationDbContext())
            {
                var orderes = await dbContext.Orders.ToListAsync();
                return orderes;
            }
          
        }

        public async Task UpdateOrder(Order order)
        {
            using(var dbContext=new PlaystationDbContext())
            {
                dbContext.Entry(order).Property(p => p.StartTime).IsModified = true;
                dbContext.Entry(order).Property(p => p.EndTime).IsModified = true;
                dbContext.Entry(order).Property(p => p.Amount).IsModified = true;
              



                await dbContext.SaveChangesAsync();
            }
            
        }
    }
}