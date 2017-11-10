using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar.Model
{
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
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
