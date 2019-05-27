using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Fuzzy.Set;
using Fuzzy.Summarizer;
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

        public void AddToCollection()
        {
            if (FunctionSelectionVm.Function == null)
            {
                MessageBox.Show("Please setup function in configurator!");
                return;
            }
            Quantifiers.Add(new Quantifier(LabelNameTB, new FuzzySet(FunctionSelectionVm.Function)));
        }

        private void OnRemove()
        {
            if (QuantifierSelected == null)
            {
                MessageBox.Show("Please choose summarizer");
                return;
            }
            Quantifiers.Remove(QuantifierSelected);
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
            _window.Close();
        }
    }
}