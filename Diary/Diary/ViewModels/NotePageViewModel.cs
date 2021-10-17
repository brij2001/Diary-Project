using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Models;
using Diary.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;

namespace Diary.ViewModels
{
    public class NotePageViewModel : BaseViewModel
    {
        public MvvmHelpers.Commands.Command OnAddCommand { get; }
        public MvvmHelpers.Commands.Command<Note> OnSelect { get; }
        public MvvmHelpers.Commands.Command<Note> OnSwipeDelete { get; }
        public MvvmHelpers.Commands.Command OnLoad { get; }
        public ObservableCollection<Note> Notes { get; }

        public NotePageViewModel()
        {
            OnAddCommand = new MvvmHelpers.Commands.Command(OnAddClicked);
            OnSelect = new MvvmHelpers.Commands.Command<Note>(OnSelectionChanged);
            OnSwipeDelete = new MvvmHelpers.Commands.Command<Note>(OnDeleteItem);
            Notes = new ObservableCollection<Note>();
            OnLoad = new MvvmHelpers.Commands.Command(async () => await OnLoadPage());
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
       public async Task OnLoadPage()
        {
            IsBusy = true;
            try
            {
                Notes.Clear();
                var items = await App.Database.GetNotesAsync();
                foreach (var t in items)
                {
                    Notes.Add(t);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnSelectionChanged(Note note)
        {
            await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID}");
        }

        private async void OnAddClicked()
        {
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }

        private async void OnDeleteItem(Note note)
        {
            await App.Database.DeleteNoteAsync(note);
            await OnLoadPage();
        }
    }
}