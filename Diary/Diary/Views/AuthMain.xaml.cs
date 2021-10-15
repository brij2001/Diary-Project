using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Diary.ViewModels;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthMain : ContentPage
    {
        public string WebAPIkey = "AIzaSyDDUupHlpP_0MmTg58iJQXTUsdQRcfZp1g";

        public AuthMain()

        {
            InitializeComponent();
            this.BindingContext = new AuthMainViewModel();
        }



        private void RegisterTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }
    }
}