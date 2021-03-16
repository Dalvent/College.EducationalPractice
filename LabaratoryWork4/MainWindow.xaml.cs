using System;
using System.Collections.Generic;
using System.Data;
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

namespace LabaratoryWork4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Task4ViewModel ViewModel => (Task4ViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Task4ViewModel();
            mainDataGrid.CellEditEnding += mainDataGrid_CellEditEnding;
        }

        async void mainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            await Task.Delay(10);
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var changedValue = (e.Row.Item as DataRowView).Row[e.Column.DisplayIndex].ToString();
                ViewModel.ValueTable[e.Column.DisplayIndex] = Convert.ToInt32(changedValue);
            }
        }
    }
}
