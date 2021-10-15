using Syncfusion.SfCalendar.XForms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentPage
    {
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();

        public Data.NoteDatabase NoteDatabase
        {
            get => default;
            set
            {
            }
        }

        public Calendar()
        {
            InitializeComponent();
            SfCalendar calendar = new SfCalendar();
            this.Content = calendar;
        }
    }
}