using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Type = InitialProject.Model.Type;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationRegistration.xaml
    /// </summary>
    public partial class AccommodationRegistration : Window
    {
        private readonly AccommodationRepository _accommodationRepository;

        private readonly LocationRepository _locationRepository;
        public User User { get; set; }
        public static List<Location> Locations { get; set; }
        public ObservableCollection<string> Types { get; set; }
        //public static List<string> Pictures { get; set; }

        private string _nameAcc;
        public string NameAcc
        {
            get => _nameAcc;
            set
            {
                if (value != _nameAcc)
                {
                    _nameAcc = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _location;
        public Location Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _minStay;
        public int MinStay
        {
            get => _minStay;
            set
            {
                if (value != _minStay)
                {
                    _minStay = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _daysToCancelBeforeReservation;
        public int DaysToCancelBeforeReservation
        {
            get => _daysToCancelBeforeReservation;
            set
            {
                if (value != _daysToCancelBeforeReservation)
                {
                    _daysToCancelBeforeReservation = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _picturesAcc;
        public string PicturesAcc
        {
            get => _picturesAcc;
            set
            {
                if (value != _picturesAcc)
                {
                    _picturesAcc = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HomePage_OnClick(object sender, RoutedEventArgs e)
        {
            OwnerHomePage ownerHomePage = new OwnerHomePage(User);
            ownerHomePage.Show();
            Close();

        }

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        public AccommodationRegistration(User owner)
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            //Pictures = new List<string>();
            Locations = new List<Location>(_locationRepository.FindAll());
            this.User = owner;

            Types = new ObservableCollection<string>();
            AccommodationTypeBox.Items.Add("Apartment");
            AccommodationTypeBox.Items.Add("House");
            AccommodationTypeBox.Items.Add("Cottage");

            foreach (Location location in Locations)
            {
                LocationComboBox.Items.Add(location.City);
            }
                
        }
           // LocationComboBox.Items.Insert(0, "");

        private void AccommodationRegistration_OnClick(object sender, RoutedEventArgs e)
        {
                Accommodation accommodation = new Accommodation();
                AccommodationService accommodationService = new AccommodationService();
                accommodation.Name = NameAcc;
                accommodation.Location=Location;
                accommodation.Type = (Type) AccommodationTypeBox.SelectedIndex;
                accommodation.LocationId = LocationComboBox.SelectedIndex;
                accommodation.OwnerId = User.Id;
              
                accommodation.Pictures = PicturesAcc.Split(',').ToList();
                accommodation.MaxGuests = Convert.ToInt32(_maxGuests);
                accommodation.MinStay = Convert.ToInt32(_minStay);
                accommodation.DaysToCancelBeforeReservation = Convert.ToInt32(_daysToCancelBeforeReservation);
                accommodationService.CreateAccommodation(accommodation);
                MessageBox.Show("Successfully registered an accommodation!");
            }

        private void LocationComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }



    }
}
