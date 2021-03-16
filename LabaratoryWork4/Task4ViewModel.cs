using LabaratoryLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LabaratoryWork4
{
    public class Task4ViewModel : BaseViewModel
    {
        public Task4ViewModel()
        {
            ShowTaskValue = new ActionCommand(() => {
                var rightEvenNumbers = GetEvenСomposition(ValueTable.Take(ValueTable.Count / 2).ToArray());
                var leftEvenNumbers = GetEvenСomposition(ValueTable.Skip(ValueTable.Count + 1 / 2).ToArray());
                var rightEvenNumbersString = rightEvenNumbers != null ? rightEvenNumbers.ToString() : "нет четных значений";
                var leftEvenNumbersString = leftEvenNumbers != null ? leftEvenNumbers.ToString() : "нет четных значений";

                MessageBox.Show($"Произведения правых четных значений {rightEvenNumbersString}\n" +
                    $"Произведения левых четных значений {leftEvenNumbersString}\n");
            });
        }

        private Random random = new Random();

        public IList<int> ValueTable { get; set; } = new List<int>();
        private int valuesCount;
        public int ValuesCount
        {
            get => valuesCount;
            set
            {
                valuesCount = value;
                RegenerateTableValues();
                NotifyPropertyChanged(nameof(ValuesCount));
                NotifyPropertyChanged(nameof(ValueTableView));
            }
        }
        public DataView ValueTableView => CreateDataTable().DefaultView;
        public bool IsFillManually { get; set; } = true;
        public bool IsFillRandom { get; set; }

        public ICommand ShowTaskValue { get; set; }

        private int? GetEvenСomposition(IList<int> values)
        {
            int? result = null;
            foreach (var value in values)
            {
                if(value % 2 == 0)
                {
                    if(!result.HasValue)
                    {
                        result = value;
                    }
                    else
                    {
                        result *= value;
                    }
                }
            }
            return result;
        }

        private void RegenerateTableValues()
        {
            if(IsFillRandom)
            {
                ValueTable = GenerateRandomNumbers();
            }
            else
            {
                ValueTable = RegenerateTableValuesWithZeroValue();
            }
        }

        private IList<int> GenerateRandomNumbers()
        {
            return Enumerable.Repeat(0, ValuesCount)
                .Select(i => random.Next(-100, 100))
                .ToArray();
        }

        private IList<int> RegenerateTableValuesWithZeroValue()
        {
            var newValueTable = new int[valuesCount];
            for (int i = 0; i < Math.Min(ValueTable.Count, valuesCount); i++)
            {
                newValueTable[i] = ValueTable[i];
            }
            return newValueTable;
        }
        private DataTable CreateDataTable()
        {
            var dataTable = new DataTable();
            for (int i = 0; i < ValueTable.Count; i++)
            {
                dataTable.Columns.Add((i + 1).ToString());
            }
            dataTable.Rows.Add(ValueTable.Cast<object>().ToArray());
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
    }
}
