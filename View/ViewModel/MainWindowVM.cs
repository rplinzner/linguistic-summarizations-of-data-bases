using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Data;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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

        #endregion

        #region OtherVM

        public SetupVM SetupVm { get; set; }

        #endregion

        public MainWindowVM()
        {
            ReadWindowsSetting();
            ApplyBase(_isDarkTheme);
            Covers = CoverRepository.All();
            SetupVm = new SetupVM(this);
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

    }
}