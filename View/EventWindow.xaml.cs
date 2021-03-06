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
using System.Windows.Shapes;

namespace WPFCalendar.View
{
    /// <summary>
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        public EventWindow()
        {
            InitializeComponent();

            this.eventViewModel.CloseAction = new Action(() =>
            {
                this.DialogResult = true;
                this.Close();
            });

            this.eventViewModel.RemoveEventAction = new Action(() =>
            {
                this.DialogResult = true;
                this.eventViewModel.IsRemoveEvent = true;
                this.Close();
            });
        }
    }
}
