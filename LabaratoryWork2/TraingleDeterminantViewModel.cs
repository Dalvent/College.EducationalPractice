using LabaratoryLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabaratoryWork2
{
    public class TraingleDeterminantViewModel : BaseViewModel
    {
        private ICommand _calculateSquareCommand; 
        public ICommand CalculateSquareCommand
        {
            get
            {
                if(_calculateSquareCommand == null)
                {
                    _calculateSquareCommand = new ActionCommand(OnCalculateSquare);
                }
                return _calculateSquareCommand;
            }
        }

        private ICommand _calculateSquareCommandForRange;
        public ICommand CalculateSquareCommandForRange
        {
            get
            {
                if (_calculateSquareCommandForRange == null)
                {
                    _calculateSquareCommandForRange = new ActionCommand(OnCalculateSquareForRange);
                }
                return _calculateSquareCommandForRange;
            }
        }

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


        private double _trianglePointForRange1;
        public double TrianglePointForRange1
        {
            get => _trianglePointForRange1;
            set
            {
                _trianglePointForRange1 = value;
                NotifyPropertyChanged(nameof(TrianglePointForRange1));
            }
        }

        private double _trianglePointForRange2;
        public double TrianglePointForRange2
        {
            get => _trianglePoint2;
            set
            {
                _trianglePointForRange2 = value;
                NotifyPropertyChanged(nameof(TrianglePointForRange2));
            }
        }

        private double _trianglePointForRange3;
        public double TrianglePointForRange3
        {
            get => _trianglePointForRange3;
            set
            {
                _trianglePointForRange3 = value;
                NotifyPropertyChanged(nameof(TrianglePointForRange3));
            }
        }

        private double _trianglePointForRange4;
        public double TrianglePointForRange4
        {
            get => _trianglePointForRange4;
            set
            {
                _trianglePointForRange4 = value;
                NotifyPropertyChanged(nameof(TrianglePointForRange4));
            }
        }

        public double _triangleSquare;
        public double TriangleSquare
        {
            get => _triangleSquare;
            set
            {
                _triangleSquare = value;
                NotifyPropertyChanged(nameof(TriangleSquare));
            }
        }

        public ObservableCollection<double> _triangleSquareForRange;
        public ObservableCollection<double> TriangleSquareForRange
        {
            get => _triangleSquareForRange;
            set
            {
                _triangleSquareForRange = value;
                NotifyPropertyChanged(nameof(TriangleSquareForRange));
            }
        }

        public void OnCalculateSquare()
        {
            
        }

        public void OnCalculateSquareForRange()
        {

        }
    }
}
