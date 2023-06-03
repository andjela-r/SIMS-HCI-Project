using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media.Imaging;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerAndAccommodationRatingView.xaml
    /// </summary>
    public partial class OwnerAndAccommodationRatingView : Window
    {
        private readonly AccommodationReservationRepository _accomodationReservationRepository;
        private readonly AccommodationRepository _accomodationRepository;
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public ObservableCollection<AccommodationReservation> FilteredAccommodationsOld { get; set; }
        public ObservableCollection<AccommodationReservation> FilteredAccommodationsNew { get; set; }
        public User guest { get; set; }
        public string Name;

        public OwnerAndAccommodationRatingView(User guest)
        {
            InitializeComponent();
            DataContext = this;
            this.guest = guest;
            _accomodationRepository = new AccommodationRepository();
            _accomodationReservationRepository = new AccommodationReservationRepository();
            Reservations = new ObservableCollection<AccommodationReservation>(_accomodationReservationRepository.FindByGuestId(guest.Id));
            FilteredAccommodationsOld = new ObservableCollection<AccommodationReservation>();
            FilteredAccommodationsNew = new ObservableCollection<AccommodationReservation>();
            MoveButon.IsEnabled = false;
            RateButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            GoBackButton.Visibility = Visibility.Collapsed;

            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/resv.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            FilteredAccommodationsNew.Clear();
            FilteredAccommodationsOld.Clear();
            foreach (AccommodationReservation reservation in Reservations)
            {
                if (guest.Id == reservation.GuestId)
                {
                    if (reservation.EndDate < DateTime.Now)
                    {
                        if (!FilteredAccommodationsOld.Contains(reservation))
                            FilteredAccommodationsOld.Add(reservation);
                        dataGridAccommodationsOld.ItemsSource = FilteredAccommodationsOld;
                    }
                    else
                    {
                        if (!FilteredAccommodationsNew.Contains(reservation))
                            FilteredAccommodationsNew.Add(reservation);
                        dataGridAccommodationsNew.ItemsSource = FilteredAccommodationsNew;
                    }

                    Accommodation acc = _accomodationRepository.FindById(reservation.AccommodationId);
                    reservation.Name = acc.Name;
                }
                dataGridAccommodationsNew.ItemsSource = FilteredAccommodationsNew;
                dataGridAccommodationsOld.ItemsSource = FilteredAccommodationsOld;



            }
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            var dayDifference = DateTime.Today - SelectedReservation.EndDate;
            if (SelectedReservation.IsRated == true)
            {
                MessageBox.Show("You already rated this accommodation.");
            }
            else
            {
                if (dayDifference.Days > 5)
                {
                    MessageBox.Show("Sorry, your deadline for rating has passed.");
                }
                else
                {
                    RatingView rating = new RatingView(SelectedReservation, guest);
                    rating.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    rating.Show();
                }
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            dataGridAccommodationsOld.IsEnabled = true;
            dataGridAccommodationsNew.IsEnabled = true;
            MoveButon.IsEnabled = false;
            CancelButton.IsEnabled = false;
            GoBackButton.IsEnabled = false;
            RateButton.IsEnabled = false;
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
            if (SelectedReservation.IsRequested == false)
            {
                RescheduleView reschedule = new RescheduleView(SelectedReservation);
                reschedule.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                reschedule.Show();
            }
            else
            {
                RescheduleStatusView status = new RescheduleStatusView(SelectedReservation);
                status.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                status.Show();
            }
        }

        private void MyDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
                if (SelectedReservation.EndDate > DateTime.Today)
                {
                    dataGridAccommodationsOld.IsEnabled = false;
                    dataGridAccommodationsNew.IsEnabled = false;
                    MoveButon.IsEnabled = true;
                    CancelButton.IsEnabled = true;
                    GoBackButton.Visibility = Visibility.Visible;
                    GoBackButton.IsEnabled = true;
            }
                else
                {
                    dataGridAccommodationsOld.IsEnabled = false;
                    dataGridAccommodationsNew.IsEnabled = false;
                    GoBackButton.Visibility = Visibility.Visible;
                    RateButton.IsEnabled = true;
                    GoBackButton.IsEnabled = true;
                }
            
        }
    }
}
