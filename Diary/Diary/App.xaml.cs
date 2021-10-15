using Diary.Data;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diary.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Diary
{
    public partial class App : Application
    {
        private static NoteDatabase database;
        public App()
        {
            
                InitializeComponent();
            //MainPage = new AppShell();
            if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
           {
                MainPage = new AppShell();
           }
            else
           {
               MainPage = new AuthMain();
           }

        }
        public static NoteDatabase Database
        {
            get
            {
                if (database == null)
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                return database;
            }
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            
        }
    }
}