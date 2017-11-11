using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WPFCalendar.ViewModel;

namespace WPFCalendar.View
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

        private void DoubleClickEventHandler(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount > 1)
            {
                MainViewModel mainVM = this.mainViewModel;
                EventWindow eventWindow = new EventWindow();
                EventViewModel eventVM = eventWindow.eventViewModel;
                Boolean? res = eventWindow.ShowDialog();
                if(res.HasValue && res.Value)
                {

                }
            }
        }
    }
}
