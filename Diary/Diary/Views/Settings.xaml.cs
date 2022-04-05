using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Diary.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {


        public Settings()
        {

            InitializeComponent();
            switch (SettingsHelper.Theme)
            {
                case 0:
                    RadioButtonSystem.IsChecked = true;
                    break;
                case 1:
                    RadioButtonLight.IsChecked = true;
                    break;
                case 2:
                    RadioButtonDark.IsChecked = true;
                    break;
            }
        }

        private void OnRestoreButton(object sender, EventArgs args)
        {
            Console.WriteLine("restore pressed");
            //throw new NotImplementedException();
        }

        private async void OnBackupButton(object sender, EventArgs args)
        {

            var fileStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("/data/user/0/com.project.diary/files/.local/share/Notes.db3");
            Console.WriteLine("Backup pressed");
            //Console.WriteLine(db);
            var cacheFile = Path.Combine(FileSystem.CacheDirectory, "SampleDoc.pdf");

            await Share.RequestAsync(new ShareFileRequest {
                Title = "Backup",
                File = new ShareFile("/data/user/0/com.project.diary/files/.local/share/Notes.db3")
        });
        }

        bool loaded;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            loaded = true;
        }

        void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!loaded)
                return;

            if (!e.Value)
                return;

            var val = (sender as RadioButton)?.Value as string;
            if (string.IsNullOrWhiteSpace(val))
                return;

            switch (val)
            {
                case "System":
                    SettingsHelper.Theme = 0;
                    break;
                case "Light":
                    SettingsHelper.Theme = 1;
                    break;
                case "Dark":
                    SettingsHelper.Theme = 2;
                    break;
            }


          TheTheme.SetTheme();

        }

    }
}