using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFCalendar.Model;

namespace WPFCalendar.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        private EventModel _event;
        private Boolean _isRemoveEvent;
        private ICommand _removeEventCommand;
        private ICommand _closeCommand;

        public EventModel Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                OnPropertyChanged("EventStart");
                OnPropertyChanged("EventEnd");
                OnPropertyChanged("EventTitle");
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
                if(_event != null)
                    return _event.Start.ToString("HH:mm");
                return "";
            }
        }

        public String EventEnd
        {
            get
            {
                if(_event != null)
                    return _event.End.ToString("HH:mm");
                return "";
            }
        }

        public String EventTitle
        {
            get
            {
                if(_event != null)
                    return _event.Title;
                return "";
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

    }
}
