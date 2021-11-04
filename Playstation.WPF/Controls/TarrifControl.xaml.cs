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
    /// Interaction logic for TarrifControl.xaml
    /// </summary>
    public partial class TarrifControl : UserControl
    {
        public TarrifControl()
        {
            InitializeComponent();
        }

        private void create_tarrif_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateTarrifView createTarrifView = new CreateTarrifView();
            createTarrifView.ShowDialog();
        }
    }
}
