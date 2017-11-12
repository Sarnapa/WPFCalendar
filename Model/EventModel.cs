using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar.Model
{
    [Serializable]
    public class EventModel: INotifyPropertyChanged
    {
        private DateTime _date;
        private DateTime _start;
        private DateTime _end;
        private String _title;

        public DateTime Date 
        { 
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public DateTime Start 
        { 
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                OnPropertyChanged("Start");
                OnPropertyChanged("EventText");
            }
        }
        public DateTime End 
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                OnPropertyChanged("End");
                OnPropertyChanged("EventText");
            }
        }
        public String Title 
        { 
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
                OnPropertyChanged("EventText");
            }
        }

        public String EventText
        {
            get
            {
                return _start.ToString("HH:mm") + "-" + _end.ToString("HH:mm") + " " + _title;
            }
        }

        [field:NonSerialized]public event PropertyChangedEventHandler PropertyChanged;

        public EventModel()
        { }
        
        public EventModel(DateTime date, DateTime start, DateTime end, String title)
        {
            Date = date;
            Start = start;
            End = end;
            Title = title;
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
