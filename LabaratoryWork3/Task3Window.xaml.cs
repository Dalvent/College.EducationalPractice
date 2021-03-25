using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Task3Window.xaml
    /// </summary>
    public partial class Task3Window : Window
    {
        public ObservableCollection<NumberContainer> NumberContainers { get; set; }
        public Task3Window()
        {
            InitializeComponent();
            DataContext = this;
            NumberContainers = new ObservableCollection<NumberContainer>();
            NumberContainers.Add(new NumberContainer(1));
        }

        private void showNotLastEvenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lastNotEvenPositionIndex = Enumerable.Range(1, NumberContainers.Count).Last(number => number % 2 == 1);
                var numberOnLastNotEvenPosition = NumberContainers[lastNotEvenPositionIndex - 1];
                NumberContainers.Clear();
                NumberContainers.Add(numberOnLastNotEvenPosition);
            }
            catch { NumberContainers.Clear(); }
        }

        public class NumberContainer
        {
            public NumberContainer()
            {
            }
            public NumberContainer(int number)
            {
                Number = number;
            }
            public int Number { get; set; }
        }
    }
}
