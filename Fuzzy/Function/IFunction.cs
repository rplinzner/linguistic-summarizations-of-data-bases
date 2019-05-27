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
        double[] GetCore();
        double[] GetSupp();
    }
}
