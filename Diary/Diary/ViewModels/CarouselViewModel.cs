using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.ViewModels
{
    public class CarouselViewModel:BaseViewModel
    {
        private List<CarouselModel> imageCollection = new List<CarouselModel>();
        public CarouselViewModel()
        {
            ImageCollection.Add(new CarouselModel("person1.jpg", "Title1","1"));
            ImageCollection.Add(new CarouselModel("person2.jpg", "Title2", "2"));
            ImageCollection.Add(new CarouselModel("person3.jpg", "Title3", "3"));
            ImageCollection.Add(new CarouselModel("person4.jpg", "Title4", "4"));
            ImageCollection.Add(new CarouselModel("person5.jpg", "Title5", "5"));
            for (int i = 1; i < 7; i++)
                ImageCollection.Add(new CarouselModel("image" + i + "_min.jpg", "Title","5"));
        }
        public List<CarouselModel> ImageCollection
        {
            get { return imageCollection; }
            set { imageCollection = value; }
        }
    }

    public class CarouselModel
    {
        public CarouselModel(string imagestr,string Titlestr, string idstr)
        {
            Image = imagestr;
            Title = Titlestr;
            Id = idstr;
        }
        private string _title;
        private string _id;
        private string _image;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }
    }


}
