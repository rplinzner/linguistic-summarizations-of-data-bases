using System;
using System.Collections.Generic;

namespace Fuzzy.Function
{
    public class TrapezoidalFunction : IFunction
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        public double Height { get; set; }
        //   d    c
        //  a      b

        public TrapezoidalFunction()
        {
            
        }
        public TrapezoidalFunction(double a, double b, double c, double d, double height = 1)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            Height = height;
        }

        public double Value(double x)
        {
            if (x >= D && x <= C) return 1;
            if (x > A && x < D) return (x - A) / (D - A);
            if (x > C && x < B) return (B - x) / (B - C);
            return 0;
        }

        public double Range()
        {
            return Math.Abs(B - A);
        }

        public double GetHeight() => Height;
        public double SupportCardinality() => ((Math.Abs(B - A) + Math.Abs(D - C)) * Height) / 2.0;
        public double DomainCardinality() => Math.Abs(B - A);
        public double Cardinality() => SupportCardinality();
        public List<double> GetValues() => new List<double>() {A, B, C, D};

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
            return "Trapezoidal";
        }
    }
}
