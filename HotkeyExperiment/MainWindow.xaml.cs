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
    /// Also is used as DataContext for NotifyIcon.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowInteropHelper winHelp;
        private DXManager dx;

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

        private void ToggleHdr()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "ms-settings:display",
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Minimized
            };
            var proc = Process.Start(psi);

            //if (proc == null)
            //{
            //    Debug.WriteLine("Failed to launch Settings > System > Display page");
            //    return;
            //}

            AutomationElement settingsApp = AutomationElement.RootElement.FindFirst
                (TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Settings"));

            if (settingsApp == null)
            {
                Debug.WriteLine("Could not access Settings app");
                return;
            }

            // TODO: Why is the settings app not being minimized, and why don't I get a proc?

            AutomationElement hdrToggle = settingsApp.FindFirst
                (TreeScope.Descendants, new PropertyCondition
                    (AutomationElement.AutomationIdProperty, "SystemSettings_Display_AdvancedColorSupport_ToggleSwitch"));

            if (hdrToggle == null)
            {
                Debug.WriteLine("Could not find HDR toggle");
                return;
            }

            TogglePattern toggle = (TogglePattern)hdrToggle.GetCurrentPattern(TogglePattern.Pattern);
            ToggleState currState = toggle.Current.ToggleState;
            toggle.Toggle();

            Debug.WriteLine("HDR State: " + dx.IsHdrActive());
        }

        public ICommand ExitCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.Shutdown()
                };
            }
        }

        public ICommand ToggleHdrCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => ToggleHdr(),
                    CanExecuteFunc = () => dx != null
                };
            }
        }

        public ICommand MoreInfoCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Process.Start("https://13thsymphony.github.io")
                };
            }
        }
    }
}
