using Playstation.WPF.Controls;
using Playstation.WPF.Interfaces;
using Playstation.WPF.Models;
using Playstation.WPF.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Playstation.WPF.Views
{
    /// <summary>
    /// Interaction logic for UpdateOrderView.xaml
    /// </summary>
    public partial class UpdateOrderView : Window
    {
        IOrderService orderService = new OrderService();
        IDeviceService deviceService = new DeviceService();
        public int _deviceid;

        public HomeControl HomeControl { get; }

        public UpdateOrderView(int devicid, HomeControl homeControl)
        {
            _deviceid = devicid;
            HomeControl = homeControl;
            InitializeComponent();
        }

        private async void Save_btn_Click(object sender, RoutedEventArgs e)
        {

            var orders = await orderService.GetOrders();
            var neworders = orders.OrderByDescending(x => x.StartTime).FirstOrDefault(i => i.DeviceId == _deviceid);

           
            if(int.TryParse(time_txt.Text, out int amountminute)&&neworders is not null)
            {

                amountminute = neworders.Minute + amountminute;

                int endhour = amountminute / 60;
                int endminute = amountminute % 60;
                string datestring = "";

                if (neworders.StartTime.Minute + endminute >= 60)
                {
                    endhour = endhour + neworders.StartTime.Hour + 1;
                    endminute = (neworders.StartTime.Minute + endminute) % 60;



                    if (endminute < 10)
                    {
                        datestring = endhour.ToString() + ":" + "0" + endminute.ToString();
                    }
                    else
                    {
                        datestring = endhour.ToString() + ":" + endminute.ToString();
                    }
                }
                else
                {
                    endhour = endhour + neworders.StartTime.Hour;
                    endminute = (neworders.StartTime.Minute + endminute) % 60;
                    if (endminute < 10)
                    {
                        datestring = endhour.ToString() + ":" + "0" + endminute.ToString();
                    }
                    else
                    {
                        datestring = endhour.ToString() + ":" + endminute.ToString();
                    }
                }
                int amount = (int)((Convert.ToDouble(amountminute) / 60) * neworders.Tarrif.Amount);


              

               
                if (neworders.Tarrif.TarrifType == TarrifType.Simple)
                {
                   

                    var updateorder = new Order()
                    {
                        Id = neworders.Id,
                        StartTime=neworders.StartTime,
                        EndTime = DateTime.ParseExact(datestring, "HH:mm", null),
                        Minute=amountminute,
                        Amount=amount,
                        TarrifId=neworders.TarrifId,
                        Tarrif=neworders.Tarrif,
                        Device=neworders.Device,
                        DeviceId=neworders.DeviceId,
                        
                        Closed = false

                    };


                    await orderService.UpdateOrder(updateorder);

                    var newordersedit = HomeControl.orders.ToList();


                    foreach (var i in newordersedit)
                    {
                        if (i.Id == _deviceid)
                        {
                            i.StartTime = updateorder.StartTime;
                            i.EndTime = updateorder.EndTime;
                            i.OrderTarrif = updateorder.Tarrif.Title;
                           
                            i.Title = updateorder.Device.Title;

                        }
                    }
                    HomeControl.home_datagrid.ItemsSource = newordersedit;

                }
            }
            else
            {
                MessageBox.Show("Данные были введены неверно");
            }


            this.Close();

        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
