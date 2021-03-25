using LabaratoryLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LaboratoryWork1
{
    class FunctionComputationViewModel : BaseViewModel, IDataErrorInfo
    {
        public FunctionComputationViewModel()
        {
            CalculateCommand = new ActionCommand(OnCalculateCommand);
        }
        public ICommand CalculateCommand { get; set; }
        private bool IsValidated => IsCurrectArgumentX() && IsCorrectCalculationAccuracy();
        private bool IsNotValidated => !IsValidated;
        private double _calculationAccuracy;
        public double CalculationAccuracy
        {
            get => _calculationAccuracy;
            set
            {
                _calculationAccuracy = value;
                NotifyPropertyChanged(nameof(CalculationAccuracy));
                NotifyPropertyChanged(nameof(IsValidated));
                NotifyPropertyChanged(nameof(IsNotValidated));
            }
        }
        private double _argumentX;
        public double ArgumentX
        {
            get => _argumentX;
            set
            {
                _argumentX = value;
                NotifyPropertyChanged(nameof(ArgumentX));
                NotifyPropertyChanged(nameof(IsValidated));
                NotifyPropertyChanged(nameof(IsNotValidated));
            }
        }
        private double _answer;
        public double Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                NotifyPropertyChanged(nameof(Answer));
            }
        }

        private double _seriesSum;
        public double SeriesSum
        {
            get => _seriesSum;
            set
            {
                _seriesSum = value;
                NotifyPropertyChanged(nameof(SeriesSum));
            }
        }

        private int _membersOfSeries;
        public int MembersOfSeries
        {
            get => _membersOfSeries;
            set
            {
                _membersOfSeries = value;
                NotifyPropertyChanged(nameof(MembersOfSeries));
            }
        }
        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case nameof(ArgumentX):
                        if (!IsCurrectArgumentX())
                            error = "аргумент X должен быть больше -1 и меньше или равно 1";
                        break;
                    case nameof(CalculationAccuracy):
                        if (!IsCorrectCalculationAccuracy())
                            error = "точность должен быть больше 0 и меньше или равно 1";
                        break;
                }
                return error;
            }
        }

        private bool IsCurrectArgumentX() => -1 < ArgumentX && ArgumentX <= 1;
        private bool IsCorrectCalculationAccuracy() => 0 < CalculationAccuracy && CalculationAccuracy < 1;
        private int Factorial(int x) => x == 0 ? 1 : Factorial(x - 1);
        private void OnCalculateCommand()
        {
            if (!IsValidated) return;
            double sum = 0.0;
            int n = 0;
            Answer = Math.Log(ArgumentX + 1);
            while (true)
            {
                sum += (Math.Pow(-1, n) * Math.Pow(ArgumentX, n + 1)) / (n + 1);

                if(Math.Abs(Answer - sum) < CalculationAccuracy)
                { 
                    break;
                }
                n++;
            }
            SeriesSum = sum;
            MembersOfSeries = n + 1;
        }

    }
}
