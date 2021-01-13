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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Automation;

namespace UIAutomationWalker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AutomationElement appWindow = AutomationElement.RootElement.FindFirst
                (TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, TxtWindowName.Text));

            if (appWindow == null)
            {
                Output.Text = "Could not find window: " + TxtWindowName.Text;
            }
            else
            {
                Output.Text = "Walking UI tree of: " + TxtWindowName.Text;
            }

            WalkEnabledElementsWithLimit(appWindow, UIRootElement, Int32.Parse(RecursionCombo.Text));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="tvi">Writes the UI element info to this TreeViewItem's header</param>
        /// <param name="levelsToRecurse"></param>
        private void WalkEnabledElementsWithLimit(AutomationElement element, TreeViewItem tvi, int levelsToRecurse)
        {
            if (levelsToRecurse <= 0)
            {
                return;
            }

            tvi.Header = element.Current.Name + ":" + element.Current.AutomationId;
            tvi.IsExpanded = true;

            var condition1 = new PropertyCondition(AutomationElement.IsControlElementProperty, true);
            var condition2 = new PropertyCondition(AutomationElement.IsEnabledProperty, true);
            var walker = new TreeWalker(new AndCondition(condition1, condition2));
            var elementNode = walker.GetFirstChild(element);
            while (elementNode != null)
            {
                var newTvi = new TreeViewItem();
                tvi.Items.Add(newTvi);
                WalkEnabledElementsWithLimit(elementNode, newTvi, levelsToRecurse - 1);
                elementNode = walker.GetNextSibling(elementNode);
            }
        }
    }
}
