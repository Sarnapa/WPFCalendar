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
using WPFCalendar.Model;
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
                DayModel currentDay = (DayModel)((StackPanel)sender).DataContext;
                if(e.OriginalSource is StackPanel)
                {
                    eventWindow.removeEventButton.Visibility = System.Windows.Visibility.Hidden;
                    eventWindow.addModifyEventButton.Content = "Add event";
                    Boolean? res = eventWindow.ShowDialog();
                    if (res.HasValue && res.Value)
                    {
                        eventVM.AddNewEvent(currentDay.Date);
                        mainVM.AddEvent(currentDay, eventVM.Event);
                    }
                }
                else if(e.OriginalSource is TextBlock)
                {
                    var eventTextBox = ((TextBlock)e.OriginalSource);
                    if(eventTextBox.Name == "eventTextBlock")
                    {
                        eventWindow.removeEventButton.Visibility = System.Windows.Visibility.Visible;
                        eventWindow.addModifyEventButton.Content = "Modify event";
                        var currentEvent = (EventModel)eventTextBox.DataContext;
                        eventVM.Event = currentEvent;
                        Boolean? res = eventWindow.ShowDialog();
                        if (res.HasValue && res.Value)
                        {
                            if(eventVM.IsRemoveEvent)
                            {
                                mainVM.RemoveEvent(currentDay, eventVM.Event);
                            }
                            else
                            {
                                eventVM.ModifyEvent();
                                mainVM.ModifyEvent(currentDay);
                            }
                        }
                    }
                    else
                    {
                        eventWindow.removeEventButton.Visibility = System.Windows.Visibility.Hidden;
                        eventWindow.addModifyEventButton.Content = "Add event";
                        Boolean? res = eventWindow.ShowDialog();
                        if (res.HasValue && res.Value)
                        {
                            eventVM.AddNewEvent(currentDay.Date);
                            mainVM.AddEvent(currentDay, eventVM.Event);
                        }
                    }
                }
                
            }
        }
    }
}
