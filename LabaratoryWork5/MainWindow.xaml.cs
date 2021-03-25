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

namespace LabaratoryWork5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        public int[,] ValueList { get; set; } = new int[0, 0];
        public bool IsFillManually { get; set; } = true;
        public bool IsFillRandom { get; set; }
        public int ForValueHigher { get; set; }
        private int rowCount;
        public int RowCount
        {
            get => rowCount;
            set
            {
                rowCount = value;
                UpdateTable();
            }
        }
        private int columnCount;
        public int ColumnCount
        {
            get => columnCount;
            set
            {
                columnCount = value;
                UpdateTable();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            dataGrid.CellEditEnding += mainDataGrid_CellEditEnding;
            DataContext = this;
        }
        private void UpdateTable()
        {
            ValueList = GenerateRandomTable(RowCount, ColumnCount);
            dataGrid.ItemsSource = CreateDataTable(ValueList, rowCount, columnCount).DefaultView;
        }
        private DataTable CreateDataTable(int[,] table, int rowCount, int columnCount)
        {
            var dataTable = new DataTable();
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                dataTable.Columns.Add((columnIndex + 1).ToString());
            }
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                dataTable.Rows.Add(GetRow(ValueList, rowIndex).Cast<object>().ToArray());
            }
            return dataTable;
        }
        private IEnumerable<int> GetRow(int[,] array, int rowIndex)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                yield return array[rowIndex, i];
            }
        }
        private IEnumerable<int> GetColumn(int[,] array, int columnIndex)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                yield return array[i, columnIndex];
            }
        }
        private IEnumerable<IEnumerable<int>> ToRowCollection(int[,] array)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                yield return GetRow(array, i);
            }
        }
        private IEnumerable<IEnumerable<int>> ToColumnCollection(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                yield return GetColumn(array, i);
            }
        }
        private int[,] GenerateRandomTable(int rowCount, int columnCount)
        {
            var table = new int[rowCount, columnCount];
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    table[rowIndex, columnIndex] = GenerateRandomNumber();
                }
            }
            return table;
        }
        private int GenerateRandomNumber()
        {
            if(IsFillManually)
            {
                return 0;
            }
            return random.Next(-100, 100);
        }
        async void mainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var changedValue = (e.EditingElement as TextBox).Text.ToString();
                ValueList[e.Row.GetIndex() ,e.Column.DisplayIndex] = Convert.ToInt32(changedValue);
            }
        }
        private void positiveNumbersCount_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Кол-во {ConvertToArray(ValueList).Where(value => value > 0).Count()}");
        }
        private void showValueHigher_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Кол-во {ConvertToArray(ValueList).Where(value => value > ForValueHigher).Count()}");
        }
        private void multiplayNonzeroElements_Click(object sender, RoutedEventArgs e)
        {
            var colums = ToListList(ToColumnCollection(ValueList));
            var messageStringBuilder = new StringBuilder();

            messageStringBuilder.Append($"Все {MultiplayNonZero(ConvertToArray(ValueList))}\n");

            var columnMultiplayValues = new List<int>();
            for (int i = 0; i < colums.Count; i++)
            {
                columnMultiplayValues.Add(MultiplayNonZero(colums[i]));
            }
            var maxColumnMultiplayValues = columnMultiplayValues.Max();

            for (int i = 0; i < columnMultiplayValues.Count; i++)
            {
                var builder = new StringBuilder();
                builder.Append($"Колонка {i + 1}: {columnMultiplayValues[i]}");
                if(columnMultiplayValues[i] == maxColumnMultiplayValues)
                {
                    builder.Append(" - наибольшее значение");
                }
                builder.Append("\n");
                messageStringBuilder.Append(builder.ToString());
            }

            MessageBox.Show(messageStringBuilder.ToString());
        }
        private void showCountOneTwo_Click(object sender, RoutedEventArgs e)
        {
            var rowCollection = ToRowCollection(ValueList);
            int oneTwoCount = 0;

            foreach (var row in rowCollection)
            {
                if(IsOneTwo(row))
                {
                    oneTwoCount++;
                }
            }
            MessageBox.Show($"Кол-во: {oneTwoCount}");
        }
        private bool IsOneTwo(IEnumerable<int> array)
        {
            if (array.Count() != array.Distinct().Count())
                return false;

            for (int i = 1; i <= array.Count(); i++)
            {
                if(!array.Contains(i))
                {
                    return false;
                }
            }
            return true;
        }
        private List<List<int>> ToListList(IEnumerable<IEnumerable<int>> table)
        {
            return table.Select(item => item.ToList()).ToList();
        }
        private T[] ConvertToArray<T>(T[,] array)
        {
            return array.Cast<T>().ToArray();
        }
        private int MultiplayNonZero(IEnumerable<int> array)
        {
            int? result = null;
            foreach (var item in array)
            {
                if(item != 0)
                {
                    if(result == null)
                    {
                        result = item;
                    }
                    else
                    {
                        result *= item;
                    }
                }
            }

            return result.HasValue ? result.Value : 0;
        }
    }
}
