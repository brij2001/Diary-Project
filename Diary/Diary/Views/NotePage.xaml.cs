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
        public Command<Note> DeleteNoteSwipe { get; }

        NotePageViewModel vm;
        public NotePage()
        {
            InitializeComponent();
            vm=new NotePageViewModel();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }
        
    }
}