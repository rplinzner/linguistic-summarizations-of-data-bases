using System.Windows;
using System.Windows.Input;
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

        #region Command Props

        public ICommand DoSomethingPlox { get; }

        #endregion

        #region Props

        

        #endregion

        public MainWindowVM()
        {
            ReadWindowsSetting();
            ApplyBase(_isDarkTheme);
            DoSomethingPlox = new RelayCommand(() =>
            {
                MessageBox.Show("But I don't know what :(");
            });
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