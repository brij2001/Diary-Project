using Diary.Views;
using Plugin.Fingerprint;
using Xamarin.Forms;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials;

namespace Diary
{
    public partial class AppShell 
    {
        public AppShell()
        {
            Routing.RegisterRoute("authmain", typeof(AuthMain));
            FingerPrint();

            //InitializeComponent();
            Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage));

        }

        public AboutPage AboutPage
        {
            get => default;
            set
            {
            }
        }

        public NotePage NotePage
        {
            get => default;
            set
            {
            }
        }

        public Calendar Calendar
        {
            get => default;
            set
            {
            }
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