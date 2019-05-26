using Fuzzy.Set;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzy.Summarizer
{
    public class Summarizer : Base
    {
        public Summarizer(string label, FuzzySet fuzzySet) : base(label, fuzzySet) { }
    }
}
