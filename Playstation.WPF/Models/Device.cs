﻿using System.Collections.Generic;

namespace Playstation.WPF.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IpAddress { get; set; }
        public ICollection<Order> Orders { get; set; }

    }


}
