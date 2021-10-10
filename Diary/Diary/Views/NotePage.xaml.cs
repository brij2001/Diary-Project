using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Diary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        public Command<Note> OnSwipeDelete { get; }
        public NotePage()
        {
            InitializeComponent();
          
            //OnSwipeDelete = new Command<Note>(DeleteNote);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the filename as a query parameter.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID.ToString()}");
            }
        }

         async void OnAddClicked(object sender,EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }


       /* public async void DeleteNote(Note note)
        {
            bool r = await DisplayAlert("Delete?", "Would you like to delete this note?", "Yes", "No");

            if (r == false)
                return;
            var noteD = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(noteD);

            OnAppearing();
        }*/
    }
}