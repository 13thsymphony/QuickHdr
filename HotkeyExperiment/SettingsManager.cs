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
        private bool LaunchSettingsApp()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "ms-settings:display",
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Minimized
            };
            var proc = Process.Start(psi);

            SettingsApp = AutomationElement.RootElement.FindFirst
                (TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Settings"));

            return (SettingsApp != null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Whether the operation succeeded</returns>
        public bool SetHdrToggle()
        {
            if (!LaunchSettingsApp())
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
            toggle.Current.
            toggle.Toggle();

            return true;
        }
    }

    struct HdrResult
    {
        public bool WasToggleSuccessful;
        public bool CurrentHdrState;
    }
}
