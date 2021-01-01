using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace HotkeyExperiment
{
    /// <summary>
    /// Provides support for driving the Settings app to change HDR settings. Uses UI Automation.
    /// </summary>
    class SettingsManager
    {
        private AutomationElement SettingsApp;
        private DXManager DX;

        public SettingsManager(DXManager dx)
        {
            DX = dx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Whether we have a valid Automation handle to the Settings Display page</returns>
        private async Task<bool> LaunchSettingsAppAsync()
        {
            bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:display"));

            //var psi = new ProcessStartInfo
            //{
            //    FileName = "ms-settings:display",
            //    UseShellExecute = false,
            //    WindowStyle = ProcessWindowStyle.Minimized
            //};
            //var proc = Process.Start(psi);

            await Task.Delay(300); // Workaround to ensure that UI has been fully loaded.

            SettingsApp = AutomationElement.RootElement.FindFirst
                (TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Settings"));

            return (SettingsApp != null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Whether the operation succeeded</returns>
        public async Task<bool> ToggleHdr()
        {
            if (!await LaunchSettingsAppAsync())
            {
                Debug.WriteLine("Could not activate Settings app");
                return false;
            }

            AutomationElement hdrToggle = SettingsApp.FindFirst
                (TreeScope.Descendants, new PropertyCondition
                    (AutomationElement.AutomationIdProperty, "SystemSettings_Display_AdvancedColorSupport_ToggleSwitch"));

            if (hdrToggle == null)
            {
                Debug.WriteLine("Could not access HDR toggle UI element");
                return false;
            }

            TogglePattern toggle = (TogglePattern)hdrToggle.GetCurrentPattern(TogglePattern.Pattern);
            toggle.Toggle();

            return true;
        }
    }
}
