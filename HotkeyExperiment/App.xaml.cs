
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;
using NHotkey;
using NHotkey.Wpf;

namespace HotkeyExperiment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon TbIcon;
        private DXManager DX;
        private SettingsManager Settings;
        private MainWindow MWin;
        IntPtr Hwnd;

        public App()
        {
            InitializeComponent();

            TbIcon = (TaskbarIcon)FindResource("TaskbarIconRes");

            HotkeyManager.Current.AddOrReplace("Toggle HDR", Key.H, ModifierKeys.Control | ModifierKeys.Windows, OnHotkey);

            MWin = new MainWindow();
            // Don't show the window as its only purpose is to provide an HWND.
            Hwnd = MWin.GetWindowHandle();

            DX = new DXManager(Hwnd);
            Settings = new SettingsManager(DX);
        }

        private void OnHotkey(object sender, HotkeyEventArgs e)
        {
            var result = Settings.ToggleHdr();
            Debug.WriteLine("Hotkey CTRL+WIN+H invoked. Was successful: " + result);

            e.Handled = true;
        }

        public void ToggleHdr()
        {
            var result = Settings.ToggleHdr();
            Debug.WriteLine("ToggleHdr called. Was successful: " + result);
        }
    }
}
