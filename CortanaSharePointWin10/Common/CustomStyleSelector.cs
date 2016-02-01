using CortanaSharePointWin10.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.UI.Xaml.Controls.Input.Calendar;
using Windows.UI.Xaml;

namespace CortanaSharePointWin10.Common
{
    public class CustomStyleSelector : CalendarCellStyleSelector
    {
        public DataTemplate EventTemplate { get; set; }

        protected override void SelectStyleCore(CalendarCellStyleContext context, Telerik.UI.Xaml.Controls.Input.RadCalendar container)
        {
            var events = (container.DataContext as CortanaCalendarViewModel).CortanaAppointments;

            if (events.Where(e => e.StartDate.Date == context.Date.Date).Count() > 0)
            {
                context.CellTemplate = this.EventTemplate;
            }
        }
    }
}
