
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
        
        public IFunctionSelector Parent { get; set; }
        public bool IsAddButtonVisible { get; set; } = true;
        public FunctionSelectionVM(string function, int min, int max, IFunctionSelector parent)
        {
            _function = function;
            Save = new RelayCommand(OnSave);
            MinTB = min;
            MaxTB = max;
            Parent = parent;
        }

        public FunctionSelectionVM(IFunction function, int min, int max)
        {
            IsAddButtonVisible = false;
            MinTB = min;
            MaxTB = max;
            if (function.GetType() == typeof(TriangularFunction))
            {
                var vals = function.GetValues();
                ATB = vals[0];
                BTB = vals[1];
                CTB = vals[2];

            }
            else
            {
                var vals = function.GetValues();
                ATB = vals[0];
                BTB = vals[1];
                CTB = vals[2];
                DTB = vals[3];
            }
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
            Parent.AddToCollection();
            Parent.Close();
            
        }

    }
}