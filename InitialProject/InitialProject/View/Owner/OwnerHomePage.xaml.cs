using InitialProject.Model;
using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerHomePage.xaml
    /// </summary>
    public partial class OwnerHomePage : Window
    {
        public User User { get; set; }

        public OwnerHomePage(User owner)
        {
            InitializeComponent();
            this.User = owner;
        }

        private void HomePage_OnClick(object sender, RoutedEventArgs e)
        {
            OwnerHomePage ownerHomePage= new OwnerHomePage(User);
            ownerHomePage.Show();
            Close();

        }

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void Reviews_OnClick(object sender, RoutedEventArgs e)
        {
            ReviewsAndForums reviewsAndForums = new ReviewsAndForums(User);
            reviewsAndForums.Show();
            Close();
        }

        private void BookingRequests_OnClick(object sender, RoutedEventArgs e)
        {
            BookingRequests bookingRequests = new BookingRequests(User);
            bookingRequests.Show();
            Close();
        }

        private void GuestRatings_OnClick(object sender, RoutedEventArgs e)
        {
            GuestRatings guestRatings = new GuestRatings(User);
            guestRatings.Show();
            Close();
        }

        private void AccommodationRegistration_OnClick(object sender, RoutedEventArgs e)
        {
            AccommodationRegistration accommodationRegistration = new AccommodationRegistration(User);
            accommodationRegistration.Show();
            Close();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Renovations_OnClick(object sender, RoutedEventArgs e)
        {
            RenovationList renovationList = new RenovationList(User);
            renovationList.Show();
            Close();
        }
        private void AccommodationStatistics_OnClick(object sender, RoutedEventArgs e)
        {
            AccommodationStatistics accommodationStatistics = new AccommodationStatistics(User);
            accommodationStatistics.Show();
            Close();
        }
    }
}