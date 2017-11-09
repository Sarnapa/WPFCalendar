using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar.Model
{
    public class Calendar: INotifyPropertyChanged
    {
        private DateTime _firstDate = DateTime.Now.AddDays(-((double)(DateTime.Now.DayOfWeek) - 1));
        private DateTime _date;
        private DateTime _today = DateTime.Now;

        public DateTime FirstDate
        {
            get
            {
                return _firstDate;
            }
            set
            {
                _firstDate = value;
                OnPropertyChanged("FirstDate");
            }
        }

        public DateTime Date
        {
            get
            {
                return Date;
            }
            set
            {
                Date = value;
                OnPropertyChanged("FirstDate");
            }
        }

        public DateTime Today
        {
            get
            {
                return _today;
            }
            set
            {
                _today = value;
                OnPropertyChanged("Today");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Calendar()
        {}

        public void GetDate(int calendarCellIdx)
        {
            _date = _date.AddDays((double)calendarCellIdx);
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
