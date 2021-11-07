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
    /// Interaction logic for CreateTarrifView.xaml
    /// </summary>
    public partial class CreateTarrifView : Window
    {
        ITarrifService _tarrifService = new TarrifService();

        public CreateTarrifView()
        {
            InitializeComponent();
        }

        private async void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            string s = amount_txt.Text;
            
            if(title_txt.Text!=""&&amount_txt.Text!=""&&totalminutes_txt.Text!="")
            {
                Tarrif tarrif = new Tarrif()
                {
                    Title=title_txt.Text,
                    Amount=Convert.ToInt32( amount_txt.Text),
                    TotalMinutes=Convert.ToInt32(totalminutes_txt.Text)
                };
                await _tarrifService.CreateTarrif(tarrif);
                MessageBox.Show("Созданный");
                this.Close();

            }
            else
            {
                MessageBox.Show("Информация не была введена полностью!");

            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateTarrifView tarrifView = new CreateTarrifView();
            this.Close();
        }
    }
}
