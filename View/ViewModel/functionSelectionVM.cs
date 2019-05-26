using System;
using System.Windows.Input;
using Fuzzy.Function;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class FunctionSelectionVM : BaseVM   
    {
        public int MinTB { get; set; }
        public int MaxTB { get; set; }

        public double ATB { get; set; }
        public double BTB { get; set; }
        public double CTB { get; set; }
        public double DTB { get; set; }
        public IFunction Function { get; set; }
        private string _function;
        public ICommand Save { get; set; }

        public FunctionSelectionVM(string function, int min, int max)
        {
            _function = function;
            Save = new RelayCommand(OnSave);
            MinTB = min;
            MaxTB = max;
        }

        private void OnSave()
        {
            switch (_function)
            {
                case "Triangular":
                    Function = new TriangularFunction(ATB, BTB);
                    break;
                case "Trapezoidal":
                    Function = new TrapezoidalFunction(ATB, BTB, CTB, DTB);
                    break;
            }
            
        }

    }
}