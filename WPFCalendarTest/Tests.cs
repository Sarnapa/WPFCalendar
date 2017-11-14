using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using NUnit.Framework;
using WPFCalendar.Model;
using WPFCalendar.ViewModel;


namespace WPFCalendarTest
{
    // Install-Package NUnit -Version 2.6.4 -ProjectName
    // Install-Package RhinoMocks -Version 3.6.1
    // Install-Package NUnitTestAdapter -Version 2.1.1
    [TestFixture]
    public class Tests
    {
        /*[Test]
        public void AddingEventToDayModelTest()
        {
            var dayModelMock = MockRepository.GenerateMock<IDayModel>();
            var eventVM = new EventViewModel();
            eventVM.EventTitle = "event";
            eventVM.EventStart = "11:00";
            eventVM.EventEnd = "12:00";
            eventVM.AddNewEvent()
            dayModelMock.AssertWasCalled(e => e.AddEvent(eventVM.Event));
        }*/

        [Test]
        public void GoingNextWeekTest()
        {
            var mainVm = new MainViewModel();
            var firstDay = mainVm.DaysList[0].Date;
            mainVm.GetNextWeek.Execute(null);
            Assert.AreEqual(firstDay.AddDays(7), mainVm.DaysList[0].Date);
        }

        [Test]
        public void GoingPrevWeekTest()
        {
            var mainVm = new MainViewModel();
            var firstDay = mainVm.DaysList[0].Date;
            mainVm.GetPrevWeek.Execute(null);
            Assert.AreEqual(firstDay.AddDays(-7), mainVm.DaysList[0].Date);
        }

        [Test]
        public void PopupMenuTest()
        {
            var mainVm = new MainViewModel();
            Assert.AreEqual(false, mainVm.IsPopup);
            mainVm.PopupCommand.Execute(null);
            Assert.AreEqual(true, mainVm.IsPopup);
            mainVm.PopupCommand.Execute(null);
            Assert.AreEqual(false, mainVm.IsPopup);
        }

        /*[Test]
        public void NewEventWindowTest()
        {
            var mainVm = new MainViewModel();
            mainVm.
        }*/
    }
}
