using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class DiaryqeViewModel : ViewModelBase
    {
        public DiaryqeViewModel()
        {
            IncreaseCount = new Command(OnIncrease);
            Title = "Diary Eq";
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
            set => SetProperty(ref countstring, value);
        }
    }
}