using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InitialProject.DTO;
using System.Collections;
using System.Security.Cryptography;
using Xceed.Wpf.Toolkit;
using System.Windows.Controls.DataVisualization.Charting;
using System.Linq;
using System.Windows.Controls.DataVisualization.Charting;


namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        public User User { get; set; }
        private TourDTO newTour = new TourDTO();
        private TourAppointmentDTO newTourAppointment = new TourAppointmentDTO();
        private KeyPointDTO newKeyPoint = new KeyPointDTO();
        private List<int> selectedKeyPointIds = new List<int>();
        public string SelectedCity { get; set; }

        private TourService _tourService = new TourService();
        private TourAppointmentService _tourAppointmentService = new TourAppointmentService();
        private KeyPointService _keyPointService = new KeyPointService();
        private TourRequestService _tourRequestService = new TourRequestService();

        public static ObservableCollection<TourRequest> TourRequests { get; set; }

        private readonly LocationRepository _locationRepository;
        private readonly TourRepository _tourRepository;
        private readonly KeyPointRepository _keyPointRepository;
        private readonly TourAppointmentRepository _tourAppointmentRepository;

        public static List<Model.Location> Locations { get; set; }
        public static List<Tour> Tours { get; set; }
        public static List<KeyPoint> KeyPoints { get; set; }

        public CreateTour(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;

            _locationRepository = new LocationRepository();
            _tourRepository = new TourRepository();
            _keyPointRepository = new KeyPointRepository();

            Locations = new List<Model.Location>(_locationRepository.FindAll());
            Tours = new List<Tour>(_tourRepository.FindAll());
            KeyPoints = new List<KeyPoint>(_keyPointRepository.FindAll());

            usernameLabel.Content = "Hello, \n" + user.Username + "!";

            foreach (Model.Location location in Locations)
            {
                LocationBox.Items.Add(location.Id + "-" + location.City);
            }

            foreach (Model.Location location in Locations)
            {
                TRLocationBox.Items.Add(location.Id + "-" + location.City);
            }

            foreach (Tour tour in Tours)
            {
                TourBox.Items.Add(tour.Id + "-" + tour.Name);
            }
        }

        public CreateTour(User user, string language)
        {
            InitializeComponent();

            this.DataContext = this;
            this.User = user;
            usernameLabel.Content = "Hello, \n" + user.Username + "!";

            LanguageBox.Text = language;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            Tours = new List<Tour>(_tourRepository.FindAll());
            Locations = new List<Model.Location>(_locationRepository.FindAll());
            foreach (Tour tour in Tours)
            {
                TourBox.Items.Add(tour.Id + "-" + tour.Name);
            }
            foreach (Model.Location location in Locations)
            {
                TRLocationBox.Items.Add(location.Id + "-" + location.City);
            }
            foreach (Model.Location location in Locations)
            {
                LocationBox.Items.Add(location.Id + "-" + location.City);
            }
        }

        public CreateTour(User user, int locationId)
        {
            InitializeComponent();

            this.DataContext = this;
            this.User = user;
            usernameLabel.Content = "Hello, \n" + user.Username + "!";

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            Tours = new List<Tour>(_tourRepository.FindAll());
            Locations = new List<Model.Location>(_locationRepository.FindAll());
            foreach (Tour tour in Tours)
            {
                TourBox.Items.Add(tour.Id + "-" + tour.Name);
            }
            foreach (Model.Location location in Locations)
            {
                TRLocationBox.Items.Add(location.Id + "-" + location.City);
            }
            /*foreach (Model.Location location in Locations)
            {
                LocationBox.Items.Add(location.Id + "-" + location.City);
            }*/
            foreach (Model.Location location in Locations)
            {
                if (location.Id == locationId)
                {
                    LocationBox.Items.Add(location.Id + "-" + location.City);
                    LocationBox.SelectedItem = location.Id + '-' + location.City;
                    break;
                }
            }
        }

        public CreateTour(User user, TourRequest selectedData)
        {
            InitializeComponent();

            this.DataContext = this;
            this.User = user;
            usernameLabel.Content = "Hello, \n" + user.Username + "!";

            LanguageBox.Text = selectedData.Language;
            MaxGuestsBox.Text = selectedData.MaxTourists.ToString();
            DescriptionBox.Text = selectedData.Description;

            DateBox.DisplayDateStart = selectedData.StartDate;
            DateBox.DisplayDateEnd = selectedData.EndDate;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            Tours = new List<Tour>(_tourRepository.FindAll());
            Locations = new List<Model.Location>(_locationRepository.FindAll());
            foreach (Tour tour in Tours)
            {
                TourBox.Items.Add(tour.Id + "-" + tour.Name);
            }
            foreach (Model.Location location in Locations)
            {
                if (location.City == selectedData.Location.City)
                {
                    LocationBox.Items.Add(location.Id + "-" + location.City);
                    LocationBox.SelectedItem = location.Id +'-'+ location.City;
                    break;
                }
            }

            foreach (Model.Location location in Locations)
            {
                TRLocationBox.Items.Add(location.Id + "-" + location.City);
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void CreateTour_OnClick(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour(User);
            createTour.Show();
            Close();
        }

        private void Statistics_OnClick(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics(User);
            statistics.Top = this.Top;
            statistics.Show();
            Close();
        }

        private void Requests_OnClick(object sender, RoutedEventArgs e)
        {
            Requests requests = new Requests(User);
            requests.Show();
            Close();
        }

        private void MyProfile_OnClick(object sender, RoutedEventArgs e)
        {
            MyProfile myProfile = new MyProfile(User);
            myProfile.Show();
            Close();
        }

        private void Tours_OnClick(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new ToursView(User);
            toursView.Show();
            Close();
        }

        private void CreateNewTour_OnClick(object sender, RoutedEventArgs e)
        {
            int locationId = int.Parse(LocationBox.Text.Split('-')[0].Trim());

            newTour.Name = NameBox.Text;
            newTour.Language = LanguageBox.Text;
            newTour.Duration = float.Parse(DurationBox.Text);
            newTour.MaxTourists = int.Parse(MaxGuestsBox.Text);
            newTour.LocationId = locationId;
            newTour.GuideId = this.User.Id;
            newTour.Pictures = PicturesBox.Text.Split(',').ToList();
            newTour.Description = DescriptionBox.Text;

            int id = _tourService.CreateTour(newTour);
            System.Windows.MessageBox.Show("Tour created successfully!", "Status", MessageBoxButton.OK);
            
            TourBox.Items.Add( id + "-" + newTour.Name);

            NameBox.Text = string.Empty;
            LanguageBox.Text = string.Empty;
            DurationBox.Text = string.Empty;
            MaxGuestsBox.Text = string.Empty;
            LocationBox.Text = string.Empty;
            PicturesBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;

        }

        private void CreateNewAppointment_OnClick(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = DateBox.SelectedDate ?? DateTime.MinValue;
            DateTime selectedTime = (DateTime)TimeBox.Value;
            DateTime selectedDateTime = selectedDate.Date + selectedTime.TimeOfDay;

            newTourAppointment.TourId = int.Parse(TourBox.Text.Split('-')[0].Trim());
            newTourAppointment.StartTime = selectedDateTime;
            newTourAppointment.KeyPointIds = selectedKeyPointIds.ToList();

            _tourAppointmentService.CreateAppointment(newTourAppointment);
            System.Windows.MessageBox.Show("Tour appointment created successfully!", "Status", MessageBoxButton.OK);

            CreateTour createTour = new CreateTour(User);
            createTour.Show();
            Close();
        }

        private void CreateNewKeyPoint_OnClick(object sender, RoutedEventArgs e)
        {
            newKeyPoint.Name = KeyPointNameBox.Text;

            int id = _keyPointService.CreateKeyPoint(newKeyPoint);
            selectedKeyPointIds.Add(id);
            System.Windows.MessageBox.Show("Key point created successfully!", "Status", MessageBoxButton.OK);

            KeyPointNameBox.Text = string.Empty;
        }

        private void GetTouristsNumber_OnClick(object sender, RoutedEventArgs e)
        {
            string language = TRLanguageBox.Text.ToLower();

            string location = TRLocationBox.Text.Split('-')[0].Trim();

            string year = TRYearBox.Text;


            string locationId = TRLocationBox.Text.Split('-')[0].Trim();

            int result = _tourRequestService.GetNumberOfTourRequestsForStatistcs(language, locationId, year);

            GuestNumberBox.Text = result.ToString();
        }

        private void RecommendTourByLanguage_OnClick(object sender, RoutedEventArgs e)
        {
            string language = _tourRequestService.RecommendTourLanguage();

            CreateTour createTour = new CreateTour(User, language);
            createTour.Show();
            Close();
        }

        private void RecommendTourByLocation_OnClick(object sender, RoutedEventArgs e)
        {
            int locationId = _tourRequestService.RecommendTourLocation();

            CreateTour createTour = new CreateTour(User, locationId);
            createTour.Show();
            Close();
        }

    }
}
