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
    public class DayModel: INotifyPropertyChanged
    {
        private DateTime _date;
        private String _dateColor;
        private String _eventColor;
        private String _mainFontColor;
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

        public String DateColor
        {
            get
            {
                return _dateColor;
            }
            set
            {
                _dateColor = value;
                OnPropertyChanged("DateColor");
            }
        }

        public String MainFontColor
        {
            get
            {
                return _mainFontColor;
            }
            set
            {
                _mainFontColor = value;
                OnPropertyChanged("MainFontColor");
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
                OnPropertyChanged("EventsList");
            }
        }

        public String DateText
        {
            get
            {
                return _date.ToString("MMMM dd", CultureInfo.CreateSpecificCulture("en-GB")); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DayModel(DateTime date, String dateColor, String eventColor, String mainFontColor, List<EventModel> allEventsList)
        {
            Date = date;
            DateColor = dateColor;
            _eventColor = eventColor;
            MainFontColor = mainFontColor;
            EventsList = GetDayEvents(allEventsList);
        }

        public void AddEvent(EventModel e)
        {
            e.EventColor = _eventColor;
            _eventsList.Add(e);
            EventsList = new ObservableCollection<EventModel>(_eventsList.OrderBy(o => o.Start));
        }

        public void RemoveEvent(EventModel e)
        {
            _eventsList.Remove(e);
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
                        e.EventColor = _eventColor;
                        eventsList.Add(e);
                    }
                }
            }
            eventsList = new ObservableCollection<EventModel>(eventsList.OrderBy(o => o.Start));
            return eventsList;
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
