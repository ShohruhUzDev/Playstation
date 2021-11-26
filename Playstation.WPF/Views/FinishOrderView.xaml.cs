using Playstation.WPF.AdbManager;
using Playstation.WPF.Controls;
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
       
        public int _deviceid;

        public HomeControl HomeControl { get; }

        public FinishOrderView( int devicid, HomeControl homeControl)
        {
            _deviceid = devicid;
            HomeControl = homeControl;
           
            InitializeComponent();
        }

        private async void Ok_btn_Click(object sender, RoutedEventArgs e)
        {

            var orders = await orderService.GetOrders();
            var neworders = orders.OrderByDescending(x => x.StartTime).FirstOrDefault(i => i.DeviceId == _deviceid);

            if(neworders is null)
            {
                this.Close();
            }
            else
            {
                int minute;
                if (neworders.Tarrif.TarrifType == TarrifType.Simple)
                {


                    int amount = (int)((Convert.ToDouble(neworders.Minute) / 60) * neworders.Tarrif.Amount);

                    var updateorder = new Order()
                    {
                        Id = neworders.Id,
                        Amount = neworders.Amount,
                        Closed = false

                    };


                    await orderService.UpdateOrder(updateorder);

                }


                if (neworders.Tarrif.TarrifType == TarrifType.Vip)
                {

                    var nowdatetime = DateTime.Now.Hour;
                    if (DateTime.Now.Minute >= neworders.StartTime.Minute)
                    {
                        minute = DateTime.Now.Minute - neworders.StartTime.Minute;
                    }
                    else
                    {
                        minute = DateTime.Now.Minute + 60 - neworders.StartTime.Minute;
                        nowdatetime = DateTime.Now.Hour - 1;
                    }

                    //hour
                    if (nowdatetime > neworders.StartTime.Hour)
                    {
                        minute = (nowdatetime - neworders.StartTime.Hour) * 60 + minute;
                    }

                    int amount = (int)((Convert.ToDouble(minute) / 60) * neworders.Tarrif.Amount);


                    var updateorder = new Order()
                    {
                        Id = neworders.Id,
                        StartTime = neworders.StartTime,
                        Amount = amount,
                        TarrifId = neworders.TarrifId,
                        DeviceId = neworders.DeviceId,
                        EndTime = DateTime.Now,
                        Minute = minute,
                        Closed = false

                    };


                    await orderService.UpdateOrder(updateorder);

                }


                var newordersedit = HomeControl.orders.ToList();

                foreach (var i in newordersedit)
                {

                    if (i.Id == _deviceid)
                    {
                        i.StartTime = DateTime.Now;
                        i.OrderTarrif = null;
                        i.EndTime = DateTime.ParseExact("00:00", "HH:mm", null);

                    }
                }
                HomeControl.home_datagrid.ItemsSource = newordersedit;
                List<MyDevice> myDevices = new List<MyDevice>();


                MyAdbManager myAdb = new MyAdbManager();
                myDevices = myAdb.GetDevices();

                     
               foreach (var item in myDevices)
                {
                      if (item.Serial == neworders.Device.IpAddress + ":5555")
                                {

                                    MyDevice myDevice = new MyDevice(item.Serial);
                                    myDevice.ExecuteShellCommand($"-s {item.Serial} reboot");
                                    // myDevice.ExecuteShellCommand($"-s {item.Serial} shell input keyevent 26");


                                }          
               }

                        
                    

                


            }

            

            // creaeButton.IsEnabled = true;
            this.Close();
           
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            var orders = await orderService.GetOrders();
            var neworders= orders.OrderByDescending(x=>x.StartTime).FirstOrDefault(i=>i.DeviceId== _deviceid);



            if (neworders is not null&& neworders.Closed)
            {
                if (neworders.Tarrif.TarrifType == TarrifType.Simple)
                {
                    titleDevice_txt.Text = neworders.Device.Title;
                    amount_txt.Text = neworders.Amount.ToString();
                    totalminutes_txt.Text = neworders.Minute.ToString();
                }

                if (neworders.Tarrif.TarrifType == TarrifType.Vip)
                {
                    int minute;

                    //minute
                    if (DateTime.Now.Minute >= neworders.StartTime.Minute)
                    {
                        minute = DateTime.Now.Minute - neworders.StartTime.Minute;
                    }
                    else
                    {
                        minute = DateTime.Now.Minute + 60 - neworders.StartTime.Minute;
                    }

                    //hour
                    if (DateTime.Now.Hour > neworders.StartTime.Hour)
                    {
                        minute = (DateTime.Now.Hour - neworders.StartTime.Hour) * 60 + minute;
                    }

                    int amount = (int)((Convert.ToDouble(minute) / 60) * neworders.Tarrif.Amount);


                    if (neworders != null)
                    {
                        titleDevice_txt.Text = neworders.Device.Title;
                        amount_txt.Text = amount.ToString();
                        totalminutes_txt.Text = minute.ToString();
                    }
                }


            }

            else
            {

                titleDevice_txt.Text = "";
                amount_txt.Text = "";
                totalminutes_txt.Text = "";
            }









        

           


          

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
