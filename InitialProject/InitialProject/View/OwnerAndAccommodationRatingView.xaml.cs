using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerAndAccommodationRatingView.xaml
    /// </summary>
    public partial class OwnerAndAccommodationRatingView : Window
    {
        private readonly AccommodationReservationRepository _accomodationReservationRepository;
        private readonly RequestStatusRepository _requestStatusRepository;
        private readonly AccommodationRepository _accomodationRepository;
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public ObservableCollection<AccommodationReservation> FilteredAccommodations { get; set; }
        public User guest { get; set; }

        public OwnerAndAccommodationRatingView(User guest)
        {
            InitializeComponent();
            DataContext = this;
            this.guest = guest;
            _accomodationRepository = new AccommodationRepository();
            _accomodationReservationRepository = new AccommodationReservationRepository();
            _requestStatusRepository = new RequestStatusRepository();
            Reservations = new ObservableCollection<AccommodationReservation>(_accomodationReservationRepository.FindByGuestId(guest.Id));
            FilteredAccommodations = new ObservableCollection<AccommodationReservation>();
            MoveButon.Visibility = Visibility.Collapsed;
            RateButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            GoBackButton.Visibility = Visibility.Collapsed;

            FilteredAccommodations.Clear();
            foreach (AccommodationReservation reservation in Reservations)
            {
                if (guest.Id == reservation.GuestId)
                {
                    if (!FilteredAccommodations.Contains(reservation))
                        FilteredAccommodations.Add(reservation);
                    dataGridAccommodations.ItemsSource = FilteredAccommodations;
                }
            }
            dataGridAccommodations.ItemsSource = FilteredAccommodations;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                 if (SelectedReservation.EndDate > DateTime.Today)
                {
                    dataGridAccommodations.IsEnabled = false;
                    MoveButon.Visibility = Visibility.Visible;
                    CancelButton.Visibility = Visibility.Visible;
                    SelectButton.Visibility = Visibility.Collapsed;
                    GoBackButton.Visibility = Visibility.Visible;
                }
                else
                {
                    dataGridAccommodations.IsEnabled = false;
                    GoBackButton.Visibility = Visibility.Visible;
                    RateButton.Visibility = Visibility.Visible;
                    SelectButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Please select accommodation for reservation.");
            }

        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            var dayDifference = DateTime.Today - SelectedReservation.EndDate;
            if(dayDifference.Days > 5)
            {
                MessageBox.Show("Sorry, your deadline for rating has passed.");
            }
            else
            {
                RatingView rating = new RatingView(SelectedReservation, guest);
                rating.Show();
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            dataGridAccommodations.IsEnabled = true;
            MoveButon.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            SelectButton.Visibility = Visibility.Visible;
            GoBackButton.Visibility = Visibility.Collapsed;
            RateButton.Visibility = Visibility.Collapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var accommodation = _accomodationRepository.FindById(SelectedReservation.AccommodationId);
            var dayDifference = SelectedReservation.StartDate - DateTime.Today;
            if (dayDifference.Days < accommodation.DaysToCancelBeforeReservation)
            {
                    MessageBox.Show("Sorry, Canceletion is not posiible.");
                    return;
            }
            else
            {
                _accomodationReservationRepository.DeleteById(SelectedReservation.Id);
                MessageBox.Show("We will inform the owmer.");
            }
        }

        private void MoveButon_Click(object sender, RoutedEventArgs e)
        {
            RescheduleView reschedule = new RescheduleView(SelectedReservation);
            reschedule.Show();
        }
    }
}
