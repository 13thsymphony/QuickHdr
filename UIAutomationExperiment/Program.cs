using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;

namespace UIAutomationExperiment
{
    class Program
    {
        [STAThread]

        static void Main(string[] args)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "ms-settings:display",
                UseShellExecute = true
            };
            var proc = Process.Start(psi);

            if (proc == null)
            {
                Console.WriteLine("Failed to launch Settings > System > Display page");
            }

            AutomationElement settingsApp = AutomationElement.RootElement.FindFirst
                (TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Settings"));

            if (settingsApp == null)
            {
                Console.WriteLine("Could not access Settings app");
            }

            //WalkEnabledElementsWithLimit(settingsApp, 5);

            HdrToggle = settingsApp.FindFirst
                (TreeScope.Descendants, new PropertyCondition
                    (AutomationElement.AutomationIdProperty, "SystemSettings_Display_AdvancedColorSupport_ToggleSwitch"));

            HotkeyManager.Current.AddOrReplace("Toggle HDR", Key.H, ModifierKeys.Windows | ModifierKeys.Control, OnHdrToggle);

            Console.WriteLine("Awaiting CTRL+WIN+H hotkey. Press any key to exit.");

            Console.ReadLine();

            //ToggleHdrState();
        }

        static private AutomationElement HdrToggle;

        static private void OnHdrToggle(object sender, HotkeyEventArgs e)
        {
            if (HdrToggle == null)
            {
                return;
            }

            TogglePattern toggle = (TogglePattern)HdrToggle.GetCurrentPattern(TogglePattern.Pattern);
            ToggleState currState = toggle.Current.ToggleState;
            toggle.Toggle();

            e.Handled = true;
        }

        /// <summary>
        /// Walks the UI Automation tree of an element recursively. Limits its recursion depth to levelToRecurse.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="levelsToRecurse"></param>
        static private void WalkEnabledElementsWithLimit(AutomationElement element, int levelsToRecurse)
        {
            if (levelsToRecurse <= 0)
            {
                return;
            }

            Console.WriteLine(levelsToRecurse + ": " + element.Current.Name + " " + element.Current.ClassName);

            if (element.Current.AutomationId == "SystemSettings_Display_AdvancedColorSupport_ToggleSwitch")
            {
                
            }

            Condition condition1 = new PropertyCondition(AutomationElement.IsControlElementProperty, true);
            Condition condition2 = new PropertyCondition(AutomationElement.IsEnabledProperty, true);
            TreeWalker walker = new TreeWalker(new AndCondition(condition1, condition2));
            AutomationElement elementNode = walker.GetFirstChild(element);
            while (elementNode != null)
            {
                WalkEnabledElementsWithLimit(elementNode, levelsToRecurse - 1);
                elementNode = walker.GetNextSibling(elementNode);
            }
        }
    }
}
