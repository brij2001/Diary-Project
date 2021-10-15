using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    
    class AuthMainViewModel
    {
        public string WebAPIkey = "AIzaSyDDUupHlpP_0MmTg58iJQXTUsdQRcfZp1g";
        public string Email { get; set; }
        public string Password { get; set; }

        public Command LoginCommand { get; }
        public AuthMainViewModel()
        {
            LoginCommand = new Command(Loginbutton_Clicked);
        }
        async void Loginbutton_Clicked(Object sender)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                Application.Current.MainPage = new AppShell();
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }
        }
    }
}
