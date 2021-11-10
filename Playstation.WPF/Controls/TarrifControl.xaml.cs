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
    /// Interaction logic for TarrifControl.xaml
    /// </summary>
    public partial class TarrifControl : UserControl
    {
        ITarrifService _tarrifService=new TarrifService();
        IEnumerable<Tarrif> tarrifs = new List<Tarrif>();

        public TarrifControl()
        {
            InitializeComponent();
        }

        private void create_tarrif_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateTarrifView createTarrifView = new CreateTarrifView(this);
            createTarrifView.ShowDialog();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tarrifs = await _tarrifService.GetTarrifs();
            tarrif_datagrid.ItemsSource = tarrifs;
        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = tarrif_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            int id = Convert.ToInt32(CellValue);

            MessageBoxResult res = MessageBox.Show("Вы бы хотели его удалить?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);

            

            if (res == MessageBoxResult.Yes)
            {

                await _tarrifService.DeleteTarrif(id);

                MessageBox.Show("Удалено");



            }


            tarrifs = await _tarrifService.GetTarrifs();
            tarrif_datagrid.ItemsSource = tarrifs;
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = tarrif_datagrid;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            int id = Convert.ToInt32(CellValue);
            
            UpdateTarrifView updateDeviceView = new UpdateTarrifView(id, this);
            updateDeviceView.ShowDialog();
        }
    }
}
