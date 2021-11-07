using Playstation.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.Interfaces
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetDevices();
        Task<Device> GetByIdDevice(int id);
        Task<Device> CreateDevice(Device device);
        Task UpdateDevice(Device device);
        Task DeleteDevice(int id);
    }
}
