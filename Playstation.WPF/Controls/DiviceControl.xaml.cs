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
    /// Interaction logic for DiviceControl.xaml
    /// </summary>
    public partial class DiviceControl : UserControl
    {
        IDeviceService _deviceService=new DeviceService();
        IEnumerable<Device> devices = new List<Device>();

        public DiviceControl()
        {
            
            InitializeComponent();
            //Task.Run(async ()
            //    =>
            //{
            //    devices = await _deviceService.GetDevices();
            //    device_datagrid.ItemsSource = devices;

            //});
           


        }

        private void create_device_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateDeviceView createDevice = new CreateDeviceView();
            createDevice.ShowDialog();
         }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            
            devices = await _deviceService.GetDevices();
            device_datagrid.ItemsSource = devices;
        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = device_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            int id=Convert.ToInt32(CellValue);

            MessageBoxResult res = MessageBox.Show("Вы бы хотели его удалить?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (res == MessageBoxResult.Yes)
            {
                
                await _deviceService.DeleteDevice(id);
                
                 MessageBox.Show("Удалено");
                
               

            }


            devices = await _deviceService.GetDevices();
            device_datagrid.ItemsSource = devices;
        }
    }
}
