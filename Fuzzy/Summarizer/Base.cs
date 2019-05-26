﻿using Fuzzy.Set;
using System.Collections.Generic;

namespace Fuzzy.Summarizer
{
    public abstract class Base
    {
        public string Label { get; set; }
        public FuzzySet FuzzySet { get; set; }
        public Base(string label, FuzzySet fuzzySet)
        {
            Label = label;
            FuzzySet = fuzzySet;
        }
    }
}
