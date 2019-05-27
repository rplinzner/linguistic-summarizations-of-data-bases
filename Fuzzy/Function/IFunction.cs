using System.Collections.Generic;

namespace Fuzzy.Function
{
    public interface IFunction
    {
        double Value(double x);
        double Range();
        double GetHeight();
        double SupportCardinality();
        double DomainCardinality();
        double Cardinality();
        List<double> GetValues();
        double[] GetCore();
        double[] GetSupp();
    }
}
