using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            }
        }

        public AttributesListVm SelectedSecondAttribute
        {
            get => _selectedSecondAttribute;
            set
            {
                _selectedSecondAttribute = value;
                SecondSummarizers = value.Summarizers;
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
                "OR"
            };
            var temp = parent.Covers.Select(c => c.CoverType).Distinct().ToList();
            Subjects = new ObservableCollection<int>(temp);
            Button = new RelayCommand(Summarize);
        }

        public void Summarize()
        {
            CovertypeConverter convert = new CovertypeConverter();
            List<Cover> subject = Parent.Covers.Where(x => x.CoverType == SelectedSubject).ToList();
            ObservableCollection<Summary> summaries = new ObservableCollection<Summary>();
            foreach (var quantifier in Quantifiers)
            {
                string summarization = $"{quantifier} {convert.Convert(SelectedSubject, null, null, null)}";
                summarization += $" ARE/HAVE {SelectedSummarizer}";
                summarization = summarization.Replace("Trapezoidal", String.Empty);
                summarization = summarization.Replace("Triangular", String.Empty);
                Summary summary = new Summary()
                {
                    Description = summarization,
                    T1 = new DegreeOfTruth()
                    {
                        Summarizer1 = SelectedSummarizer, Quantifier = quantifier,
                        ValuesForSummarizer1 = Parent.Covers.Select(x => x.Aspect).ToList()
                    }.Call(),
                    T2 = new DegreeOfImprecision(new List<Summarizer>() {SelectedSummarizer}).Call(),
                    T5 = new LengthOfSummary(new List<Summarizer>() {SelectedSummarizer}).Call(),
                    T6 = new DegreeOfQuantifierImprecision(quantifier).Call(),
                    T7 = new DegreeOfQuantifierCardinality(quantifier).Call(),
                    T8 = new DegreeOfSummarizerCardinality(new List<Summarizer>() {SelectedSummarizer}).Call(),
                    T9 = new DegreeOfQualifierImprecision().Call(),
                    T10 = new DegreeOfQualifierImprecision().Call(),
                    T11 = new LengthOfQualifier().Call(),
                };
                summary.CalculateT();


                summaries.Add(summary);
            }

//            foreach (var quantifier in Quantifiers)
//            {
//                string summarization = $"{quantifier} {convert.Convert(SelectedSubject, null, null, null)}";
//                //if (SelectedQualifiers != null)
//                //{
//                //    summarization += $" BEING/HAVING {SelectedQualifiers}";
//                //}
//                summarization += $" ARE/HAVE ${SelectedSecondSummarizer}";
//
//                summaries.Add(new Summary()
//                {
//                    Description = summarization,
//                    T2 = new DegreeOfImprecision(new List<Summarizer>() {SelectedSecondSummarizer}).Call(),
//                    T5 = new LengthOfSummary(new List<Summarizer>() {SelectedSecondSummarizer}).Call(),
//                    T6 = new DegreeOfQuantifierImprecision(quantifier).Call(),
//                    T7 = new DegreeOfQuantifierCardinality(quantifier).Call(),
//                    T8 = new DegreeOfSummarizerCardinality(new List<Summarizer>() {SelectedSecondSummarizer}).Call(),
//                    T9 = new DegreeOfQualifierImprecision().Call(),
//                    T10 = new DegreeOfQualifierImprecision().Call(),
//                    T11 = new LengthOfQualifier().Call(),
//                });
//            }

//            foreach (var quantifier in Quantifiers)
//            {
//                string summarization = $"{quantifier} {convert.Convert(SelectedSubject, null, null, null)}";
//                //if (SelectedQualifiers != null)
//                //{
//                //    summarization += $" BEING/HAVING {SelectedQualifiers}";
//                //}
//                summarization += $" ARE/HAVE ${SelectedSecondSummarizer}";
//
//                Summary summary = new Summary()
//                {
//                    Description = summarization
//                };
//            }
        }



    }
}