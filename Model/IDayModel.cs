using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar.Model
{
    public class IDayModel
    {
        public void AddEvent(EventModel e);
        public void RemoveEvent(EventModel e);
        private ObservableCollection<EventModel> GetDayEvents(List<EventModel> allEventsList);
    }
}
