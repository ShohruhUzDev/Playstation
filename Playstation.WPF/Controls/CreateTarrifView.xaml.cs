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
    /// Interaction logic for CreateTarrifView.xaml
    /// </summary>
    public partial class CreateTarrifView : Window
    {
        public CreateTarrifView()
        {
            InitializeComponent();
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateTarrifView tarrifView = new CreateTarrifView();
            this.Hide();
        }
    }
}
