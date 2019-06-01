using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Data;
using Fuzzy.Summarizer;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serialization;
using View.ViewModel.Base;

namespace View.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        #region private fields

        #endregion
        #region Theme related fields
        private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private const string RegistryValueName = "AppsUseLightTheme";

        private bool _isDarkTheme = true;

        #endregion



        #region Props

        public List<Cover> Covers { get; set; }
        public ICommand Save { get; set; }
        public ICommand Open { get; set; }

        #endregion

        #region OtherVM

        public SummarizerVM SummarizerVm { get; set; }
        public QuantifierVM QuantifierVm { get; set; }
        public SummaryVM SummaryVm { get; set; }

        #endregion

        public MainWindowVM()
        {
            ReadWindowsSetting();
            ApplyBase(_isDarkTheme);
            Covers = CoverRepository.All();
            SummarizerVm = new SummarizerVM(this);
            QuantifierVm = new QuantifierVM(this);
            SummaryVm = new SummaryVM(SummarizerVm.AttributesList, QuantifierVm.Quantifiers, this);
            Save = new RelayCommand(OnSave);
            Open = new RelayCommand(OnOpen);
        }

        #region Theme Solving Methods
        private void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        private void ReadWindowsSetting()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                object registryValueObject = key?.GetValue(RegistryValueName);
                if (registryValueObject == null)
                {
                    _isDarkTheme = false;
                }

                int registryValue;
                if (registryValueObject == null)
                {
                    registryValue = 0;
                }
                else
                {
                    registryValue = (int)registryValueObject;
                }

                _isDarkTheme = registryValue <= 0;
            }
        }


        #endregion


        private void OnSave()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = "json",
                FileName = "data",
                Filter = "JSON files (*.json)|*.json",
                RestoreDirectory = true
            };

            var result = sfd.ShowDialog();
            if (result == true)
            {
                SerializeObject data = new SerializeObject()
                {
                    Quantifiers = QuantifierVm.Quantifiers,
                    SummarizerAttributesList = SummarizerVm.AttributesList
                };
                JsonSerializer.Serialize<SerializeObject>(data, sfd.FileName);
            }
        }

        private void OnOpen()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = "data",
                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json",
                RestoreDirectory = true
            };
            var result = ofd.ShowDialog();
            if (result == true)
            {
                var data = JsonSerializer.Deserialize<SerializeObject>(ofd.FileName);
                QuantifierVm.Quantifiers = data.Quantifiers;
                SummarizerVm.AttributesList = data.SummarizerAttributesList;
            }
            SummaryVm = new SummaryVM(SummarizerVm.AttributesList, QuantifierVm.Quantifiers, this);
           // var temp = SummarizerVm.AttributesList.Select(c => c.Summarizers).ToList();
            QuantifierVm.Draw();
//            foreach (var VARIABLE in temp)
//            {
//                foreach (var summarizer in VARIABLE)
//                {
//                    SummaryVm.Qualifiers.Add(summarizer);
//                }
//            }
//            SummaryVm.Qualifiers.Add(new Summarizer("", null));
        }

    }
}