﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.Windows.Shapes;
using Teflon.Modules;

namespace UITest
{
    /// <summary>
    /// Shell.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(Shell))]
    public partial class Shell : Window
    {
        [ImportingConstructor]
        public Shell()
        {
            InitializeComponent();
        }
    }
}
