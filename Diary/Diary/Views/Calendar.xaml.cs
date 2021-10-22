using Diary.ViewModels;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diary.Models;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentPage
    {

        public Calendar()
        {
            InitializeComponent();
        }

        private void calendar_OnInlineLoaded(object sender, InlineEventArgs args)
        {
            ListView listView = new ListView();
            listView.BackgroundColor = Color.White;

            listView.BindingContext = new ViewModel();

            ObservableCollection<ViewModel> ListViewCollection = new ObservableCollection<ViewModel>();

            for (int i = 0; i < args.Appointments.Count; i++)
                ListViewCollection.Add(new ViewModel() { Subject = ((args.Appointments as CalendarEventCollection)[i] as CalendarInlineEvent).Subject, ColorInline = ((args.Appointments as CalendarEventCollection)[i] as CalendarInlineEvent).Color });

            listView.ItemsSource = ListViewCollection;
            listView.ItemTemplate = new DataTemplate(() =>
            {

                ViewCell viewCell = new ViewCell();



                Label label = new Label();
                label.FontSize = 13;
                //label.GestureRecognizers.Add(new TapGestureRecognizer(HandleAction));
                label.TextColor = Color.Black;
                label.Margin = new Thickness(20, 0, 20, 0);
                label.VerticalTextAlignment = TextAlignment.Center;
                label.SetBinding(Label.TextProperty, "Subject");
                
                viewCell.View = label;
                return viewCell;
            });

            args.View = listView;
        }

        //private async void HandleAction(EventArgs arg1, object arg2)
        //{
        //    var a = arg1.Text;
        //  // string title= arg1.Text;
        //   // var note1 = await App.Database.GetNoteAsync(id);
        //    //await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPageViewModel.ItemId)}={note.ID}");
        //}
    }
    public class ViewModel
    {
        private string Sub;
        public string Subject
        {
            set { Sub = value; }
            get { return Sub; }
        }
        private Color SubColorInline;
        public Color ColorInline
        {
            set { SubColorInline = value; }
            get { return SubColorInline; }
        }
        public ViewModel(string a, Color b)
        {
            Subject = a;
            ColorInline = b;
        }
        public ViewModel()
        {

        }
    }
}
