using InitialProject.Model;
using InitialProject.View.GuestFolder;
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
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuestView.xaml
    /// </summary>
    public partial class GuestView : Window
    {
        public User User { get; set; }

        public GuestView(User guest)
        {
            InitializeComponent();
            this.User = guest;

            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/home.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerAndAccommodationRatingView rating = new OwnerAndAccommodationRatingView(User);
            rating.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            rating.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AccommodationView accView = new AccommodationView(User);
            accView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            accView.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ProfileView profile = new ProfileView(User);
            profile.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            profile.Show();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have 2 new messages: \n --The owner updated your request. You can check it on your profile. \n --Something something");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            LocationListView profile = new LocationListView(User);
            profile.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            profile.Show();
        }
    }
}
