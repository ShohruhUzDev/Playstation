using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.Models
{
    public class Order
    {
      
       public int Id { get; set; }
       public DateTime StartTime { get; set; }
       public DateTime EndTime { get; set; }
       public int Amount { get; set; }
       public bool Closed { get; set; }
       public int DeviceId { get; set; }
       public int Minute { get; set; }
       public Device Device { get; set; }
       public Tarrif Tarrif { get; set; }
       public int TarrifId { get; set; }





    }


}
