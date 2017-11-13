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
        private List<EventModel> _allEventsList = new List<EventModel>();
        private Boolean _isPopup;
        private List<ColorScheme> _colorSchemesList;
        private ColorScheme _currentColorScheme;
        private List<String> _fontsList;
        private String _currentFont;
        private ICommand _getPrevWeek;
        private ICommand _getNextWeek;
        private ICommand _popupCommand;

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

        public Boolean IsPopup
        {
            get
            {
                return _isPopup;
            }
            private set
            {
                _isPopup = value;
                OnPropertyChanged("IsPopup");
            }
        }

        public List<ColorScheme> ColorSchemesList
        {
            get
            {
                return _colorSchemesList;
            }
        }

        public ColorScheme CurrentColorScheme
        {
            get
            {
                return _currentColorScheme;
            }
            set
            {
                _currentColorScheme = value;
                if (DaysList.Count > 0)
                {
                    DateTime firstDateOfWeek = CalendarService.GetFirstDateOfWeek(DaysList[0].Date.Date);
                    DaysList = GetDaysList(firstDateOfWeek);
                }
                OnPropertyChanged("CurrentColorScheme");
            }
        }

        public List<String> FontsList
        {
            get
            {
                return _fontsList;
            }
        }

        public String CurrentFont
        {
            get
            {
                return _currentFont;
            }
            set
            {
                _currentFont = value;
                OnPropertyChanged("CurrentFont");
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

        public ICommand PopupCommand
        {
            get
            {
                return _popupCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public MainViewModel()
        {
            _colorSchemesList = new List<ColorScheme>
            {
                new ColorScheme("Basic Scheme", "#CCFFFF", "#009999", "#006666", "#AAAAFF", "#AAAAFF", "#FFFFFF", "#FFFF42", "#000066", "#007777"),
                new ColorScheme("Dark Scheme", "#F9A825", "#FF8F00", "#EF6C00", "#424242", "#424242", "#999999", "#8BC34A", "#FAFAFA", "#FFEA00"),
                new ColorScheme("Green Scheme", "#B2FF59", "#76FF03", "#64DD17", "#4CAF50", "#4CAF50", "#C5E1A5", "#F9A825", "#212121", "#37474F")
            };
            CurrentColorScheme = _colorSchemesList[0];

            _fontsList = new List<String>
            {
                "Arial",
                "Courier New",
                "Times New Roman"
            };
            CurrentFont = _fontsList[0];

            _allEventsList = (List<EventModel>)SerializationService.ReadSource();
            DateTime firstDateOfWeek = CalendarService.GetFirstDateOfWeek(DateTime.Now.Date);
            DaysList = GetDaysList(firstDateOfWeek);
            WeeksList = GetWeeksList(firstDateOfWeek);

            _getPrevWeek = new RelayCommand(GetPrevWeekAction, null);
            _getNextWeek = new RelayCommand(GetNextWeekAction, null);
            _popupCommand = new RelayCommand(e => IsPopup = !IsPopup, null);
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
            int calendarCellsCount = _weekDaysCount * _weeksCount;
            for (int i = 0; i < calendarCellsCount; ++i)
            {
                if (date.CompareTo(DateTime.Now.Date) == 0)
                    daysList.Add(new DayModel(date, CurrentColorScheme.TodayCellBackgroundColor, CurrentColorScheme.EventColor,
                        CurrentColorScheme.MainFontColor, _allEventsList));
                else
                    daysList.Add(new DayModel(date, CurrentColorScheme.DayCellBackgroundColor, CurrentColorScheme.EventColor,
                        CurrentColorScheme.MainFontColor, _allEventsList));
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

        public void AddEvent(DayModel day, EventModel e)
        {
            day.addEvents(e);
            _allEventsList.Add(e);
            WriteToSource();
        }

        public void ModifyEvent(DayModel day)
        {
            day.EventsList = new ObservableCollection<EventModel>(day.EventsList.OrderBy(o => o.Start));
            WriteToSource();
        }

        public void RemoveEvent(DayModel day, EventModel e)
        {
            day.removeEvents(e);
            _allEventsList.Remove(e);
            WriteToSource();
        }

        private void WriteToSource()
        {
            Debug.WriteLine(_allEventsList.Count);
            SerializationService.WriteToSource(_allEventsList);
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
