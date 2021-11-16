using System.Collections.Generic;

namespace Playstation.WPF.Models
{
    public class Tarrif
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Title { get; set; }
        public int TotalMinutes { get; set; }
        public TarrifType TarrifType { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
    public enum TarrifType
    {
        Vip, 
        Simple
    }


}
