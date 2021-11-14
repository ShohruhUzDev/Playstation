using Playstation.WPF.Interfaces;
using Playstation.WPF.Models;
using Playstation.WPF.Services;
using Playstation.WPF.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Playstation.WPF.Controls
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        ITarrifService _tarrifService = new TarrifService();
        IDeviceService _deviceService = new DeviceService();
        IOrderService _orderService = new OrderService();
        public   List<OrderDevice> orders = new List<OrderDevice>();
        public Button CreateButton;
        public HomeControl()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var devices = await _deviceService.GetDevices();
            foreach (var i in devices)
            {
                orders.Add(new OrderDevice()
                {
                    Id = i.Id,
                    Title = i.Title,
                    OrderTarrif=null
                });

            }
            home_datagrid.ItemsSource = orders;

        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = home_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            int id = Convert.ToInt32(CellValue);

            CreateButton = (Button)sender;

            CreateOrderView createOrder = new CreateOrderView(id, (Button)sender, this);
            createOrder.ShowDialog();

        }

        private void finish_btn_Click(object sender, RoutedEventArgs e)
        {

            DataGrid dataGrid = home_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            int id = Convert.ToInt32(CellValue);
            FinishOrderView finishOrderView = new FinishOrderView(CreateButton, id);
            finishOrderView.ShowDialog();
          
        }
    }
}
