using Fuzzy.Summarizer;

namespace Fuzzy.Quality
{
    public class DegreeOfQuantifierImprecision : IDegree
    {
        public Quantifier Quantifier { get; set; }

        public DegreeOfQuantifierImprecision(Quantifier quantifier)
        {
            Quantifier = quantifier;
        }
        public double Call()
        {
            return 1.0 - Quantifier.FuzzySet.DegreeOfFuzziness();
        }
    }
}
