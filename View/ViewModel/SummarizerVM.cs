using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Fuzzy.Function;
using Fuzzy.Set;
using Fuzzy.Summarizer;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class SummarizerVM : BaseVM
    {
        private MainWindowVM Parent { get; set; }
        public ObservableCollection<AttributesListVm> AttributesList { get; set; }
        public AttributesListVm AttributeSelected { get; set; }

        public ObservableCollection<string> FunctionComboBox { get; set; }
        public string SelectedFunction
        {
            get;
            set;
        }


        public string LabelNameTB { get; set; }
        public ICommand OpenFunctionParamsWindow { get; set; }
        public ICommand AddSummarizer { get; set; }

        public FunctionSelectionVM FunctionSelectionVm { get; set; }

        public Summarizer SummarizerSelected { get; set; }

        public SummarizerVM(MainWindowVM parent)
        {
            Parent = parent;
            AttributesList = AtributesLoader.ConvertCoverToAtributesListVms(parent.Covers);
            FunctionComboBox = new ObservableCollection<string> {
                "Trapezoidal",
                "Triangular"
            };
            AddSummarizer = new RelayCommand(AddToSummarizers);
            OpenFunctionParamsWindow = new RelayCommand(ShowFunctionWindow);
        }

        private void AddToSummarizers()
        {
            if (FunctionSelectionVm.Function == null)
            {
                MessageBox.Show("Please setup function in configurator!");
                return;
            }
            AttributeSelected.Summarizers.Add(new Summarizer(LabelNameTB, new FuzzySet(FunctionSelectionVm.Function)));
        }

        private void ShowFunctionWindow()
        {
            if (string.IsNullOrEmpty(SelectedFunction))
            {
                MessageBox.Show("Please choose function type");
                return;
            }
            FunctionSelectionVm = new FunctionSelectionVM(SelectedFunction, AttributeSelected.Min, AttributeSelected.Max);
            FunctionSelectionWindow window = new FunctionSelectionWindow()
            {
                DataContext = FunctionSelectionVm
            };
            window.Show();

        }
    }
}