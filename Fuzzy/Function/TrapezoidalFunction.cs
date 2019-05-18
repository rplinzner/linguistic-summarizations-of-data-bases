using System;

namespace Fuzzy.Function
{
    public class TrapezoidalFunction : IFunction
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }

        //   d    c
        //  a      b
        public TrapezoidalFunction(double a, double b, double c, double d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
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
    }
}
