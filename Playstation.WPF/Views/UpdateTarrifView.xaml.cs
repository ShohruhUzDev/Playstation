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
    /// Interaction logic for UpdateTarrifView.xaml
    /// </summary>
    public partial class UpdateTarrifView : Window
    {
        ITarrifService _tarrifService = new TarrifService();
        public int _id;
        public TarrifControl TarrifControl { get; }
        public UpdateTarrifView(int id, TarrifControl tarrifControl)
        {
            _id = id;
            TarrifControl = tarrifControl;
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tarrif = await _tarrifService.GetByIdTarrif(_id);
            if (tarrif is not null)
            {
                title_txt.Text = tarrif.Title;
                amount_txt.Text = tarrif.Amount.ToString();
                totalminutes_txt.Text = tarrif.TotalMinutes.ToString();
               
            }

        }

        private async void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (title_txt.Text != "" && amount_txt.Text != "" && totalminutes_txt.Text != "" && int.TryParse(amount_txt.Text, out int amount) && int.TryParse(totalminutes_txt.Text, out int minutes))
            {
                Tarrif tarrif = new Tarrif()
                {
                    Id=_id,
                    Title = title_txt.Text,
                    Amount = amount,
                    TotalMinutes = minutes
                };
                await _tarrifService.UpdateTarrif(tarrif);
                var tarrifs = await _tarrifService.GetTarrifs();
                TarrifControl.tarrif_datagrid.ItemsSource = tarrifs;

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

        private async void Window_Closed(object sender, EventArgs e)
        {
            var tarrifs = await _tarrifService.GetTarrifs();
            TarrifControl.tarrif_datagrid.ItemsSource = tarrifs;
        }
    }
}
