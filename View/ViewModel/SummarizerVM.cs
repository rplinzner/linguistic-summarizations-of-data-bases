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
using LiveCharts.Wpf.Charts.Base;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class SummarizerVM : BaseVM, IFunctionSelector
    {
        private MainWindowVM Parent { get; set; }
        public ObservableCollection<AttributesListVm> AttributesList { get; set; }

        public AttributesListVm AttributeSelected
        {
            get
            {
                return _attributeSelected;
            }
            set
            {
                _attributeSelected = value;
                Draw();
            }
        }

        public ObservableCollection<string> FunctionComboBox { get; set; }
        public string SelectedFunction { get; set; }


        public string LabelNameTB { get; set; }
        public ICommand OpenFunctionParamsWindow { get; set; }
        public ICommand Details { get; }
        public ICommand Remove { get; }


        public FunctionSelectionVM FunctionSelectionVm { get; set; }

        public Summarizer SummarizerSelected { get; set; }
        private FunctionSelectionWindow _window;
        private AttributesListVm _attributeSelected;

        public SeriesCollection SeriesCollection { get; set; }
        public int MinValue { get; set; } = 0;
        public int MaxValue { get; set; } = 1;

        public SummarizerVM(MainWindowVM parent)
        {
            Parent = parent;
            AttributesList = AtributesLoader.ConvertCoverToAtributesListVms(parent.Covers);
            FunctionComboBox = new ObservableCollection<string> {
                "Trapezoidal",
                "Triangular"
            };

            OpenFunctionParamsWindow = new RelayCommand(ShowFunctionWindow);
            Remove = new RelayCommand(OnRemove);
            Details = new RelayCommand(OnDetails);
        }

        public void AddToCollection()
        {
            if (FunctionSelectionVm.Function == null)
            {
                MessageBox.Show("Please setup function in configurator!");
                return;
            }

            var temp = new Summarizer(LabelNameTB, new FuzzySet(FunctionSelectionVm.Function));
            AttributeSelected.Summarizers.Add(temp);
            Parent.SummaryVm.Qualifiers.Add(temp);
            Draw();

        }

        private void Draw()
        {
            if (AttributeSelected == null)
            {
                return;
            }
            SeriesCollection = new SeriesCollection();
            MinValue = AttributeSelected.Min;
            MaxValue = AttributeSelected.Max;


            foreach (var summarizer in AttributeSelected.Summarizers)
            {
                ChartValues<ObservablePoint> lineValues = new ChartValues<ObservablePoint>();
                if (summarizer.FuzzySet.MembershipFunction.GetType() == typeof(TriangularFunction))
                {
                    var xs = summarizer.FuzzySet.MembershipFunction.GetValues();
                    lineValues.Add(new ObservablePoint(xs[0], 0));
                    lineValues.Add(new ObservablePoint(xs[2], 1));
                    lineValues.Add(new ObservablePoint(xs[1], 0));

                }
                if (summarizer.FuzzySet.MembershipFunction.GetType() == typeof(TrapezoidalFunction))
                {
                    var xs = summarizer.FuzzySet.MembershipFunction.GetValues();
                    lineValues.Add(new ObservablePoint(xs[0], 0));
                    lineValues.Add(new ObservablePoint(xs[3], 1));
                    lineValues.Add(new ObservablePoint(xs[2], 1));
                    lineValues.Add(new ObservablePoint(xs[1], 0));

                }

                SeriesCollection.Add(new LineSeries()
                {
                    Values = lineValues,
                    Title = summarizer.Label,
                    LineSmoothness = 0.0,
                    ScalesYAt = 0
                });
            }

        }

        private void OnDetails()
        {
            if (SummarizerSelected == null)
            {
                MessageBox.Show("Please choose summarizer");
                return;
            }

            try
            {
                FunctionSelectionVm =
                    new FunctionSelectionVM(SummarizerSelected.FuzzySet.MembershipFunction, AttributeSelected.Min, AttributeSelected.Max);
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

        public void Close()
        {
            LabelNameTB = string.Empty;
            _window.Close();
        }

        private void OnRemove()
        {
            if (SummarizerSelected == null)
            {
                MessageBox.Show("Please choose summarizer");
                return;
            }
            AttributeSelected.Summarizers.Remove(SummarizerSelected);
            Draw();
        }

        private void ShowFunctionWindow()
        {
            if (string.IsNullOrEmpty(SelectedFunction))
            {
                MessageBox.Show("Please choose function type");
                return;
            }

            try
            {
                FunctionSelectionVm =
                    new FunctionSelectionVM(SelectedFunction, AttributeSelected.Min, AttributeSelected.Max, this);
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


    }
}