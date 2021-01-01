using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// The only purpose for having a Window is to provide the DXManager with a Window handle (HWND).
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowInteropHelper winHelp;

        public MainWindow()
        {
            InitializeComponent();

            winHelp = new WindowInteropHelper(this);
            winHelp.EnsureHandle();
        }

        public IntPtr GetWindowHandle()
        {
            return winHelp.Handle;
        }
    }
}
