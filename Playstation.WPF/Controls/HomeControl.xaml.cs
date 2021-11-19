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
        public int deviceId;
        public HomeControl()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var devices = await _deviceService.GetDevices();
            var orders1 = await _orderService.GetOrders();



          
            foreach (var i in devices)
            {
                var deviceorder = orders1.OrderByDescending(i => i.StartTime).FirstOrDefault(x => x.DeviceId == i.Id);
                if(deviceorder is null)
                {
                    orders.Add(new OrderDevice()
                    {
                        Id = i.Id,
                        Title = i.Title,
                        StartTime=DateTime.Now,
                        OrderTarrif = null
                    });
                }
                else
                {
                    if (deviceorder.Closed)
                    {
                        if(deviceorder.Tarrif.TarrifType==TarrifType.Simple)
                        {
                            orders.Add(new OrderDevice()
                            {
                                Id = i.Id,
                                Title = i.Title,
                                StartTime = deviceorder.StartTime,
                                EndTime=deviceorder.EndTime,
                                OrderTarrif = deviceorder.Tarrif.Title
                                
                            });
                        }
                        else
                        { 
                            if(deviceorder.Tarrif.TarrifType==TarrifType.Vip)
                            {
                                orders.Add(new OrderDevice()
                                {
                                    Id = i.Id,
                                    Title = i.Title,
                                    StartTime = deviceorder.StartTime,
                                    OrderTarrif = deviceorder.Tarrif.Title
                                   
                                });
                            }
                           

                        }
                        
                    }
                    else
                    {
                        orders.Add(new OrderDevice()
                        {
                            Id = i.Id,
                            Title = i.Title,
                            StartTime=DateTime.Now,
                            OrderTarrif = null
                           
                        });
                    }
                }
               
               
                

            }


            home_datagrid.ItemsSource = orders;
            for (int i = 0; i < home_datagrid.Items.Count; i++)
            {
                
                DataGridRow row = (DataGridRow)home_datagrid.ItemContainerGenerator
                                                           .ContainerFromIndex(i);
               // row.Visibility=Visibility.Hidden;
            }

        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = home_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            deviceId = Convert.ToInt32(CellValue);

            CreateButton = (Button)sender;

            CreateOrderView createOrder = new CreateOrderView(deviceId, (Button)sender, this);
            createOrder.ShowDialog();

        }

        private void finish_btn_Click(object sender, RoutedEventArgs e)
        {

            DataGrid dataGrid = home_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            deviceId = Convert.ToInt32(CellValue);
            FinishOrderView finishOrderView = new FinishOrderView(CreateButton, deviceId, this);
            finishOrderView.ShowDialog();
          
        }

        private  void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = home_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            deviceId = Convert.ToInt32(CellValue);


            UpdateOrderView updateOrder = new UpdateOrderView(deviceId, this);
            updateOrder.ShowDialog();
        }
    }
}
