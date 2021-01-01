using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotkeyExperiment
{
    class DummyDataContext
    {
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
    }
}
