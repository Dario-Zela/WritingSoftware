using System;
using Blaze.Core;

namespace Blaze.MVVM.ViewModel
{
    public class Detail : Observable
    {
        private string _title = "Title";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
                Console.WriteLine("Test");
            }
        }

        private string _subtitle = "SubTitle";
        public string SubTitle
        {
            get => _subtitle;
            set
            {
                _subtitle = value;
                OnPropertyChanged();
            }
        }

        private string _image = "Image";
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        private string _description = "Description";
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    public class StoryElementViewModel : Observable
    {
        public Detail _details = new Detail();


        public StoryElementViewModel()
        {

        }
    }
}
