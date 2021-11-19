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
        public enum TarrifEnum
        {
            Vip,
            Tarrif
        }
        public TarrifEnum tarrifEnum;
        ITarrifService tarrifService = new TarrifService();
        IDeviceService deviceService = new DeviceService();
        IOrderService orderService = new OrderService();
        List<TarrifForCBX> tarrifFors = new List<TarrifForCBX>();
        
        public int id;

        public HomeControl HomeControl { get; }

        public CreateOrderView(int id, HomeControl homeControl)
        {
            InitializeComponent();
            this.id = id;
           
            HomeControl = homeControl;
        }

        private  void tarrif_btn_Click(object sender, RoutedEventArgs e)
        {
            tarrifEnum = TarrifEnum.Tarrif;
            time_grid.Visibility = Visibility.Visible;
            time_txt.Visibility = Visibility.Visible;

           
           
        }

        private  void vip_btn_Click(object sender, RoutedEventArgs e)
        {
            tarrifEnum = TarrifEnum.Vip;
            time_grid.Visibility = Visibility.Visible;
            time_txt.Visibility = Visibility.Hidden;


            
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var orders = await orderService.GetOrders();
            var iforder = orders.OrderByDescending(x => x.StartTime).FirstOrDefault(i => i.DeviceId == id);

            if(iforder is not null)
            {
                if (iforder.Closed)
                {
                    MessageBox.Show("У этого устройства есть заказ. Вы не можете создать заказ");
                    this.Close();
                }
                else
                {

                    time_grid.Visibility = Visibility.Hidden;



                    var tarrifs = await tarrifService.GetTarrifs();
                    foreach (var i in tarrifs)
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



                    //amount textblock is visible false
                    amount_txt.Visibility = Visibility.Hidden;

                    Save_btn.IsEnabled = false;
                }
            }
           else
            {
                time_grid.Visibility = Visibility.Hidden;



                var tarrifs = await tarrifService.GetTarrifs();
                foreach (var i in tarrifs)
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



                //amount textblock is visible false
                amount_txt.Visibility = Visibility.Hidden;

                Save_btn.IsEnabled = false;
            }
            


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

            Order neworder = new Order();




                Order order = new Order();
                if (tarrifEnum == TarrifEnum.Tarrif)
                {

                if (int.TryParse(time_txt.Text, out int amountminute) && tarrif_cbx.SelectedIndex != -1)
                {
                    int endhour = amountminute / 60;
                    int endminute = amountminute % 60;

                    if (startminute + endminute >= 60)
                    {
                        endhour = endhour + starthour + 1;
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
                    else
                    {
                        endhour = endhour + starthour;
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
                    int amount = (int)((Convert.ToDouble(amountminute) / 60) * tarrif.Amount);

                    order = new Order()
                    {
                        DeviceId = id,
                        StartTime = DateTime.Now,
                        TarrifId = tarrifid,
                        EndTime = DateTime.ParseExact(datestring, "HH:mm", null),
                        Amount = amount,
                        Minute=amountminute,
                        Closed = true


                    };


                   neworder = await orderService.CreateOrder(order);
                }
                else
                {
                    MessageBox.Show("Данные были введены неверно");
                    tarrif_cbx.SelectedIndex = -1;
                    time_txt.Text = "";
                }

               


               

                //home controllerdagi ordersni 
                var newordersedit = HomeControl.orders.ToList();


                foreach (var i in newordersedit)
                {
                    if (i.Id == id)
                    {
                        i.StartTime = neworder.StartTime;
                        i.EndTime = neworder.EndTime;
                        i.Title = device.Title;
                        i.OrderTarrif = tarrif.Title;

                    }
                }





                HomeControl.home_datagrid.ItemsSource = newordersedit;

            }
           


            if (tarrifEnum == TarrifEnum.Vip)
            {
                if (tarrif_cbx.SelectedIndex != -1)
                {

                    order = new Order()
                    {
                        DeviceId = id,
                        StartTime = DateTime.Now,
                        TarrifId = tarrifid,
                        Closed = true
                    };

                    neworder= await orderService.CreateOrder(order);
                }
                else
                {
                    MessageBox.Show("Данные были введены неверно");
                    tarrif_cbx.SelectedIndex = -1;
                    time_txt.Text = "";
                }

               


               


                var newordersedit = HomeControl.orders.ToList();


                foreach (var i in newordersedit)
                {
                    if (i.Id == id)
                    {
                        i.StartTime = neworder.StartTime;
                        
                        i.Title = device.Title;
                        i.OrderTarrif = tarrif.Title;

                    }
                }





                HomeControl.home_datagrid.ItemsSource = newordersedit;

            }
           
        


            this.Close();
           




        }
        
        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void time_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amount;
           if(int.TryParse(tarrif_cbx.SelectedValue.ToString(), out int tarrifid)&& int.TryParse(time_txt.Text, out int time))
            {
                var tarrif = await tarrifService.GetByIdTarrif(tarrifid);

                amount = (int)((double)(tarrif.Amount/60) * time);

                amount_txt.Text = "Итого=" + amount.ToString();
                amount_txt.Visibility = Visibility.Visible;

                Save_btn.IsEnabled = true;


            }
           else
            {
                amount_txt.Visibility = Visibility.Hidden;
                Save_btn.IsEnabled = false;
            }

          




          
        }

        private async void tarrif_cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool b = int.TryParse(tarrif_cbx.SelectedValue.ToString(), out int tarrifid);

            var tarrif = await tarrifService.GetByIdTarrif(tarrifid);

            if(tarrif.TarrifType==TarrifType.Vip)
            {
                Save_btn.IsEnabled = true;
            }

        }
    }
}
