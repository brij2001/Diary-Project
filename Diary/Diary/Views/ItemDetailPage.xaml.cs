using Diary.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Diary.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}