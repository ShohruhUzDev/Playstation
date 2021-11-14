using Playstation.WPF.Interfaces;
using Playstation.WPF.Models;
using Playstation.WPF.Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FinishOrderView.xaml
    /// </summary>
    public partial class FinishOrderView : Window
    {
        IOrderService orderService = new OrderService();
        IDeviceService deviceService = new DeviceService();
        ITarrifService tarrifService = new TarrifService();
        public Button creaeButton;
        public int _deviceid;
        public FinishOrderView(Button button, int devicid)
        {
            _deviceid = devicid;
            creaeButton = button;
            InitializeComponent();
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            creaeButton.IsEnabled = true;
            this.Close();
           
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            var orders = await orderService.GetOrders();
            var neworders= orders.OrderByDescending(x=>x.StartTime).FirstOrDefault(i=>i.DeviceId== _deviceid);
            int minute;

            //minute
           if(neworders.EndTime.Minute>=neworders.StartTime.Minute)
            {
               minute= neworders.EndTime.Minute - neworders.StartTime.Minute;
            }
           else
            {
                minute = neworders.EndTime.Minute + 60 - neworders.StartTime.Minute;
            }

           //hour
           if(neworders.EndTime.Hour>neworders.StartTime.Hour)
            {
                minute = (neworders.EndTime.Hour - neworders.StartTime.Hour) * 60 + minute;
            }




            if(neworders != null)
            {
                titleDevice_txt.Text = neworders.Device.Title;
                amount_txt.Text = neworders.Amount.ToString();
                totalminutes_txt.Text = minute.ToString();
            }
        }
    }
}
