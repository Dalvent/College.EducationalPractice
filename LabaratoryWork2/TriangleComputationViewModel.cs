using LabaratoryLib;
using LabMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LabaratoryWork2
{
    class TriangleComputationViewModel : BaseViewModel
    {
        private ICommand _calculateSquareCommand;
        public ICommand CalculateSquareCommand => _calculateSquareCommand ?? (_calculateSquareCommand = new ActionCommand(OnCalculateSquare));
        private ICommand _calculateSquareInRangeCommand;
        public ICommand CalculateSquareInRangeCommand => _calculateSquareInRangeCommand ?? (_calculateSquareInRangeCommand = new ActionCommand(OnCalculateSquareInRange));

        private double _trianglePoint1;
        public double TrianglePoint1
        {
            get => _trianglePoint1;
            set
            {
                _trianglePoint1 = value;
                NotifyPropertyChanged(nameof(TrianglePoint1));
            }
        }

        private double _trianglePoint2;
        public double TrianglePoint2
        {
            get => _trianglePoint2;
            set
            {
                _trianglePoint2 = value;
                NotifyPropertyChanged(nameof(TrianglePoint2));
            }
        }

        private double _trianglePoint3;
        public double TrianglePoint3
        {
            get => _trianglePoint3;
            set
            {
                _trianglePoint3 = value;
                NotifyPropertyChanged(nameof(TrianglePoint3));
            }
        }

        private double _trianglePointInRange1;
        public double TrianglePointInRange1
        {
            get => _trianglePointInRange1;
            set
            {
                _trianglePointInRange1 = value;
                NotifyPropertyChanged(nameof(TrianglePointInRange1));
            }
        }

        private double _trianglePointInRange2;
        public double TrianglePointInRange2
        {
            get => _trianglePointInRange2;
            set
            {
                _trianglePointInRange2 = value;
                NotifyPropertyChanged(nameof(TrianglePointInRange2));
            }
        }

        private double _trianglePointInRange3;
        public double TrianglePointInRange3
        {
            get => _trianglePointInRange3;
            set
            {
                _trianglePointInRange3 = value;
                NotifyPropertyChanged(nameof(TrianglePointInRange3));
            }
        }

        private double _trianglePointInRange4;
        public double TrianglePointInRange4
        {
            get => _trianglePointInRange4;
            set
            {
                _trianglePointInRange4 = value;
                NotifyPropertyChanged(nameof(TrianglePointInRange4));
            }
        }

        private double? _triangleSquare;
        public double? TriangleSquare
        {
            get => _triangleSquare;
            set
            {
                _triangleSquare = value;
                NotifyPropertyChanged(nameof(TriangleSquare));
            }
        }

        private IEnumerable<double> _triangleSquareInRange;
        public IEnumerable<double> TriangleSquareInRange
        {
            get => _triangleSquareInRange;
            set
            {
                _triangleSquareInRange = value;
                NotifyPropertyChanged(nameof(TriangleSquareInRange));
            }
        }

        private void OnCalculateSquare()
        {
            try
            {
                TriangleSquare = TriangleFormulas.Square(TrianglePoint1, TrianglePoint2, TrianglePoint3);
            }
            catch (TriangleNotExistException)
            {
                TriangleSquare = null;
                MessageBox.Show("Треугольник не существует. Невозможно найти площадь", 
                    "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnCalculateSquareInRange()
        {
            TriangleSquareInRange = GetAllExistedTrianglesSquare().ToList();
        }

        private IEnumerable<double> GetAllExistedTrianglesSquare()
        {
            if(TriangleFormulas.IsTriangleExist(TrianglePointInRange1, TrianglePointInRange2, TrianglePointInRange3))
                yield return TriangleFormulas.Square(TrianglePointInRange1, TrianglePointInRange2, TrianglePointInRange3);

            if (TriangleFormulas.IsTriangleExist(TrianglePointInRange1, TrianglePointInRange2, TrianglePointInRange4))
                yield return TriangleFormulas.Square(TrianglePointInRange1, TrianglePointInRange2, TrianglePointInRange4);

            if (TriangleFormulas.IsTriangleExist(TrianglePointInRange1, TrianglePointInRange3, TrianglePointInRange4))
                yield return TriangleFormulas.Square(TrianglePointInRange1, TrianglePointInRange3, TrianglePointInRange4);

            if (TriangleFormulas.IsTriangleExist(TrianglePointInRange2, TrianglePointInRange3, TrianglePointInRange4))
                yield return TriangleFormulas.Square(TrianglePointInRange2, TrianglePointInRange3, TrianglePointInRange4);

        }
    }
}
