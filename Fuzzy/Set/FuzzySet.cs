using Fuzzy.Function;
using System;

namespace Fuzzy.Set
{
    public class FuzzySet
    {
       public IFunction MembershipFunction { get; set; }

        public FuzzySet(IFunction membershipFunction)
        {
            MembershipFunction = membershipFunction;
        }

        public double Membership(double x)
        {
            return MembershipFunction.Value(x);
        }

        public double Union(FuzzySet other, double x)
        {
            return Math.Max(Membership(x), other.Membership(x));
        }

        public double Intersection(FuzzySet other, double x)
        {
            return Math.Min(Membership(x), other.Membership(x));
        }

        public double TNorm(FuzzySet other, double x, double y)
        {
            return Math.Min(Membership(x), other.Membership(y));
        }

        public double SNorm(FuzzySet other, double x, double y)
        {
            return Math.Max(Membership(x), other.Membership(y));
        }

        public bool Equals(FuzzySet other, double x)
        {
            return Membership(x).Equals(other.Membership(x));
        }
    }
}
