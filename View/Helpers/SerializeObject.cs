using System.Collections.ObjectModel;
using Fuzzy.Summarizer;

namespace View.ViewModel
{
    public class SerializeObject
    {
        public ObservableCollection<AttributesListVm> SummarizerAttributesList { get; set; }
        public ObservableCollection<Quantifier> Quantifiers { get; set; }

    }
}