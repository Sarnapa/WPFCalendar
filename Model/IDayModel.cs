using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar.Model
{
    public interface IDayModel
    {
        void AddEvent(EventModel e);
        void RemoveEvent(EventModel e);
    }
}
