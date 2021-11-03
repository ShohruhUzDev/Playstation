using Microsoft.EntityFrameworkCore;
using Playstation.Domain.Context;
using Playstation.Domain.Interfaces;
using Playstation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.Domain.Services
{
   public class DeviceService : IDeviceService
    {
        private readonly PlaystationDbContext _dbContext;

        public DeviceService(PlaystationDbContext dbContext)
        {
            _dbContext = dbContext;
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
            _dbContext.Update(device);
            await _dbContext.SaveChangesAsync();
        }
    }
}
