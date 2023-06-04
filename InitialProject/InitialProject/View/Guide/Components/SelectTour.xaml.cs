using InitialProject.Model;
using InitialProject.View.Tourist;
using InitialProject.View.Tourist.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Guide.Components
{
    /// <summary>
    /// Interaction logic for SelectTour.xaml
    /// </summary>
    public partial class SelectTour : UserControl
    {
        public User guide
        {
            get { return (User)GetValue(GuideProperty); }
            set { SetValue(GuideProperty, value); }
        }
        public static readonly DependencyProperty GuideProperty =
            DependencyProperty.Register("Guide", typeof(User), typeof(SelectTour), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SelectTour), new PropertyMetadata(string.Empty));

        public string ImagePath
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(SelectTour), new PropertyMetadata("../../../Resources/Images/tour_picture.jpg"));


        public SelectTour()
        {
            InitializeComponent();
        }

        private void TrackTourLive_OnClick(object sender, RoutedEventArgs e)
        {
            TrackTourLive trackTourLive = new TrackTourLive(guide);
            trackTourLive.Show();
        }
    }
}
