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
    /// Interaction logic for CreateOrderView.xaml
    /// </summary>
    public partial class CreateOrderView : Window
    {
        ITarrifService tarrifService = new TarrifService();
        IDeviceService deviceService = new DeviceService();
        IOrderService orderService = new OrderService();
        List<TarrifForCBX> tarrifFors = new List<TarrifForCBX>();
        public Button createbtn;
        public int id;

        public HomeControl HomeControl { get; }

        public CreateOrderView(int id,Button button, HomeControl homeControl)
        {
            InitializeComponent();
            this.id = id;
            createbtn = button;
            HomeControl = homeControl;
        }

        private void tarrif_btn_Click(object sender, RoutedEventArgs e)
        {
            time_grid.Visibility = Visibility.Visible;
            time_txt.Visibility = Visibility.Visible;
        }

        private void vip_btn_Click(object sender, RoutedEventArgs e)
        {
            time_grid.Visibility = Visibility.Visible;
            time_txt.Visibility = Visibility.Hidden;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            time_grid.Visibility = Visibility.Hidden;



            var tarrifs = await tarrifService.GetTarrifs();
            foreach(var i in tarrifs)
            {
                tarrifFors.Add(new TarrifForCBX()
                {
                    Id = i.Id,
                    Name = i.Title
                });
            }
            tarrif_cbx.ItemsSource = tarrifFors;
            tarrif_cbx.DisplayMemberPath = "Name";
            tarrif_cbx.SelectedValuePath = "Id";
        }

        private async void Save_btn_Click(object sender, RoutedEventArgs e)
        {

            var device = await deviceService.GetByIdDevice(id);

            int tarrifid;
            bool b=int.TryParse(tarrif_cbx.SelectedValue.ToString(), out tarrifid);

            var tarrif = await tarrifService.GetByIdTarrif(tarrifid);

            int starthour = DateTime.Now.Hour;
            int startminute = DateTime.Now.Minute;
            string datestring = "";

            int amountminute = Convert.ToInt32(time_txt.Text);

            int endhour = amountminute / 60;
            int endminute = amountminute % 60;

            if(startminute+endminute>=60)
            {
                endhour = endhour + starthour + 1;
                endminute = (startminute + endminute) % 60;

              

                if (endminute<10)
                {
                    datestring = endhour.ToString() + ":" +"0"+ endminute.ToString();
                }
                else
                {
                    datestring = endhour.ToString() + ":" +  endminute.ToString();
                }
            }
            else
            {
                endhour = endhour + starthour ;
                endminute = (startminute + endminute) % 60;
                if (endminute < 10)
                {
                    datestring = endhour.ToString() + ":" + "0" + endminute.ToString();
                }
                else
                {
                    datestring = endhour.ToString() + ":" + endminute.ToString();
                }
            }
            double amount = (Convert.ToDouble(time_txt.Text) / 60) * tarrif.Amount;
          
            
          
        

            var order = new Order()
            {
                DeviceId = id,
              
                StartTime = DateTime.Now,
                TarrifId = tarrifid,
               
                EndTime = DateTime.ParseExact(datestring, "HH:mm", null),
                Amount = amount,
                Closed=false
              
                
            };


           var neworder= await orderService.CreateOrder(order);


            var homeorder = new OrderDevice()
            {
               Id=device.Id,
               Title=device.Title,
               StartTime=neworder.StartTime,
               EndTime=neworder.EndTime,
               OrderTarrif=tarrif.Title
            };
            var newordersedit = HomeControl.orders.ToList();
              
           
            foreach(var i in newordersedit)
            {
                if(i.Id==id)
                {
                    i.StartTime = neworder.StartTime;
                    i.EndTime = neworder.EndTime;
                    i.Title = device.Title;
                    i.OrderTarrif = tarrif.Title;

                }
            }





            HomeControl.home_datagrid.ItemsSource= newordersedit;

            var a = HomeControl.home_datagrid.row;

            this.Close();
            createbtn.IsEnabled = false;




        }
        
        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
