using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Fuzzy.Function;
using Fuzzy.Set;
using Fuzzy.Summarizer;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class QuantifierVM : BaseVM, IFunctionSelector
    {
        public MainWindowVM Parent { get; set; }

        public ObservableCollection<Quantifier> Quantifiers { get; set; } = new ObservableCollection<Quantifier>();
        public Quantifier QuantifierSelected { get; set; }
        public ObservableCollection<string> FunctionComboBox { get; set; }
        public string SelectedFunction
        {
            get;
            set;
        }


        public string LabelNameTB { get; set; }
        public ICommand OpenFunctionParamsWindow { get; set; }
        public ICommand Details { get; }
        public ICommand Remove { get; }


        public FunctionSelectionVM FunctionSelectionVm { get; set; }
        private FunctionSelectionWindow _window;

        public SeriesCollection SeriesCollection { get; set; }
        public int MinValue { get; set; } = 0;
        public int MaxValue { get; set; } = 1;

        public QuantifierVM(MainWindowVM parent)
        {
            Parent = parent;
            FunctionComboBox = new ObservableCollection<string> {
                "Trapezoidal",
                "Triangular"
            };
            
            OpenFunctionParamsWindow = new RelayCommand(ShowFunctionWindow);
            Details=new RelayCommand(OnDetails);
            Remove=new RelayCommand(OnRemove);
        }

        internal void Draw()
        {
            
            SeriesCollection = new SeriesCollection();
           
            foreach (var quantifier in Quantifiers)
            {
                ChartValues<ObservablePoint> lineValues = new ChartValues<ObservablePoint>();
                if (quantifier.FuzzySet.MembershipFunction.GetType() == typeof(TriangularFunction))
                {
                    var xs = quantifier.FuzzySet.MembershipFunction.GetValues();
                    lineValues.Add(new ObservablePoint(xs[0], 0));
                    lineValues.Add(new ObservablePoint(xs[2], 1));
                    lineValues.Add(new ObservablePoint(xs[1], 0));

                }
                if (quantifier.FuzzySet.MembershipFunction.GetType() == typeof(TrapezoidalFunction))
                {
                    var xs = quantifier.FuzzySet.MembershipFunction.GetValues();
                    lineValues.Add(new ObservablePoint(xs[0], 0));
                    lineValues.Add(new ObservablePoint(xs[3], 1));
                    lineValues.Add(new ObservablePoint(xs[2], 1));
                    lineValues.Add(new ObservablePoint(xs[1], 0));

                }

                SeriesCollection.Add(new LineSeries()
                {
                    Values = lineValues,
                    Title = quantifier.Label,
                    LineSmoothness = 0.0,
                    ScalesYAt = 0
                });
            }

        }



        public void AddToCollection()
        {
            if (FunctionSelectionVm.Function == null)
            {
                MessageBox.Show("Please setup function in configurator!");
                return;
            }
            Quantifiers.Add(new Quantifier(LabelNameTB, new FuzzySet(FunctionSelectionVm.Function)));
            Draw();

        }

        private void OnRemove()
        {
            if (QuantifierSelected == null)
            {
                MessageBox.Show("Please choose summarizer");
                return;
            }
            Quantifiers.Remove(QuantifierSelected);
            Draw();
        }

        private void OnDetails()
        {
            if (QuantifierSelected == null)
            {
                MessageBox.Show("Please choose summarizer");
                return;
            }

            try
            {
                FunctionSelectionVm =
                    new FunctionSelectionVM(QuantifierSelected.FuzzySet.MembershipFunction, 0, 1);
                _window = new FunctionSelectionWindow()
                {
                    DataContext = FunctionSelectionVm
                };
                _window.Show();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ShowFunctionWindow()
        {
            if (string.IsNullOrEmpty(SelectedFunction))
            {
                MessageBox.Show("Please choose function type");
                return;
            }
            FunctionSelectionVm = new FunctionSelectionVM(SelectedFunction, 0,1, this);
            _window = new FunctionSelectionWindow()
            {
                DataContext = FunctionSelectionVm
            };
            _window.Show();
        }

        public void Close()
        {
            LabelNameTB = string.Empty;
            _window.Close();
        }
    }
}