﻿using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace QuickHdr
{
    /// <summary>
    /// Provides support for driving the Settings app to change HDR settings. Uses UI Automation.
    /// </summary>
    class SettingsManager
    {
        private AutomationElement SettingsApp;
        private DXManager DX;
        private TaskbarIcon TB;
        private AutomationPropertyChangedEventHandler PropHandler;

        public SettingsManager(DXManager dx, TaskbarIcon tb)
        {
            DX = dx;
            TB = tb;
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
        public async Task<bool> ToggleHdrAsync()
        {
            // Unsure if this is needed. I'm not sure if the event handler will be left dangling in error cases.
            Automation.RemoveAllEventHandlers();

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

            // TODO: The toggle is immediately updated before the mode change completes, so it's not useful to us.
            //Automation.AddAutomationPropertyChangedEventHandler(hdrToggle, TreeScope.Element,
            //     PropHandler = new AutomationPropertyChangedEventHandler(OnHdrToggleUpdated),
            //     TogglePattern.ToggleStateProperty);

            return true;
        }

        /// <summary>
        /// TODO: This method isn't used for now.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        private void OnHdrToggleUpdated(object src, AutomationPropertyChangedEventArgs e)
        {
            // Make sure the element still exists. Elements such as tooltips
            // can disappear before the event is processed.
            AutomationElement sourceElement;
            try
            {
                sourceElement = src as AutomationElement;

                ToggleState state = (ToggleState)e.NewValue;

                switch (state)
                {
                    case ToggleState.Off:
                        Debug.WriteLine("HDR state: OFF");
                        break;

                    case ToggleState.On:
                        Debug.WriteLine("HDR state: ON");
                        break;

                    default:
                        Debug.WriteLine("HDR state: UNKNOWN");
                        break;
                }
            }
            catch (ElementNotAvailableException)
            {
            }
            finally
            {
                Automation.RemoveAllEventHandlers();
            }

        }
    }
}