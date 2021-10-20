using Diary.Views;
using Plugin.Fingerprint;
using Xamarin.Forms;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials;
using System;

namespace Diary
{
    public partial class AppShell 
    {
        public AppShell()
        {
            
            FingerPrint();

            //InitializeComponent();
            Routing.RegisterRoute("AuthMain", typeof(AuthMain));
            Routing.RegisterRoute("NotePage", typeof(NotePage));
            Routing.RegisterRoute("NoteEntryPage", typeof(NoteEntryPage));
            Routing.RegisterRoute("Register", typeof(Register));
            Routing.RegisterRoute("Settings", typeof(Settings));
            Routing.RegisterRoute("Corousel", typeof(Carousel));
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Preferences.Set("MyFirebaseRefreshToken", "");
            await Shell.Current.GoToAsync("AuthMain");
        }

        public async void FingerPrint()
        {
            var avail = await CrossFingerprint.Current.GetAvailabilityAsync();
            if (avail != FingerprintAvailability.Available)
            {
                await this.DisplayAlert("Warning. No Biometric available!", "Your will be using this app without Authentication", "OK.");
                InitializeComponent();
                return;
            }

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration
                ("Auth", "Need permission to use authentication."));

            if (authResult.Authenticated)
            {
                InitializeComponent();
            }
            else
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            }


        }
    }
}