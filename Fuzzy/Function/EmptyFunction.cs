using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzy.Function
{
    public class EmptyFunction : IFunction
    {
        public double Range() => 0;
        public double Value(double x) => 0;
        public double GetHeight() => 0;
    }
}
