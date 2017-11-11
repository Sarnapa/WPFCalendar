using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WPFCalendar.Model;

namespace WPFCalendar.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static readonly int _weekDaysCount = 7;
        private static readonly int _weeksCount = 4;
        private List<DayModel> _daysList = new List<DayModel>();
        private List<String> _weeksList = new List<string>();
        private ICommand _getPrevWeek;
        private ICommand _getNextWeek;

        public int WeekDaysCount 
        {
            get
            {
                return _weekDaysCount;
            }
        }

        public int WeeksCount 
        {
            get
            {
                return _weeksCount;
            }
        }
        
        public List<DayModel> DaysList 
        {
            get
            {
                return _daysList;
            }
            set
            {
                _daysList = value;
                OnPropertyChanged("DaysList");
            }
        }

        public List<String> WeeksList
        {
            get
            {
                return _weeksList;
            }
            set
            {
                _weeksList = value;
                OnPropertyChanged("WeeksList");
            }
        }

        public ICommand GetPrevWeek
        {
            get
            {
                return _getPrevWeek;
            }
        }

        public ICommand GetNextWeek
        {
            get
            {
                return _getNextWeek;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public MainViewModel()
        {
            DateTime firstDateOfWeek = CalendarService.GetFirstDateOfWeek(DateTime.Now.Date);
            DaysList = GetDaysList(firstDateOfWeek);
            WeeksList = GetWeeksList(firstDateOfWeek);
            _getPrevWeek = new RelayCommand(GetPrevWeekAction, null);
            _getNextWeek = new RelayCommand(GetNextWeekAction, null);
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private List<DayModel> GetDaysList(DateTime date)
        {
            List<DayModel> daysList = new List<DayModel>();
            List<EventModel> allEventsList = (List<EventModel>)SerializationService.ReadSource();

            int calendarCellsCount = _weekDaysCount * _weeksCount;
            for (int i = 0; i < calendarCellsCount; ++i)
            {
                if (date.CompareTo(DateTime.Now.Date) == 0)
                    daysList.Add(new DayModel(date, new SolidColorBrush(Color.FromRgb(255, 255, 66)), allEventsList));
                else
                    daysList.Add(new DayModel(date, new SolidColorBrush(Colors.White), allEventsList));
                date = date.AddDays(1);
            }
            return daysList;
        }

        private List<String> GetWeeksList(DateTime date)
        {
            List<String> weeksList = new List<String>();

            int weekNumber = CalendarService.GetWeekOfYear(date);
            for(int i = 0; i < _weeksCount; ++i)
            {
                weeksList.Add(String.Format("W{0}\n{1}", weekNumber, date.Year));
                date = date.AddDays(7);
                weekNumber = CalendarService.GetWeekOfYear(date);
            }
            return weeksList;
        }

        // Commands Actions
        private void GetPrevWeekAction(object obj)
        {
            DateTime firstDate = DaysList[0].Date.AddDays(-7);
            DaysList = GetDaysList(firstDate);
            WeeksList = GetWeeksList(firstDate);
        }

        private void GetNextWeekAction(object obj)
        {
            DateTime firstDate = DaysList[0].Date.AddDays(7);
            DaysList = GetDaysList(firstDate);
            WeeksList = GetWeeksList(firstDate);
        }

    }
}
