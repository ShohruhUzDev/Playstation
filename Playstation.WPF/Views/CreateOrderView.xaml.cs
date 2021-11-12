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

        public CreateOrderView(int id,Button button)
        {
            InitializeComponent();
            this.id = id;
            createbtn = button;
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

            var order = new Order()
            { 
               DeviceId=id,
               StartTime=DateTime.Now,
                TarrifId=tarrifid,
                
            };
            await orderService.CreateOrder(order);
            this.Close();
            


            createbtn.IsEnabled = false;
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
