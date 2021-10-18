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
using System.IO;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using NativeMedia;

namespace Diary.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class NoteEntryPageViewModel : BaseViewModel
    {
        public MvvmHelpers.Commands.Command OnSaveCommand { get; }
        public MvvmHelpers.Commands.Command OnDeleteCommand { get; }
        public MvvmHelpers.Commands.Command PickImageCommand { get; }
        public MvvmHelpers.Commands.Command DeleteImageCommand { get; }

        public NoteEntryPageViewModel()
        {
            OnSaveCommand = new MvvmHelpers.Commands.Command(OnSaveButtonClicked);
            OnDeleteCommand = new MvvmHelpers.Commands.Command(OnDeleteButtonClicked);
            PickImageCommand = new MvvmHelpers.Commands.Command(OnPickImageButtonClicked);
            DeleteImageCommand = new MvvmHelpers.Commands.Command(OnDeleteImageButtonCliked);
        }

        private string _ITEMID;
        private string _TITLE;
        private string _TEXT;
        private DateTime _DATETIME;
        private string _IMAGE;
        public Note note = new Note();
        public DateTime datetime { get => _DATETIME; set => SetProperty(ref _DATETIME, value); }
        public string title { get => _TITLE; set => SetProperty(ref _TITLE, value); }
        public string text { get => _TEXT; set => SetProperty(ref _TEXT, value); }
        public string image { get => _IMAGE; set => SetProperty(ref _IMAGE, value); }

        public string ItemId
        {
            get { return _ITEMID; }
            set
            {
                _ITEMID = value;
                LoadNote(value);
            }
        }

        private async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                var note1 = await App.Database.GetNoteAsync(id);
                title = note1.Title;
                text = note1.Text;
                datetime = note1.Date;
                image = note1.image;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        public string photoPathDB;

        private async void OnSaveButtonClicked()
        {
            note.ID = Convert.ToInt32(ItemId);
            note.Date = datetime;
            if (ItemId != null && (await Application.Current.MainPage.DisplayAlert("Update Date?", "Do you want to update the date and time of the entry?", "Yes", "No")) == true)
                note.Date = DateTime.Now;
            note.Text = text;
            note.Title = title;
            note.image = image;

            if (note.image == null && photoPathDB != null)
                note.image = photoPathDB;
            else if (note.image != null && photoPathDB != null)
                note.image = photoPathDB;

            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                // Save the file.
                await App.Database.SaveNoteAsync(note);
            }
            photoPathDB = null;
            _ = Application.Current.MainPage.DisplayToastAsync("Note Saved.", 800);
            await Shell.Current.GoToAsync(".."); // Navigate back
        }

        private async void OnDeleteButtonClicked()
        {
            bool r = await Application.Current.MainPage.DisplayAlert("Delete?", "Would you like to delete this note?", "Yes", "No");

            if (r == false)
                return;

            note.ID = Convert.ToInt32(ItemId);
            await App.Database.DeleteNoteAsync(note);

            await Shell.Current.GoToAsync("..");// Navigate back
        }

        public void SavePicture(string photoName, string documentsPath, Stream data)
        {
            documentsPath = Path.Combine(documentsPath, "imagesFolder");
            Directory.CreateDirectory(documentsPath);

            string filePath = Path.Combine(documentsPath, photoName);

            byte[] bArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
        }

        private async void OnPickImageButtonClicked()
        {
            var st = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (st != PermissionStatus.Granted)
            {
                await Application.Current.MainPage.DisplayAlert("Permission Needed", "To upload an image storage access is required.", "OK");
                await Permissions.RequestAsync<Permissions.StorageRead>();
                await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            st = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (st != PermissionStatus.Granted)
            {
                await Application.Current.MainPage.DisplayAlert("Permission Not Granted", "Permission wasn't granred last time." +
                     "Go to System Settings to allow access.", "OK");
                return;
            }

            var results = await MediaGallery.PickAsync(1, MediaFileType.Image);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (results.Files == null)
            {
                return;
            }
            foreach (var media in results.Files)
            {
                Random rnd = new Random();
                var fileName = media.NameWithoutExtension;
                var ext = media.Extension;
                string photoName = rnd.Next(50, 100).ToString();
                photoName += fileName.ToString() + "." + ext.ToString();
                Console.WriteLine(photoName);
                Console.WriteLine(path);
                photoPathDB = Path.Combine(path, "imagesFolder", photoName);
                await media.OpenReadAsync();
                SavePicture(photoName, path, await media.OpenReadAsync());
            }
        }

        private async void OnDeleteImageButtonCliked()
        {
            note.ID = Convert.ToInt32(ItemId);
            note.image = null;
            await App.Database.SaveNoteAsync(note);
            await Application.Current.MainPage.DisplayToastAsync("Image Removed", 800);
        }
    }
}