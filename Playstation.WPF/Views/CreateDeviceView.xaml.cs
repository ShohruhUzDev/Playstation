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

namespace Playstation.WPF.Controls
{
    /// <summary>
    /// Interaction logic for CreateDeviceView.xaml
    /// </summary>
    public partial class CreateDeviceView : Window
    {
        IDeviceService _deviceService=new DeviceService();
        IEnumerable<Device> devices = new List<Device>();
        public DiviceControl DiviceControl { get; }

        public CreateDeviceView(DiviceControl diviceControl)
        {
           
            InitializeComponent();
            DiviceControl = diviceControl;
        }


        private async void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            if(ipadress_txt.Text!=""&&title_txt.Text!="")
            {
                var device = new Device()
                {
                    IpAddress = ipadress_txt.Text,
                    Title = title_txt.Text
                };

                await _deviceService.CreateDevice(device);
                devices = await _deviceService.GetDevices();
              
                DiviceControl.device_datagrid.ItemsSource = devices;
                MessageBox.Show("Созданный");
                this.Close();

            }
            else
            {
                MessageBox.Show("Информация не была введена полностью!");

            }
          
            //CreateDeviceView createDeviceView = new CreateDeviceView();
            DiviceControl diviceControl = new DiviceControl();
            //devices = await _deviceService.GetDevices();
            // device_datagrid.ItemsSource = devices;
        }
    }
}
