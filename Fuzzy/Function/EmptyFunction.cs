﻿namespace Fuzzy.Function
{
    public class EmptyFunction : IFunction
    {
        public double Range() => 0;
        public double Value(double x) => 0;
        public double GetHeight() => 0;
        public double[] GetCore() => new double[2] { double.NaN, double.NaN };
        public double[] GetSupp() => new double[2] { double.NaN, double.NaN };
    }
}