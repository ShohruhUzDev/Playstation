﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.Domain.Models
{
    public class Order
    {
       public int Id { get; set; }
       public DateTime StartTime { get; set; }
       public DateTime EndTime { get; set; }
       public double Amount { get; set; }
       public bool Closed { get; set; }
       public int DeviceId { get; set; }
       public Device Device { get; set; }




    }


}
