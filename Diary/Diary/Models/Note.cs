using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Models
{
    public class Note
    {
        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
