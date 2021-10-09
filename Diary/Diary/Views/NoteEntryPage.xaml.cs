using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diary.Models;
using Xamarin.CommunityToolkit.Extensions;
using NativeMedia;
using Xamarin.Essentials;

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

        public string photoPathDB;

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.Now;
            note.image = photoPathDB;
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                // Save the file.
                await App.Database.SaveNoteAsync(note);
            }
            await this.DisplayToastAsync("Note Save.", 800);
            await Shell.Current.GoToAsync(".."); // Navigate back
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool r = await DisplayAlert("Delete?", "Would you like to delete this note?", "Yes", "No");

            if (r == false)
                return;
            var note = (Note)BindingContext;
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

        private async void OnPickImageButtonClicked(object sender, EventArgs e)
        {
            var st = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (st != PermissionStatus.Granted)
            {
                await DisplayAlert("Permission Needed", "To upload an image storage access is required.", "OK");
                var status = await Permissions.RequestAsync<Permissions.StorageRead>();
                await Permissions.RequestAsync<Permissions.StorageWrite>();
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
                photoName = photoName + (fileName.ToString() + "." + ext.ToString());
                Console.WriteLine(photoName);
                Console.WriteLine(path);
                photoPathDB = Path.Combine(path,"imagesFolder", photoName);
                var a = await media.OpenReadAsync();
                SavePicture(photoName, path, await media.OpenReadAsync());
            }
        }
    }
}