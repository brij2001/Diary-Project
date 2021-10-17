using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Diary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diary.ViewModels;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        NotePageViewModel _vm;

        public NotePage()
        {
            InitializeComponent();
            BindingContext = _vm = new NotePageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.OnAppearing();
        }
    }
}