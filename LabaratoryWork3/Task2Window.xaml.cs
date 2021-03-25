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
using System.Windows.Shapes;

namespace LabaratoryWork3
{
    /// <summary>
    /// Interaction logic for Task2Window.xaml
    /// </summary>
    public partial class Task2Window : Window
    {
        private int NUMBERS_COUNT = 15;
        private Random random = new Random();
        public Task2Window()
        {
            InitializeComponent();
            dataGrid.ItemsSource = CreateDataTable(GenerateRandomNumbers(NUMBERS_COUNT)).DefaultView;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private DataTable CreateDataTable(IList<int> values)
        {
            var dataTable = new DataTable();
            for (int i = 0; i < values.Count; i++)
            {
                dataTable.Columns.Add((i + 1).ToString());
            }
            dataTable.Rows.Add(values.Cast<object>().ToArray());
            return dataTable;
        }

        private IList<int> GenerateRandomNumbers(int numbersCount)
        {
            return Enumerable.Repeat(0, numbersCount)
                .Select(i => GenerateRandomNumber())
                .ToArray();
        }

        private int GenerateRandomNumber()
        {
            return random.Next(-100, 100);
        }
        private void UseTask2CellsPattern()
        {
            try
            {
                var cells = dataGrid.Columns.Select(column => column.GetCellContent(dataGrid.Items[0]) as TextBlock).ToList();
                var firstPositiveNumberIndex = cells.IndexOf(cells.First(cell => IsCellValuePositiveNumber(cell)));
                var lastPositiveNumberIndex = cells.IndexOf(cells.Last(cell => IsCellValuePositiveNumber(cell)));
                var middleOfIntervalIndex = Math.Ceiling(Convert.ToDecimal((lastPositiveNumberIndex + firstPositiveNumberIndex) / 2));
                for (int i = firstPositiveNumberIndex + 1; i < lastPositiveNumberIndex; i++)
                {
                    cells[i].Foreground = Brushes.Red;
                    if (i < middleOfIntervalIndex)
                        SwapCellsValue(cells[i], cells[lastPositiveNumberIndex - i + firstPositiveNumberIndex]);
                }
            } catch { return; }
        }

        private bool IsCellValuePositiveNumber(TextBlock cell)
        {
            try
            {
                return Convert.ToInt32(cell.Text) % 2 == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SwapCellsValue(TextBlock textBlock1, TextBlock textBlock2)
        {
            var buffer = textBlock1.Text;
            textBlock1.Text = textBlock2.Text;
            textBlock2.Text = buffer;
        }

        private void useTaskPatternButton_Click(object sender, RoutedEventArgs e)
        {
            UseTask2CellsPattern();
        }
    }
}
