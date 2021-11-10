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
        IOrderService orderService = new OrderService();
        List<TarrifForCBX> tarrifFors = new List<TarrifForCBX>();

        public GlavnayaControl GlavnayaControl { get; }

        public CreateOrderView(GlavnayaControl glavnayaControl)
        {
            InitializeComponent();
            GlavnayaControl = glavnayaControl;
        }

        private void tarrif_btn_Click(object sender, RoutedEventArgs e)
        {
            time_grid.Visibility = Visibility.Visible;
        }

        private void vip_btn_Click(object sender, RoutedEventArgs e)
        {
            time_grid.Visibility = Visibility.Hidden;
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

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            var order = new Order()
            {
                StartTime = DateTime.Now,
                EndTime = Convert.ToDateTime(time_txt.Text)
            };
           

        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
