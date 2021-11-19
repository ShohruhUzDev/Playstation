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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Playstation.WPF.Controls
{
    /// <summary>
    /// Interaction logic for OrderControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        IOrderService orderService = new OrderService();
        List<OrderShow> orderShow = new List<OrderShow>();

        public OrderControl()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           var orders = await orderService.GetOrders();


            foreach(var i in orders)
            {
                orderShow.Add(new OrderShow()
                {
                    Id = i.Id,
                    DeviceTitle = i.Device.Title,
                    OrderTitle = i.Tarrif.Title,
                    StartTime = i.StartTime,
                    EndTime = i.EndTime,
                    Amount = i.Amount
                });

            }
            order_datagrid.ItemsSource = orderShow;

        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = order_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            int id = Convert.ToInt32(CellValue);

            MessageBoxResult res = MessageBox.Show("Вы бы хотели его удалить?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);



            if (res == MessageBoxResult.Yes)
            {

                await orderService.DeleteOrder(id);

               // MessageBox.Show("Удалено");



            }


          var orders  = await orderService.GetOrders();
            foreach (var i in orders)
            {
                orderShow.Add(new OrderShow()
                {
                    Id = i.Id,
                    DeviceTitle = i.Device.Title,
                    OrderTitle = i.Tarrif.Title,
                    StartTime = i.StartTime,
                    EndTime = i.EndTime,
                    Amount = i.Amount
                });

            }
            order_datagrid.ItemsSource = orderShow;
            
        }
    }
}
