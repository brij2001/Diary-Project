using Diary.Views;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Fingerprint.Abstractions;

namespace Diary
{
    public partial class AppShell
    {
        public AppShell()
        {
            FingerPrint();

            //InitializeComponent();
            Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage));
            
        }
        public async void FingerPrint()
        {
            var avail = await CrossFingerprint.Current.IsAvailableAsync();
            if (!avail)
            {
                await App.Current.MainPage.DisplayAlert("Warning", "No Biometric available", "OK.");

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