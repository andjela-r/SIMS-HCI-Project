using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        public User User { get; set; }
        public ObservableCollection<AccommodationReservation> FilteredAccommodations { get; set; }
        private readonly AccommodationReservationRepository _reservationRepository;
        private readonly GuestRepository _guestRepository;
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public static ObservableCollection<Model.Guest> Guests { get; set; }
        public ObservableCollection<GuestRating> FilteredRatings { get; set; }
        private readonly GuestRatingRepository _ratingRepository;
        public static ObservableCollection<GuestRating> Rating { get; set; }

        int brojac;

        public ProfileView(User user)
        {
            InitializeComponent();
            this.User = user;
            FilteredAccommodations = new ObservableCollection<AccommodationReservation>();
            FilteredAccommodations.Clear();
            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/profile.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            _reservationRepository = new AccommodationReservationRepository();
            _guestRepository = new GuestRepository();
            Reservations = new ObservableCollection<AccommodationReservation>(_reservationRepository.FindAll());
            NameLabel.Content = user.Username;
            Image.Visibility = Visibility.Collapsed;
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);
            Guests = new ObservableCollection<Model.Guest>(_guestRepository.FindAll());
            _ratingRepository = new GuestRatingRepository();
            Rating = new ObservableCollection<GuestRating>(_ratingRepository.FindAll());
            FilteredRatings = new ObservableCollection<GuestRating>();

            foreach (GuestRating rating in Rating)
            {
                if (rating.GuestId == user.Id)
                {
                    if (!FilteredRatings.Contains(rating))
                        FilteredRatings.Add(rating);
                    dataGridRatings.ItemsSource = FilteredRatings;
                }
            }
            dataGridRatings.ItemsSource = FilteredRatings;



            foreach (AccommodationReservation reservation in Reservations)
            {
                if (reservation.GuestId == user.Id && reservation.EndDate >= oneYearAgo)
                    brojac++;
            }

            foreach(Model.Guest guest in Guests)
            {
               if(guest.UserId == user.Id && brojac >= 10)
               {   
                    Image.Visibility = Visibility.Visible;
                    SuperGuestLabel.Content = "Super guest";
                    SuperGuestLabel.Foreground = Brushes.Goldenrod;
                    if (guest.IsSuperGuest)
                    {
                        return;
                    }
                    else
                    {
                        Model.Guest super = _guestRepository.FindById(guest.Id);
                        super.IsSuperGuest = true;
                        super.BonusPoints = 5;
                        _guestRepository.Update(super);
                    }                         
               }
               else
               {
                   SuperGuestLabel.Content = "Guest";
                   Model.Guest super1 = _guestRepository.FindByUserId(guest.UserId);
                   super1.IsSuperGuest = false;
                   super1.BonusPoints = 0;
                   _guestRepository.Update(super1);
               }

            }

        }

        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            GuestView guest = new GuestView(User);
            guest.Show();
        }
    }
}
