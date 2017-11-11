using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPFCalendar.Model
{
    public class DayModel
    {
        private DateTime _date;
        private SolidColorBrush _dateColor;
        private ObservableCollection<EventModel> _eventsList = new ObservableCollection<EventModel>();

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        public SolidColorBrush DateColor
        {
            get
            {
                return _dateColor;
            }
            set
            {
                _dateColor = value;
            }
        }

        public ObservableCollection<EventModel> EventsList
        {
            get
            {
                return _eventsList;
            }
            set
            {
                _eventsList = value;
            }
        }

        public String DateText
        {
            get
            {
                return _date.ToString("MMMM dd", CultureInfo.CreateSpecificCulture("en-GB")); 
            }
        }

        public DayModel(DateTime date, SolidColorBrush color, List<EventModel> allEventsList)
        {
            Date = date;
            DateColor = color;
            EventsList = GetDayEvents(allEventsList);
        }

        private ObservableCollection<EventModel> GetDayEvents(List<EventModel> allEventsList)
        {
            ObservableCollection<EventModel> eventsList = new ObservableCollection<EventModel>();
            if (allEventsList != null)
            {
                foreach (EventModel e in allEventsList)
                {
                    // Date.Date - "pozbywamy się" godziny na wszelki wypadek
                    if (e.Date.Date.CompareTo(Date.Date) == 0)
                    {
                        eventsList.Add(e);
                    }
                }
            }
            return eventsList;
        }

        public void addEvent(EventModel e)
        {
            _eventsList.Add(e);
        }

        public void removeEvent(EventModel e)
        {
            _eventsList.Remove(e);
        }
    }
}
