using Playstation.WPF.Controls;
using Playstation.WPF.Interfaces;
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

namespace Playstation.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDeviceService _deviceService;
        private readonly ITarrifService _tarrifService;

        public MainWindow()
        {
            //IDeviceService deviceService
           // _deviceService = deviceService;
            InitializeComponent();
        }

        private void tarif_btn_Click(object sender, RoutedEventArgs e)
        {
            TarrifControl tarrifControl = new TarrifControl();
            Controls_grid.Children.Clear();
            Controls_grid.Children.Add(tarrifControl);
        }

        private void device_btn_Click(object sender, RoutedEventArgs e)
        {
            DiviceControl control = new DiviceControl();
            Controls_grid.Children.Clear();

            Controls_grid.Children.Add(control);
        }

        private void home_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
