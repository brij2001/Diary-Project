using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;


namespace Diary.Models
{
    public class Note: INotifyPropertyChanged
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string image { get; set; }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
