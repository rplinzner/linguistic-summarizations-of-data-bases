using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzy.Function
{
    public class TriangularFunction : IFunction
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }


        public TriangularFunction(double a, double b)
        {
            A = a;
            B = b;
            C = (b - a) / 2;
        }
        
        public double Range()
        {
            throw new NotImplementedException();
        }

        public double Value(double x)
        {
            throw new NotImplementedException();
        }
    }
}
