using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Hardcodet.Wpf.TaskbarNotification;
using NHotkey;
using NHotkey.Wpf;

namespace HotkeyExperiment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskbarIcon tb;
        private WindowInteropHelper winHelp;
        private DXManager dx;

        public MainWindow()
        {
            InitializeComponent();

            tb = (TaskbarIcon)FindResource("NotifyIcon");

            HotkeyManager.Current.AddOrReplace("Toggle HDR", Key.H, ModifierKeys.Control | ModifierKeys.Windows, OnHotkey);
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            winHelp = new WindowInteropHelper(this);
            dx = new DXManager(winHelp.Handle);
        }

        private void OnHotkey(object sender, HotkeyEventArgs e)
        {
            Output.Text += "HDR State: " + dx.IsHdrActive() + " " + e.Name + "\n";
            e.Handled = true;
        }
    }
}
