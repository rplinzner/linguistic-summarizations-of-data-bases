using Fuzzy.Set;
using System.Collections.Generic;

namespace Fuzzy.Summarizer
{
    public abstract class Base
    {
        public Dictionary<string, FuzzySet> FuzzySets;
        public Base()
        {
            FuzzySets = new Dictionary<string, FuzzySet>();
        }
        public void AddLinguisticValue(string label, FuzzySet fuzzySet) => FuzzySets.Add(label, fuzzySet);
        public void RemoveLinguisticValue(string label) => FuzzySets.Remove(label);
    }
}
