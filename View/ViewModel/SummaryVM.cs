using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Input;
using Data;
using Fuzzy.Quality;
using Fuzzy.Summarizer;
using View.Converters;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class SummaryVM : BaseVM
    {
        private AttributesListVm _selectedAttribute;
        private AttributesListVm _selectedSecondAttribute;

        #region props

        public ObservableCollection<AttributesListVm> Attributes { get; set; }

        public AttributesListVm SelectedAttribute
        {
            get => _selectedAttribute;
            set
            {
                _selectedAttribute = value;
                Summarizers = value.Summarizers;
                CheckForNullElements();
            }
        }

        public AttributesListVm SelectedSecondAttribute
        {
            get => _selectedSecondAttribute;
            set
            {
                _selectedSecondAttribute = value;
                SecondSummarizers = value.Summarizers;
                CheckForNullElements();
            }
        }

        public ObservableCollection<Summarizer> Summarizers { get; set; }
        public Summarizer SelectedSummarizer { get; set; }

        public ObservableCollection<Summarizer> SecondSummarizers { get; set; }
        public Summarizer SelectedSecondSummarizer { get; set; }

        public ObservableCollection<int> Subjects { get; set; }
        public int SelectedSubject { get; set; }

        public ObservableCollection<Summarizer> Qualifiers { get; set; } = new ObservableCollection<Summarizer>();
        public Summarizer SelectedQualifier { get; set; }

        public ObservableCollection<string> Conjunctions { get; set; }
        public string SelectedConjunction { get; set; }

        public ObservableCollection<Summary> Summaries { get; set; } = new ObservableCollection<Summary>();

        public ICommand Button { get; set; }

        public ObservableCollection<Quantifier> Quantifiers { get; set; }

        public MainWindowVM Parent { get; set; }


        #endregion

        public SummaryVM(ObservableCollection<AttributesListVm> attributes,
            ObservableCollection<Quantifier> quantifiers, MainWindowVM parent)
        {
            Attributes = attributes;
            Quantifiers = quantifiers;
            Parent = parent;
            Conjunctions = new ObservableCollection<string>
            {
                "AND",
                "OR",
                ""
            };
            SelectedConjunction = Conjunctions[2];
            var temp = parent.Covers.Select(c => c.CoverType).Distinct().ToList();
            Subjects = new ObservableCollection<int>(temp);
            Button = new RelayCommand(Summarize);
        }

        private void CheckForNullElements()
        {
            if (SecondSummarizers == null || Qualifiers == null)
            {
                return;
            }

            var sum = SecondSummarizers.FirstOrDefault(t => string.IsNullOrEmpty(t.Label));
            var qual = Qualifiers.FirstOrDefault(t => string.IsNullOrEmpty(t.Label));

            if (sum == null)
            {
                SecondSummarizers.Add(new Summarizer("", null));
            }
            if (qual == null)
            {
                Qualifiers.Add(new Summarizer("", null));
            }

        }

        private List<int> ExtractColumn(AttributesListVm attr)
        {

            switch (attr.Name)
            {
                case "Elevation":
                    return Parent.Covers.Select(x => x.Elevation).ToList();
                case "Slope":
                    return Parent.Covers.Select(x => x.Slope).ToList();
                case "HorizontalDistanceToHydrology":
                    return Parent.Covers.Select(x => x.HorizontalDistanceToHydrology).ToList();
                case "VerticalDistanceToHydrology":
                    return Parent.Covers.Select(x => x.VerticalDistanceToHydrology).ToList();
                case "HorizontalDistanceToRoadways":
                    return Parent.Covers.Select(x => x.HorizontalDistanceToRoadways).ToList();
                case "Hillshade9Am":
                    return Parent.Covers.Select(x => x.Hillshade9Am).ToList();
                case "HillshadeNoon":
                    return Parent.Covers.Select(x => x.HillshadeNoon).ToList();
                case "Hillshade3Pm":
                    return Parent.Covers.Select(x => x.Hillshade3Pm).ToList();
                case "HorizontalDistanceToFirePoints":
                    return Parent.Covers.Select(x => x.HorizontalDistanceToFirePoints).ToList();
                case "Aspect":
                    return Parent.Covers.Select(x => x.Aspect).ToList();
            }

            return null;
        }
     

        private void Summarize()
        {
            if (string.IsNullOrEmpty(SelectedSummarizer.Label))
            {
                MessageBox.Show("Please Choose Summarizer");
                return;
            }

            List<int> ValuesForSummarizer = ExtractColumn(SelectedAttribute);
            List<int> ValuesForSummarizer2 = null;

            bool hasQualifier = SelectedQualifier != null && !string.IsNullOrEmpty(SelectedQualifier.Label);
            bool hasTwoSummarizers = SelectedSecondSummarizer != null && !string.IsNullOrEmpty(SelectedSecondSummarizer.Label);
            if (hasTwoSummarizers) //if second summarizer is selected
            {
                ValuesForSummarizer2 = ExtractColumn(SelectedSecondAttribute);
            }

            CovertypeConverter convert = new CovertypeConverter();
            List<Cover> subject = Parent.Covers.Where(x => x.CoverType == SelectedSubject).ToList();


            foreach (var quantifier in Quantifiers)
            {
                var sum = GetSummary(quantifier, hasQualifier, hasTwoSummarizers, convert);
                var t1 = new DegreeOfTruth()
                {
                    Summarizer1 = SelectedSummarizer,
                    Quantifier = quantifier,
                    Operation = SelectedConjunction,
                    ValuesForSummarizer1 = ValuesForSummarizer
                };
                var t2 = new DegreeOfImprecision(new List<Summarizer>() { SelectedSummarizer });
                var t3 = new DegreeOfCoverage()
                {
                    Summarizer1 = SelectedSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer,
                    Operation = SelectedConjunction
                };
                var t4 = new DegreeOfAppropriateness()
                {
                    Summarizer1 = SelectedSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer
                };
                var t5 = new LengthOfSummary(new List<Summarizer>() { SelectedSummarizer });
                var t6 = new DegreeOfQuantifierImprecision(quantifier);
                var t7 = new DegreeOfQuantifierCardinality(quantifier);
                var t8 = new DegreeOfSummarizerCardinality(new List<Summarizer>() { SelectedSummarizer });
                var t9 = new DegreeOfQualifierImprecision();
                var t10 = new DegreeOfQualifierCardinality();
                var t11 = new LengthOfQualifier();
                if (hasQualifier) //If Qualifier Is Applied
                {
                    t1.Qualifier = SelectedQualifier;
                    t3.Qualifier = SelectedQualifier;
                    t9 = new DegreeOfQualifierImprecision(new List<Summarizer> { SelectedQualifier });
                    t10 = new DegreeOfQualifierCardinality(SelectedQualifier);
                    t11 = new LengthOfQualifier(new List<Summarizer>() { SelectedQualifier });
                }
                if (hasTwoSummarizers) //if second summarizer is selected
                {
                    t1.Summarizer2 = SelectedSecondSummarizer;
                    t1.ValuesForSummarizer2 = ValuesForSummarizer2;

                    t2.Summarizers.Add(SelectedSecondSummarizer);

                    t3.Summarizer2 = SelectedSecondSummarizer;
                    t3.ValuesForSummarizer2 = ValuesForSummarizer2;

                    t4.Summarizer2 = SelectedSecondSummarizer;
                    t4.ValuesForSummarizer2 = ValuesForSummarizer2;

                    t5.Summarizers.Add(SelectedSecondSummarizer);

                    t8.Summarizers.Add(SelectedSecondSummarizer);
                }

                t4.T3 = t3.Call();

                Summary summary = new Summary()
                {
                    Description = sum,
                    T1 = Math.Round(t1.Call(), 2),
                    T2 = Math.Round(t2.Call(), 2),
                    T3 = Math.Round(t3.Call(), 2),
                    T4 = Math.Round(t4.Call(), 2),

                    T5 = Math.Round(t5.Call(), 2),
                    T6 = Math.Round(t6.Call(), 2),
                    T7 = Math.Round(t7.Call(), 2),
                    T8 = Math.Round(t8.Call(), 2),
                    T9 = Math.Round(t9.Call(), 2),
                    T10 = Math.Round(t10.Call(), 2),
                    T11 = Math.Round(t11.Call(), 2),
                };
                summary.CalculateT();
                Summaries.Add(summary);
            }

        }

        private string GetSummary(Quantifier quantifier, bool hasQualifier, bool hasTwoSummarizers, CovertypeConverter convert)
        {
            var ret = $"{quantifier} {convert.Convert(SelectedSubject, null, null, null)}";

            if (hasQualifier)
            {
                ret += $" BEING/HAVING {SelectedQualifier}";
            }

            ret += $" ARE / HAVE {SelectedSummarizer}";

            if (hasTwoSummarizers)
            {
                ret += $" {SelectedConjunction} {SelectedSecondSummarizer}";
            }
            ret = ret.Replace(" Trapezoidal", String.Empty);
            ret = ret.Replace(" Triangular", String.Empty);

            return ret;
        }
        
    }
}