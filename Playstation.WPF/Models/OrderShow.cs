using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.Models
{
   public class OrderShow
    {
        public int Id { get; set; }
        public string DeviceTitle { get; set; }
        public string OrderTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Amount { get; set; }

        
    }
}
