using LabaratoryLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LabaratoryWork6Part2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private async void doOperationIa_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ViewModel.FileName1 == null || ViewModel.FileName2 == null) return;
                var fileContent1 = File.ReadAllText(ViewModel.FileName1);
                var fileContent2 = File.ReadAllText(ViewModel.FileName2);
                var fileWords1 = fileContent1.Split('\r', '\n').Where(s => s != string.Empty).ToList();
                var fileWords2 = fileContent2.Split('\r', '\n').Where(s => s != string.Empty).ToList();
                var fileSymbolCount1 = GetContainsCount(fileWords1, 'я');
                var fileSymbolCount2 = GetContainsCount(fileWords2, 'я');
                if (fileSymbolCount1 >= fileSymbolCount2)
                {
                    fileWords1.Add(fileWords1[0]);
                    fileWords1.Add(fileWords1[1]);
                    fileWords1.Add(fileWords1[2]);
                    using (var writer = new StreamWriter(ViewModel.FileName1, false))
                        await writer.WriteAsync(ToString(fileWords1));
                    MessageBox.Show("Операция выполнена в файле 1");
                }
                else
                {
                    fileWords2.Add(fileWords2[0]);
                    fileWords2.Add(fileWords2[1]);
                    fileWords2.Add(fileWords2[2]);
                    using (var writer = new StreamWriter(ViewModel.FileName2, false))
                        await writer.WriteAsync(ToString(fileWords2));
                    MessageBox.Show("Операция выполнена в файле 2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка, либо файл удален, либо строк меньше 3");
            }

        }

        private string ToString(IEnumerable<string> values)
        {
            var builder = new StringBuilder();
            foreach (var value in values)
            {
                builder.Append(value + Environment.NewLine);
            }
            return builder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        private int GetContainsCount(IEnumerable<string> words, char symbol)
        {
            return words.Where(w => w.ToLower().Contains(symbol)).Count();
        }

        private void openFile1_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ViewModel.FileName1 = dialog.FileName;
                var s = ViewModel;
                DataContext = null;
                DataContext = s;
            }
        }

        private void openFile2_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ViewModel.FileName2 = dialog.FileName;
                var s = ViewModel;
                DataContext = null;
                DataContext = s;
            }
        }

        private void createFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "text | *.txt";
            if (dialog.ShowDialog() == true)
            {
                ViewModel.ByteFileName = dialog.FileName;
                File.WriteAllBytes(ViewModel.ByteFileName, ArrayIntToByte(GenerateRandomArray(ViewModel.NumbersCount)));
            }
        }

        private void writeAtEnd_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ByteFileName == null)
                return;

            var values = ArrayByteToInt(File.ReadAllBytes(ViewModel.ByteFileName)).ToList();
            var negativeNumbersCount = values.Count(v => v < 0);
            values.Add(negativeNumbersCount);
            File.WriteAllBytes(ViewModel.ByteFileName, ArrayIntToByte(values.ToArray()));

            MessageBox.Show($"{negativeNumbersCount} - кол-во негативных значений");
        }

        private Random random = new Random();
        private int CreateRandomNum()
        {
            return random.Next(-250, 250);
        }
        private int[] GenerateRandomArray(int count)
        {
            var array = new int[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = CreateRandomNum();
            }
            return array;
        }
        private byte[] ArrayIntToByte(int[] array)
        {
            byte[] result = new byte[array.Length * sizeof(int)];
            Buffer.BlockCopy(array, 0, result, 0, result.Length);
            return result;
        }

        private int[] ArrayByteToInt(byte[] array)
        {
            var size = array.Count() / sizeof(int);
            var ints = new int[size];
            for (var index = 0; index < size; index++)
            {
                ints[index] = BitConverter.ToInt32(array, index * sizeof(int));
            }
            return ints;
        }
    }

    public class MainViewModel : BaseViewModel
    {
        private string _fileName1;
        public string FileName1
        {
            get => _fileName1;
            set
            {
                _fileName1 = value;
                NotifyPropertyChanged(FileStatus1);
                NotifyPropertyChanged(FileName1);
            }
        }

        private string _fileName2;
        public string FileName2
        {
            get => _fileName2;
            set
            {
                _fileName2 = value;
                NotifyPropertyChanged(FileStatus2);
                NotifyPropertyChanged(FileName2);
            }
        }

        public int NumbersCount { get; set; }
        public string ByteFileName { get; set; }
        public string FileStatus1 => _fileName1 == null ? "No" : "Ok";
        public string FileStatus2 => _fileName2 == null ? "No" : "Ok";
    }
}
