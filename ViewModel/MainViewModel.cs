using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFCalendar.Model;

namespace WPFCalendar.ViewModel
{
    public class MainViewModel
    {
        private Calendar _calendar;
        private ObservableCollection<Event> _eventsList;
        private SerializationService _serializationService;

        public Calendar Calendar
        {
            get
            {
                return _calendar;
            }
            set
            {
                _calendar = value;
            }
        }

        public ObservableCollection<Event> EventsList
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

        public ICommand GetDate { get; private set;}

        public MainViewModel()
        {
            Calendar = new Calendar();
            EventsList = new ObservableCollection<Event>();
            _serializationService = new SerializationService();
            EventsList = (ObservableCollection<Event>)_serializationService.ReadSource();
            
            GetDate = new RelayCommand(GetDateAction, null);
        }

        private void GetDateAction(object obj)
        {
            Debug.WriteLine("Dupka");
        }


    }
}
