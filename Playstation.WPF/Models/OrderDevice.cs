using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.Models
{
   public class OrderDevice
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DevieIpAdress { get; set; }

        public string OrderTarrif { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public bool Closed { get; set; }


    }
}
