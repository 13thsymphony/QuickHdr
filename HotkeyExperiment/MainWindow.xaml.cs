﻿using System;
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

using NHotkey;
using NHotkey.Wpf;

namespace HotkeyExperiment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HotkeyManager.Current.AddOrReplace("Toggle HDR", Key.H, ModifierKeys.Control | ModifierKeys.Windows, OnHotkey);
        }

        private void OnHotkey(object sender, HotkeyEventArgs e)
        {
            Output.Text += e.Name + "\n";
            e.Handled = true;
        }
    }
}
