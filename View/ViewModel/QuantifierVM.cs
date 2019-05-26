using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Fuzzy.Set;
using Fuzzy.Summarizer;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class QuantifierVM : BaseVM
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
        public ICommand AddQuantifier { get; set; }

        public FunctionSelectionVM FunctionSelectionVm { get; set; }

        public QuantifierVM(MainWindowVM parent)
        {
            Parent = parent;
            FunctionComboBox = new ObservableCollection<string> {
                "Trapezoidal",
                "Triangular"
            };
            AddQuantifier = new RelayCommand(AddToQualifiers);
            OpenFunctionParamsWindow = new RelayCommand(ShowFunctionWindow);
        }

        private void AddToQualifiers()
        {
            if (FunctionSelectionVm.Function == null)
            {
                MessageBox.Show("Please setup function in configurator!");
                return;
            }
            Quantifiers.Add(new Quantifier(LabelNameTB, new FuzzySet(FunctionSelectionVm.Function)));
        }

        private void ShowFunctionWindow()
        {
            if (string.IsNullOrEmpty(SelectedFunction))
            {
                MessageBox.Show("Please choose function type");
                return;
            }
            FunctionSelectionVm = new FunctionSelectionVM(SelectedFunction, 0,1);
            FunctionSelectionWindow window = new FunctionSelectionWindow()
            {
                DataContext = FunctionSelectionVm
            };
            window.Show();
        }
    }
}