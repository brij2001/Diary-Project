using System;
using System.Collections.Generic;
using System.Text;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Diary.Data;
using System.Collections.ObjectModel;

namespace Diary.ViewModels
{
    class CalendarViewModel:BaseViewModel
    {
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();

        public CalendarViewModel()
        {
            CalendarInlineEvent event1 = new CalendarInlineEvent();
            event1.StartTime = new DateTime(2021, 9, 4, 5, 0, 0);
            event1.EndTime = new DateTime(2021, 9, 4, 5, 0, 0);
            event1.Subject = "Go to Meeting";
            event1.Color = Color.Fuchsia;

            CalendarInlineEvent event2 = new CalendarInlineEvent();
            event2.StartTime = new DateTime(2021, 9, 4, 10, 0, 0);
            event2.EndTime = new DateTime(2021, 9, 4, 10, 0, 0);
            event2.Subject = "Planning";
            event2.Color = Color.Green;
            
            CalendarInlineEvents.Add(event1);
            CalendarInlineEvents.Add(event2);
            //event2 = null;
            //event2.StartTime = new DateTime(2021, 9, 11, 10, 0, 0);
            //event2.EndTime = new DateTime(2021, 9,11, 12, 0, 0);
            //event2.Subject = "Planning2";
            //event2.Color = Color.Green;
            //CalendarInlineEvents.Add(event2);
        }
        
    }
    
}
