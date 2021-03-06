﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuickHdr
{
    public class TaskbarIconDataContext
    {
        public ICommand MoreInfoCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = "https://13thsymphony.github.io/quickhdr/",
                            UseShellExecute = true
                        };

                        Process.Start(psi);
                    }
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

        public ICommand ToggleHdrCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => ((App)Application.Current).ToggleHdrAsync()
                };
            }
        }
    }
}
