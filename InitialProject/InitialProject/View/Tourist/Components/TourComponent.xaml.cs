using InitialProject.Repository;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using InitialProject.Model;

namespace InitialProject.View.Tourist.Components
{
    /// <summary>
    /// Interaction logic for TourComponent.xaml
    /// </summary>
    public partial class TourComponent : UserControl
    {
        public User Tourist
        {
            get { return (User)GetValue(TouristProperty); }
            set { SetValue(TouristProperty, value); }
        }
        public static readonly DependencyProperty TouristProperty =
            DependencyProperty.Register("Tourist", typeof(User), typeof(TourComponent), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public string ImagePath
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public int TourId
        {
            get { return (int)GetValue(TourIdProperty); }
            set { SetValue(TourIdProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(TourComponent), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(TourComponent), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(TourComponent), new PropertyMetadata("../../../Resources/Images/tour_picture.jpg"));

        public static readonly DependencyProperty TourIdProperty =
            DependencyProperty.Register("TourId", typeof(int), typeof(TourComponent), new PropertyMetadata(1));

        public TourComponent()
        {
            InitializeComponent();
        }

        private void BookButton_OnClick(object sender, RoutedEventArgs e)
        {

            BookWindow bookWindow = new BookWindow(Tourist, TourId);
            bookWindow.Show();
        }
    }
    
}
