﻿using Microsoft.EntityFrameworkCore;
using Playstation.WPF.Context;
using Playstation.WPF.Interfaces;
using Playstation.WPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.Services
{
   public class DeviceService : IDeviceService
    {
        private readonly PlaystationDbContext _dbContext;

        public DeviceService()
        {
            _dbContext = new PlaystationDbContext();
        }
        public async Task<Device> CreateDevice(Device device)
        {
            _dbContext.Add(device);
            await _dbContext.SaveChangesAsync();
            return device;
        }

        public async Task DeleteDevice(int id)
        {
            var device = await _dbContext.Devices.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Remove(device);
        await _dbContext.SaveChangesAsync();
        
        }

        public async Task<Device> GetByIdDevice(int id)
        {
            var device = await _dbContext.Devices.FirstOrDefaultAsync(x => x.Id == id);
            return device;
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            var devices = await _dbContext.Devices.ToListAsync();
            return devices;
        }

        public async Task UpdateDevice(Device device)
        {
            using(var dbcontext=new PlaystationDbContext())
            {
                
                dbcontext.Entry(device).Property(p => p.Title).IsModified = true;
                dbcontext.Entry(device).Property(p => p.IpAddress).IsModified = true;

                //_dbContext.Entry(device).State = EntityState.Modified;
               // _dbContext.Update(device);
                await dbcontext.SaveChangesAsync();
            }
           
        }
    }
}
