using System.Linq;

namespace View.ViewModel
{
    public class Summary
    {
        public string Description { get; set; }
        public double T1 { get; set; }
        public double T2 { get; set; }
        public double T3 { get; set; }
        public double T4 { get; set; }
        public double T5 { get; set; }
        public double T6 { get; set; }
        public double T7 { get; set; }
        public double T8 { get; set; }
        public double T9 { get; set; }
        public double T10 { get; set; }
        public double T11 { get; set; }
        public double T { get; set; }
        public double Weight { get; set; } = 0.09;
        public void CalculateT()
        {
            T = ((T1 + T2 + T3 + T4 + T5 + T6 + T7 + T8 + T9 + T10 + T11) * Weight) / 11;
        }
    }
}