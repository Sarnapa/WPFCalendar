using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFCalendar.Model;

namespace WPFCalendar.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private EventModel _event;
        private Boolean _isRemoveEvent;
        private String _eventStart;
        private String _eventEnd;
        private String _eventTitle;
        private ICommand _removeEventCommand;
        private ICommand _closeCommand;

        public String Error
        {
            get { return String.Empty; }
        }

        public String this[string fieldName]
        {
            get
            {
                String result = null;
                DateTime tempDate = new DateTime();
                String timeFormat = "HH:mm";
                switch (fieldName)
                {
                    case "EventTitle":
                        if (String.IsNullOrEmpty(EventTitle))
                            result = "Title field must not be empty!";
                        else if (EventTitle.Length > 30)
                            result = "Title must be at most 30 characters in length!";
                        break;
                    case "EventStart":
                        if (String.IsNullOrEmpty(EventStart))
                            result = "Start time field must not be empty!";
                        else if (!DateTime.TryParseExact(EventStart, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
                            result = "Required format: HH:mm";
                        break;
                    case "EventEnd":
                        if (String.IsNullOrEmpty(EventEnd))
                            result = "End time field must not be empty!";
                        else if (!DateTime.TryParseExact(EventEnd, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
                            result = "Required format: HH:mm";
                        break;
                }
                return result;
            }
        }

        public EventModel Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                EventTitle = _event.Title;
                EventStart = _event.Start.ToString("HH:mm");
                EventEnd = _event.End.ToString("HH:mm");
            }
        }

        public Boolean IsRemoveEvent
        {
            get
            {
                return _isRemoveEvent;
            }
            set
            {
                _isRemoveEvent = value;
            }
        }


        public String EventStart
        {
            get
            {
                return _eventStart;
            }
            set
            {
                _eventStart = value;
                OnPropertyChanged("EventStart");
            }
        }

        public String EventEnd
        {
            get
            {
                return _eventEnd;
            }
            set
            {
                _eventEnd = value;
                OnPropertyChanged("EventEnd");
            }
        }

        public String EventTitle
        {
            get
            {
                return _eventTitle;
            }
            set
            {
                _eventTitle = value;
                OnPropertyChanged("EventTitle");
            }
        }

        public ICommand RemoveEventCommand
        {
            get
            {
                return _removeEventCommand;
            }
            set
            {
                _removeEventCommand = value;
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand;
            }
            set
            {
                _closeCommand = value;
            }
        }

        public Action CloseAction { get; set; }
        public Action RemoveEventAction { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public EventViewModel()
        {
            CloseCommand = new RelayCommand(
                new Action<object>(
                    delegate(object obj)
                    {
                        CloseAction();
                    }
                    ), null);

            RemoveEventCommand = new RelayCommand(
                 new Action<object>(
                     delegate(object obj)
                     {
                         RemoveEventAction();
                     }
                     ), null);
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void AddNewEvent(DateTime date)
        {
            _event = new EventModel();
            _event.Date = date;
            DateTime startTime = GetTimeFromString(_eventStart);
            DateTime endTime = GetTimeFromString(_eventEnd);
            _event.Title = _eventTitle;
            _event.Start = startTime;
            _event.End = endTime;
        }

        public void ModifyEvent()
        {
            DateTime startTime = GetTimeFromString(_eventStart);
            DateTime endTime = GetTimeFromString(_eventEnd);
            _event.Title = _eventTitle;
            _event.Start = startTime;
            _event.End = endTime;
        }

        private DateTime GetTimeFromString(String timeString)
        {
            int hour = Int32.Parse(timeString.Substring(0, 2));
            int minutes = Int32.Parse(timeString.Substring(3, 2));
            return new DateTime(_event.Date.Year, _event.Date.Month, _event.Date.Day, hour, minutes, 0);
        }

    }
}
