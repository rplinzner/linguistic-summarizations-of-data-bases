using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fuzzy.Summarizer;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class AttributesListVm : BaseVM
    {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public ObservableCollection<Summarizer> Summarizers { get; set; } = new ObservableCollection<Summarizer>();

        public override string ToString()
        {
            return Name;
        }
    }
}