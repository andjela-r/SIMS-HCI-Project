using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationView.xaml
    /// </summary>
    public partial class AccommodationView : Window
    {
        private readonly AccommodationRepository _accomodationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly AccommodationReservationService _reservationService;
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public static List<Location> Locations { get; set; } 
        public ObservableCollection<string> Types { get; set; }
        public User Guest { get; set; }

        public AccommodationView(User guest)
        {
            InitializeComponent();
            DataContext = this;
            this.Guest = guest;
            _accomodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _reservationService = new AccommodationReservationService();
            Accommodations = new ObservableCollection<Accommodation>(_accomodationRepository.FindAll());
            Locations = new List<Location>(_locationRepository.FindAll());

            Types = new ObservableCollection<string>();
            TypeBox.Items.Add("");
            TypeBox.Items.Add("Apartment");
            TypeBox.Items.Add("House");
            TypeBox.Items.Add("Cottage");

            foreach (Accommodation accommodation in Accommodations)
            {
                accommodation.Location = Locations.Find(l => l.Id == accommodation.LocationId);
            }
            foreach (Accommodation accommodation in Accommodations)
            {
                if (LocationComboBox.Items.Contains(accommodation.Location.City))
                {
                    break;
                }
                LocationComboBox.Items.Add(accommodation.Location.City);
            }
            LocationComboBox.Items.Insert(0, "");

            FilteredAccommodations = new ObservableCollection<Accommodation>();   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FilteredAccommodations.Clear();
            foreach (Accommodation accommodation in Accommodations)
            {
                if (IsAccommodationMatchingSearchCriteria(accommodation))
                {
                    if(!FilteredAccommodations.Contains(accommodation))
                        FilteredAccommodations.Add(accommodation);
                    dataGridAccommodations.ItemsSource = FilteredAccommodations;
                }
            }
            dataGridAccommodations.ItemsSource = FilteredAccommodations;
        }

        public bool IsAccommodationMatchingSearchCriteria(Accommodation accommodation)
        {
            string name = NameBox.Text.ToLower();
            string[] nameWords = name.Split(' ');
            string location = LocationComboBox.Text.Replace(",", "").Replace(" ", "");
            string type = (string)TypeBox.SelectedItem;
            string guestNumber = GuestNumBox.Text;
            string daysForReservation = MinDaysBox.Text;
            bool result = false;

            if ((_reservationService.IsContainingNameWords(accommodation, nameWords) || string.IsNullOrEmpty(name)) &&
                (accommodation.Location.City.Replace(" ", "").Contains(location) || string.IsNullOrEmpty(location))&&
                (_reservationService.HasMatchingAccommodationType(accommodation, type) || string.IsNullOrEmpty(type)) &&
                (_reservationService.IsGuestNumberLessThanMaximum(accommodation, guestNumber) || string.IsNullOrEmpty(guestNumber)) &&
                (_reservationService.IsReservationGreaterThanMinimum(accommodation, daysForReservation) ||
                 string.IsNullOrEmpty(daysForReservation)))
                result = true;
            return result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                AccommodationReservationView reservationView = new AccommodationReservationView(SelectedAccommodation, Guest);
                reservationView.Show();
            }
            else
            {
                MessageBox.Show("Please select accommodation for reservation.");
            }
        }

    }
}
