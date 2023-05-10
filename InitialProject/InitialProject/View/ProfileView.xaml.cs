using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        public User Guest { get; set; }
        public ObservableCollection<AccommodationReservation> FilteredAccommodations { get; set; }
        private readonly AccommodationReservationRepository _reservationRepository;
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        int brojac;

        public ProfileView(User user)
        {
            InitializeComponent();
            this.Guest = user;
            FilteredAccommodations = new ObservableCollection<AccommodationReservation>();
            FilteredAccommodations.Clear();
            _reservationRepository = new AccommodationReservationRepository();
            Reservations = new ObservableCollection<AccommodationReservation>(_reservationRepository.FindAll());
            NameLabel.Content = user.Username;
            Image.Visibility = Visibility.Collapsed;
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);
            foreach (AccommodationReservation reservation in Reservations)
            {
                if (reservation.GuestId == user.Id && reservation.EndDate >= oneYearAgo)
                    brojac++;
            }
            if (brojac >= 10)
            {
                Image.Visibility = Visibility.Visible;
                SuperGuestLabel.Content = "SuperGuest";
                SuperGuestLabel.Foreground = Brushes.Gold;
            }
            else
            {
                SuperGuestLabel.Content = "Guest";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The number is: {brojac}");
        }
    }
}
