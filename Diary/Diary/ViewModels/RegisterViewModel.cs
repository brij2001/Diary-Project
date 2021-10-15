using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Diary.ViewModels
{
    internal class RegisterViewModel : BaseViewModel
    {
        public string WebAPIkey = "AIzaSyDDUupHlpP_0MmTg58iJQXTUsdQRcfZp1g";
        public string NewEmail{ get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public Command RegisterCommand { get; }
        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterButton);
        }

        private async void OnRegisterButton(object obj)
        {
            if (NewPassword != ConfirmPassword)
            {
                await App.Current.MainPage.DisplayAlert("Passwords Don't Match","Please Check the entered Passwords","OK");
                return;
            }
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(NewEmail,NewPassword);
                string gettoken = auth.FirebaseToken;
                await App.Current.MainPage.DisplayAlert("Alert", "Registered Successfully", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}