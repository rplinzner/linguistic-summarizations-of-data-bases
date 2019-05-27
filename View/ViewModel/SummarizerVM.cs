using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Fuzzy.Function;
using Fuzzy.Set;
using Fuzzy.Summarizer;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class SummarizerVM : BaseVM, IFunctionSelector
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
        public ICommand Details { get; }
        public ICommand Remove { get; }
        

        public FunctionSelectionVM FunctionSelectionVm { get; set; }

        public Summarizer SummarizerSelected { get; set; }
        private FunctionSelectionWindow _window;

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
            Details=new RelayCommand(OnDetails);
        }

        public void AddToCollection()
        {
            if (FunctionSelectionVm.Function == null)
            {
                MessageBox.Show("Please setup function in configurator!");
                return;
            }
            AttributeSelected.Summarizers.Add(new Summarizer(LabelNameTB, new FuzzySet(FunctionSelectionVm.Function)));
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