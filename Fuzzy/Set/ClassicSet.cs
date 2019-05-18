using System.Collections.Generic;
using System.Linq;

namespace Fuzzy.Set
{
    public class ClassicSet : ISet<ClassicSet>
    {
        public List<int> Values { get; set; }

        public ClassicSet(IList<int> list)
        {
            Values = new List<int>(list);
        }

        public ClassicSet Multiplication(ClassicSet other)
        {
            return new ClassicSet(Values.Intersect(other.Values).ToList());
        }

        public ClassicSet Sum(ClassicSet other)
        {
            return new ClassicSet(Values.Union(other.Values).ToList());
        }

        public bool Equals(ClassicSet other)
        {
            return !Values.Except(other.Values).Any();
        }
    }
}
