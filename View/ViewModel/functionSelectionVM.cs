
using System.Windows.Input;
using Fuzzy.Function;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class FunctionSelectionVM : BaseVM
    {
        public double ATB
        {
            get => _atb;
            set
            {
                _atb = value;
                aTB.X = value;
                if (IsTriangle)
                {
                    CTriangleTB = value + ((BTB - value) / 2.0);
                }
                OnPropertyChanged(nameof(ATB));
            }
        }

        private ObservablePoint aTB { get; set; } = new ObservablePoint(0, 0);

        public double BTB
        {
            get => _btb;
            set
            {
                _btb = value;
                bTB.X = value;
                if (IsTriangle)
                {
                    CTriangleTB = ATB + ((value - ATB) / 2.0);
                }
                OnPropertyChanged(nameof(BTB));
            }
        }

        private ObservablePoint bTB { get; set; } = new ObservablePoint(0, 0);

        public double CTB
        {
            get => _ctb;
            set
            {
                _ctb = value;
                cTB.X = value;
                OnPropertyChanged(nameof(CTB));
            }

        }

        private ObservablePoint cTB { get; set; } = new ObservablePoint(0, 1);

        public double DTB
        {
            get => _dtb;
            set
            {
                _dtb = value;
                dTB.X = value;
                OnPropertyChanged(nameof(DTB));
            }
        }

        private ObservablePoint dTB { get; set; } = new ObservablePoint(0, 1);

        public double CTriangleTB
        {
            get => _cTriangleTb;
            set
            {
                _cTriangleTb = value;
                cTriangleTB.X = value;
                OnPropertyChanged(nameof(CTriangleTB));
            }
        }

        private ObservablePoint cTriangleTB { get; set; } = new ObservablePoint(0, 1);

        public IFunction Function { get; set; }
        private string _function;
        private double _atb;
        private double _btb;
        private double _ctb;
        private double _dtb;
        private double _cTriangleTb;
        public ICommand Save { get; set; }

        public IFunctionSelector Parent { get; set; }
        public bool IsAddButtonVisible { get; set; } = true;
        public bool IsTrapezoid { get; set; }
        public bool IsTriangle { get; set; }

        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public FunctionSelectionVM(string function, int min, int max, IFunctionSelector parent)
        {
            MinValue = min;
            MaxValue = max;
            _function = function;
            if (function.Contains("Triangular"))
            {
                IsTriangle = true;
                ATB = min;
                BTB = max;
            }
            else
            {
                IsTrapezoid = true;
                var delta = (max - min) / 10;
                ATB = min;
                BTB = max;
                CTB = max - delta;
                DTB = min + delta;
            }
            Save = new RelayCommand(OnSave);

            Parent = parent;

            Draw();
        }

        public FunctionSelectionVM(IFunction function, int min, int max)
        {
            IsAddButtonVisible = false;
            MinValue = min;
            MaxValue = max;
            if (function.GetType() == typeof(TriangularFunction))
            {
                IsTriangle = true;
                _function = "Triangular";
                var vals = function.GetValues();
                ATB = vals[0];
                BTB = vals[1];
                CTriangleTB = vals[2];

            }
            else
            {
                IsTrapezoid = true;
                _function = "Trapezoidal";
                var vals = function.GetValues();
                ATB = vals[0];
                BTB = vals[1];
                CTB = vals[2];
                DTB = vals[3];
            }
            Draw();
        }


        private void Draw()
        {
            ChartValues<ObservablePoint> lineValues = new ChartValues<ObservablePoint>();
            SeriesCollection = new SeriesCollection();


            switch (_function)
            {
                case "Triangular":

                    lineValues.Add(aTB);
                    lineValues.Add(cTriangleTB);
                    lineValues.Add(bTB);

                    break;
                case "Trapezoidal":
                    lineValues.Add(aTB);
                    lineValues.Add(dTB);
                    lineValues.Add(cTB);
                    lineValues.Add(bTB);
                    break;
            }

            SeriesCollection.Add(new LineSeries()
            {
                Values = lineValues,
                LineSmoothness = 0.0,
                ScalesYAt = 0,
                Title = _function
            });

        }

        private void OnSave()
        {
            switch (_function)
            {
                case "Triangular":
                    if (ATB < MinValue)
                    {
                        ATB = MinValue;
                    }

                    if (BTB > MaxValue)
                    {
                        BTB = MaxValue;
                    }
                    Function = new TriangularFunction(ATB, BTB);
                    break;
                case "Trapezoidal":
                    if (ATB < MinValue)
                    {
                        ATB = MinValue;
                    }
                    if (DTB < MinValue)
                    {
                        DTB = MinValue;
                    }
                    if (BTB > MaxValue)
                    {
                        BTB = MaxValue;
                    }
                    if (CTB > MaxValue)
                    {
                        CTB = MaxValue;
                    }
                    Function = new TrapezoidalFunction(ATB, BTB, CTB, DTB);
                    break;
            }
            Parent.AddToCollection();
            Parent.Close();
        }

    }
}