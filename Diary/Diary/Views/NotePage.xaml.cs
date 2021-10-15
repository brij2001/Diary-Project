﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Diary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        public Command<Note> DeleteNoteSwipe { get; }


        public NotePage()
        {
            InitializeComponent();

            DeleteNoteSwipe = new Command<Note>(async x => await OnDeleteItem(x));
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
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID}");
            }
        }

         async void OnAddClicked(object sender,EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }


        private async Task OnDeleteItem(Note note)
        {
            await App.Database.DeleteNoteAsync(note);
        }
    }
}