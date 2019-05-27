using System;

namespace Fuzzy.Function
{
    public class TriangularFunction : IFunction
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double Height { get; set; }

        // c
        //a b

       
        public TriangularFunction(double a, double b, double height = 1.0)
        {
            A = a;
            B = b;
            C = A + ((B - A) / 2.0);
            Height = height;
        }
        
        public double Range()
        {
            return Math.Abs(B - A);
        }

        public double Value(double x)
        {
            if (x < A || x > B) return 0;
            if (Math.Abs(x) == C) return 1;
            if (x > A && x < C) return (x - A) / (C - A);
            if (x > C && x < B) return (B - x) / (B - C);
            return 0;
        }

        public double GetHeight() => Height;
        public double SupportCardinality() => (Math.Abs(B - A) * Height) / 2.0;
        public double DomainCardinality() => Math.Abs(B - A);
        public double Cardinality() => SupportCardinality();

        public double[] GetCore()
        {
            throw new NotImplementedException();
        }

        public double[] GetSupp()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Triangular";
        }
    }
}
