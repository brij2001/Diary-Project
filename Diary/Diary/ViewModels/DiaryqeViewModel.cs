using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class DiaryqeViewModel : BindableObject
    {
        public DiaryqeViewModel()
        {
            IncreaseCount = new Command(OnIncrease);
        }

        public System.Windows.Input.ICommand IncreaseCount { get; }
        private int count = 0;
        private string countstring = "Click Me";

        private void OnIncrease()
        {
            count += 1;
            CountString = $"cliked {count} times.";
        }

        public string CountString
        {
            get => countstring;
            set
            {
                if (value == countstring)
                    return;
                countstring = value;
                OnPropertyChanged();
            }
        }
    }
}