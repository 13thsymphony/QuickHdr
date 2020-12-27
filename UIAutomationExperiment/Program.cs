using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace UIAutomationExperiment
{
    class Program
    {
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

            WalkEnabledElementsWithLimit(settingsApp, 5);
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

            Console.WriteLine(levelsToRecurse + ": " + element.Current.Name);

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
