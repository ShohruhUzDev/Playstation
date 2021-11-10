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
    /// Interaction logic for UpdateDeviceView.xaml
    /// </summary>
    public partial class UpdateDeviceView : Window
    {
        IDeviceService _deviceService = new DeviceService();
       public  int _id;
        public DiviceControl DiviceControl { get; }

        public UpdateDeviceView(int id, DiviceControl diviceControl)
        {
            _id = id;
            DiviceControl = diviceControl;
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var device = await _deviceService.GetByIdDevice(_id);
            if(device is not null)
            {
                title_txt.Text = device.Title;
                ipadress_txt.Text = device.IpAddress;
            }

        }

        private async void Save_btn_Click(object sender, RoutedEventArgs e)
        {
          
            if(title_txt.Text!=""&& ipadress_txt.Text!="")
            {
                var device = new Device()
                {
                 Id=_id,
                 Title=title_txt.Text,
                 IpAddress=ipadress_txt.Text
                 };

               await _deviceService.UpdateDevice(device);
               
              
            }

            var devices = await _deviceService.GetDevices();

            DiviceControl.device_datagrid.ItemsSource = devices;
            this.Close();
         
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Window_Closed(object sender, EventArgs e)
        {
            var devices = await _deviceService.GetDevices();

            DiviceControl.device_datagrid.ItemsSource = devices;
        }
    }
}
