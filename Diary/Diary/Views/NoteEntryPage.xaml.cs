using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diary.Models;

namespace Diary.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public NoteEntryPage()
        {
            InitializeComponent();

            BindingContext = new Note();
        }

        private async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                Note note = await App.Database.GetNoteAsync(id);
                BindingContext = note;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }


            

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                // Save the file.
                await App.Database.SaveNoteAsync(note);
            }
            await DisplayAlert("Success!", "Note save!!", "OK");
            await Shell.Current.GoToAsync(".."); // Navigate back
        }


        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool r = await DisplayAlert("Delete?", "Would you like to delete this note?", "Yes", "No");
            ///
            if (r == false)
                return;
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(note);

            await Shell.Current.GoToAsync("..");// Navigate back
        }
    }
}