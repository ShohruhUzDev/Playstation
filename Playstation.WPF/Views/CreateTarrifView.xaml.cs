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

        public TarrifControl TarrifControl { get; }

        public CreateTarrifView(TarrifControl tarrifControl )
        {
            InitializeComponent();
            TarrifControl = tarrifControl;
        }

        private async void Save_btn_Click(object sender, RoutedEventArgs e)
        {
          
            

            if (title_txt.Text!=""&&amount_txt.Text!=""&&totalminutes_txt.Text!=""&&int.TryParse(amount_txt.Text, out int amount)&&int.TryParse(totalminutes_txt.Text, out int minutes)
               )
            {
                if(viptype_rdbtn.IsChecked==true)
                {
                    Tarrif tarrif = new Tarrif()
                    {
                        Title = title_txt.Text,
                        Amount = amount,
                        TotalMinutes = minutes, 
                        TarrifType=TarrifType.Vip
                    };
                    await _tarrifService.CreateTarrif(tarrif);
                }
                if (simpletype_rdbtn.IsChecked == true)
                {
                    Tarrif tarrif = new Tarrif()
                    {
                        Title = title_txt.Text,
                        Amount = amount,
                        TotalMinutes = minutes,
                        TarrifType = TarrifType.Simple
                    };
                    await _tarrifService.CreateTarrif(tarrif);
                }


                var tarrifs = await _tarrifService.GetTarrifs();

                TarrifControl.tarrif_datagrid.ItemsSource = tarrifs;
                MessageBox.Show("Созданный");
                this.Close();

            }
            else
            {
                MessageBox.Show("Информация не была введена полностью или Информация была введена неверно");

            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
