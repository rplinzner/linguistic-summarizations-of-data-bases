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
        public Summarizer SelectedQualifiers { get; set; }

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

        public void Summarize()
        {
            CovertypeConverter convert = new CovertypeConverter();
            List<Cover> subject = Parent.Covers.Where(x => x.CoverType == SelectedSubject).ToList();
            if (OneSummarizerWithoutQualifier())
            {
                OneSummarizer(subject, convert);
                return; 
            }
            if (TwoSummarizersWithoutQualifiers())
            {
                TwoSummarizers(subject, convert);
                return;
            }

            if (OneSummarizerWithQualifier())
            {
                OneSummarizerWithQ(subject, convert);
                return;
            }

            if (TwoSummarizersWithQualifier())
            {
                TwoSummarizersWithQ(subject, convert);
                return;
            }
           
        }

        private bool OneSummarizerWithoutQualifier() =>
            SelectedSummarizer != null && SelectedSecondSummarizer == null && SelectedQualifiers == null;

        private bool TwoSummarizersWithoutQualifiers() =>
            SelectedQualifiers == null && SelectedSecondSummarizer != null && SelectedSummarizer != null;
        private bool OneSummarizerWithQualifier() => SelectedSummarizer != null && SelectedSecondSummarizer == null && SelectedQualifiers != null;
        private bool TwoSummarizersWithQualifier() => SelectedQualifiers != null && SelectedSecondSummarizer != null && SelectedSummarizer != null;

        private void OneSummarizer(List<Cover> subject, CovertypeConverter convert)
        {
            List<int> ValuesForSummarizer = ExtractColumn(SelectedAttribute);
            foreach (var quantifier in Quantifiers)
            {
                string summarization = $"{quantifier} {convert.Convert(SelectedSubject, null, null, null)}";
                summarization += $" ARE/HAVE {SelectedSummarizer}";
                summarization = summarization.Replace("Trapezoidal", String.Empty);
                summarization = summarization.Replace("Triangular", String.Empty);
                Summary summary = new Summary()
                {
                    Description = summarization,
                    T1 = Math.Round(new DegreeOfTruth()
                    {
                        Summarizer1 = SelectedSummarizer,
                        Quantifier = quantifier,
                        Operation = SelectedConjunction,
                        ValuesForSummarizer1 = ValuesForSummarizer
                    }.Call(), 2),
                    T2 = Math.Round(new DegreeOfImprecision(new List<Summarizer>() { SelectedSummarizer }).Call(), 2),
                    T5 = Math.Round(new LengthOfSummary(new List<Summarizer>() { SelectedSummarizer }).Call(), 2),
                    T6 = Math.Round(new DegreeOfQuantifierImprecision(quantifier).Call(), 2),
                    T7 = Math.Round(new DegreeOfQuantifierCardinality(quantifier).Call(), 2),
                    T8 = Math.Round(new DegreeOfSummarizerCardinality(new List<Summarizer>() { SelectedSummarizer }).Call(), 2),
                    T9 = Math.Round(new DegreeOfQualifierImprecision().Call(), 2),
                    T10 = Math.Round(new DegreeOfQualifierCardinality().Call(), 2),
                    T11 = Math.Round(new LengthOfQualifier().Call(), 2),
                };
                summary.T3 = Math.Round(new DegreeOfCoverage()
                {
                    Summarizer1 = SelectedSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer
                }.Call(), 2);
                summary.T4 = Math.Round(new DegreeOfAppropriateness()
                {
                    T3 = summary.T3,
                    Summarizer1 = SelectedSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer
                }.Call(), 2);
                summary.CalculateT();

                Summaries.Add(summary);
            }
        }

        private void TwoSummarizers(List<Cover> subject, CovertypeConverter convert)
        {
            List<int> ValuesForSummarizer1 = ExtractColumn(SelectedAttribute);
            List<int> ValuesForSummarizer2 = ExtractColumn(SelectedSecondAttribute);

            foreach (var quantifier in Quantifiers)
            {
                string summarization = $"{quantifier} {convert.Convert(SelectedSubject, null, null, null)}";
                summarization += $" ARE/HAVE {SelectedSummarizer} {SelectedConjunction} {SelectedSecondSummarizer}";
                summarization = summarization.Replace("Trapezoidal", String.Empty);
                summarization = summarization.Replace("Triangular", String.Empty);
                Summary summary = new Summary()
                {
                    Description = summarization,
                    T1 = Math.Round(new DegreeOfTruth()
                    {
                        Summarizer1 = SelectedSummarizer,
                        Summarizer2 = SelectedSecondSummarizer,
                        Quantifier = quantifier,
                        ValuesForSummarizer1 = ValuesForSummarizer1,
                        ValuesForSummarizer2 = ValuesForSummarizer2,
                        Operation = SelectedConjunction
                    }.Call(), 2),
                    T2 = Math.Round(new DegreeOfImprecision(new List<Summarizer>() { SelectedSummarizer, SelectedSecondSummarizer }).Call(), 2),
                    T5 = Math.Round(new LengthOfSummary(new List<Summarizer>() { SelectedSummarizer, SelectedSecondSummarizer }).Call(), 2),
                    T6 = Math.Round(new DegreeOfQuantifierImprecision(quantifier).Call(), 2),
                    T7 = Math.Round(new DegreeOfQuantifierCardinality(quantifier).Call(), 2),
                    T8 = Math.Round(new DegreeOfSummarizerCardinality(new List<Summarizer>() { SelectedSummarizer, SelectedSecondSummarizer }).Call(), 2),
                    T9 = Math.Round(new DegreeOfQualifierImprecision().Call(), 2),
                    T10 = Math.Round(new DegreeOfQualifierCardinality().Call(), 2),
                    T11 = Math.Round(new LengthOfQualifier().Call(), 2),
                };
                summary.T3 = Math.Round(new DegreeOfCoverage()
                {
                    Summarizer1 = SelectedSummarizer,
                    Summarizer2 = SelectedSecondSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer1,
                    ValuesForSummarizer2 = ValuesForSummarizer2,
                    Operation = SelectedConjunction
                }.Call(), 2);
                summary.T4 = Math.Round(new DegreeOfAppropriateness()
                {
                    T3 = summary.T3,
                    Summarizer1 = SelectedSummarizer,
                    Summarizer2 = SelectedSecondSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer1,
                    ValuesForSummarizer2 = ValuesForSummarizer2
                }.Call(), 2);
                summary.CalculateT();

                Summaries.Add(summary);
            }
        }

        private void OneSummarizerWithQ(List<Cover> subject, CovertypeConverter convert)
        {
            List<int> ValuesForSummarizer = ExtractColumn(SelectedAttribute);

            foreach (var quantifier in Quantifiers)
            {
                string summarization = $"{quantifier} {convert.Convert(SelectedSubject, null, null, null)}";
                summarization += $" BEING/HAVING {SelectedQualifiers} ARE/HAVE {SelectedSummarizer}";
                summarization = summarization.Replace("Trapezoidal", String.Empty);
                summarization = summarization.Replace("Triangular", String.Empty);
                Summary summary = new Summary()
                {
                    Description = summarization,
                    T1 = Math.Round(new DegreeOfTruth()
                    {
                        Summarizer1 = SelectedSummarizer,
                        Quantifier = quantifier,
                        Qualifier = SelectedQualifiers,
                        Operation = SelectedConjunction,
                        ValuesForSummarizer1 = ValuesForSummarizer
                    }.Call(), 2),
                    T2 = Math.Round(new DegreeOfImprecision(new List<Summarizer>() { SelectedSummarizer }).Call(), 2),
                    T5 = Math.Round(new LengthOfSummary(new List<Summarizer>() { SelectedSummarizer }).Call(), 2),
                    T6 = Math.Round(new DegreeOfQuantifierImprecision(quantifier).Call(), 2),
                    T7 = Math.Round(new DegreeOfQuantifierCardinality(quantifier).Call(), 2),
                    T8 = Math.Round(new DegreeOfSummarizerCardinality(new List<Summarizer>() { SelectedSummarizer }).Call(), 2),
                    T9 = Math.Round(new DegreeOfQualifierImprecision(new List<Summarizer>() { SelectedQualifiers }).Call(), 2),
                    T10 = Math.Round(new DegreeOfQualifierCardinality(SelectedQualifiers).Call(), 2),
                    T11 = Math.Round(new LengthOfQualifier(new List<Summarizer>() { SelectedQualifiers }).Call(), 2),
                };
                summary.T3 = Math.Round(new DegreeOfCoverage()
                {
                    Summarizer1 = SelectedSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer,
                    Qualifier = SelectedQualifiers
                }.Call(), 2);
                summary.T4 = Math.Round(new DegreeOfAppropriateness()
                {
                    T3 = summary.T3,
                    Summarizer1 = SelectedSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer
                }.Call(), 2);
                summary.CalculateT();

                Summaries.Add(summary);
            }
        }

        private void TwoSummarizersWithQ(List<Cover> subject, CovertypeConverter convert)
        {
            List<int> ValuesForSummarizer1 = ExtractColumn(SelectedAttribute);
            List<int> ValuesForSummarizer2 = ExtractColumn(SelectedSecondAttribute);
            foreach (var quantifier in Quantifiers)
            {
                string summarization = $"{quantifier} {convert.Convert(SelectedSubject, null, null, null)}";
                summarization += $" BEING/HAVING {SelectedQualifiers} ARE/HAVE {SelectedSummarizer} {SelectedConjunction} {SelectedSecondSummarizer}";
                summarization = summarization.Replace("Trapezoidal", String.Empty);
                summarization = summarization.Replace("Triangular", String.Empty);
                Summary summary = new Summary()
                {
                    Description = summarization,
                    T1 = Math.Round(new DegreeOfTruth()
                    {
                        Summarizer1 = SelectedSummarizer,
                        Summarizer2 = SelectedSecondSummarizer,
                        Quantifier = quantifier,
                        Qualifier = SelectedQualifiers,
                        ValuesForSummarizer1 = ValuesForSummarizer1,
                        ValuesForSummarizer2 = ValuesForSummarizer2,
                        Operation = SelectedConjunction
                    }.Call(), 2),
                    T2 = Math.Round(new DegreeOfImprecision(new List<Summarizer>() { SelectedSummarizer, SelectedSecondSummarizer }).Call(), 2),
                    T5 = Math.Round(new LengthOfSummary(new List<Summarizer>() { SelectedSummarizer, SelectedSecondSummarizer }).Call(), 2),
                    T6 = Math.Round(new DegreeOfQuantifierImprecision(quantifier).Call(), 2),
                    T7 = Math.Round(new DegreeOfQuantifierCardinality(quantifier).Call(), 2),
                    T8 = Math.Round(new DegreeOfSummarizerCardinality(new List<Summarizer>() { SelectedSummarizer, SelectedSecondSummarizer }).Call(), 2),
                    T9 = Math.Round(new DegreeOfQualifierImprecision(new List<Summarizer>() { SelectedQualifiers }).Call(), 2),
                    T10 = Math.Round(new DegreeOfQualifierCardinality(SelectedQualifiers).Call(), 2),
                    T11 = Math.Round(new LengthOfQualifier(new List<Summarizer>() {SelectedQualifiers}).Call(), 2),
                };
                summary.T3 = Math.Round(new DegreeOfCoverage()
                {
                    Summarizer1 = SelectedSummarizer,
                    Summarizer2 = SelectedSecondSummarizer,
                    Qualifier = SelectedQualifiers,
                    ValuesForSummarizer1 = ValuesForSummarizer1,
                    ValuesForSummarizer2 = ValuesForSummarizer2,
                    Operation = SelectedConjunction
                }.Call(), 2);
                summary.T4 = Math.Round(new DegreeOfAppropriateness()
                {
                    T3 = summary.T3,
                    Summarizer1 = SelectedSummarizer,
                    Summarizer2 = SelectedSecondSummarizer,
                    ValuesForSummarizer1 = ValuesForSummarizer1,
                    ValuesForSummarizer2 = ValuesForSummarizer2
                }.Call(), 2);
                summary.CalculateT();

                Summaries.Add(summary);
            }
        }
    }
}