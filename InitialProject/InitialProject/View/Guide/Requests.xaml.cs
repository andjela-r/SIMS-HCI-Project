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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Window
    {
        public User User { get; set; }

        private readonly LocationRepository _locationRepository;
        private readonly TourRequestRepository _tourRequestRepository;
        private readonly PartOfComplexTourRequestRepository _partOfComplexTourRequestRepository;

        private readonly TourRequestService _tourRequestService;

        public ObservableCollection<TourRequest> FilteredTourRequests { get; set; }

        public static ObservableCollection<TourRequest> TourRequests { get; set; }
        public static ObservableCollection<TourRequest> ComplexTourRequests { get; set; }

        public static List<Location> Locations { get; set; }
        public Requests(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
            usernameLabel.Content = "Hello, \n" + user.Username + "!";

            _locationRepository = new LocationRepository();
            _tourRequestRepository = new TourRequestRepository();
            _partOfComplexTourRequestRepository = new PartOfComplexTourRequestRepository();
            _tourRequestService = new TourRequestService();

            TourRequests = new ObservableCollection<TourRequest>(_tourRequestRepository.FindAll());
            ComplexTourRequests = new ObservableCollection<TourRequest>(_partOfComplexTourRequestRepository.FindAll());

            Locations = new List<Location>(_locationRepository.FindAll());

            foreach (TourRequest tourRequest in TourRequests)
            {
                tourRequest.Location = Locations.Find(l => l.Id == tourRequest.LocationId);
            }

            foreach (TourRequest ComplexTourRequest in ComplexTourRequests)
            {
                ComplexTourRequest.Location = Locations.Find(l => l.Id == ComplexTourRequest.LocationId);
            }

            foreach (Location location in Locations)
            {
                TRLocationBox.Items.Add(location.City);
            }

            FilteredTourRequests = new ObservableCollection<TourRequest>();
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

        private TourRequest GetSelectedDataFromDataGrid()
        {
            if (TourRequestDataGrid.SelectedItem is TourRequest selectedItem)
            {
                return selectedItem;
            }

            return null;
        }

        private TourRequest GetSelectedDataFromComplexDataGrid()
        {
            if (ComplexTourRequestDataGrid.SelectedItem is TourRequest selectedItem)
            {
                return selectedItem;
            }

            return null;
        }

        private void Create_OnClick(object sender, RoutedEventArgs e)
        {
            TourRequest selectedData = GetSelectedDataFromDataGrid();

            if (selectedData.Status != RequestStatusEnum.Waiting)
            {
                System.Windows.MessageBox.Show("Tour request has to have waiting status!", "Status", MessageBoxButton.OK);
                return;
            }

            if (selectedData != null)
            {
                CreateTour createTour = new CreateTour(User, selectedData);
                createTour.Show();
                Close();
            }
        }
        private void SearchTourRequests_OnClick(object sender, RoutedEventArgs e)
        {
            FilteredTourRequests.Clear();
            foreach (TourRequest tourRequest in TourRequests)
            {
                if (IsTourRequestMatchingSearchCriteria(tourRequest))
                {
                    if (!FilteredTourRequests.Contains(tourRequest))
                        FilteredTourRequests.Add(tourRequest);
                    TourRequestDataGrid.ItemsSource = FilteredTourRequests;
                }
            }
            TourRequestDataGrid.ItemsSource = FilteredTourRequests;
        }

        public bool IsTourRequestMatchingSearchCriteria(TourRequest tourRequest)
        {
            string location = TRLocationBox.Text.Replace(",", "").Replace(" ", "");
            string touristNumber = MaxGuestsBox.Text;
            string language = LanguageBox.Text.ToLower();
            string[] languageWords = language.Split(' ');
            
            DateTime startDate = StartDateBox.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = StartDateBox.SelectedDate ?? DateTime.MaxValue;

            bool result = false;

            if ((tourRequest.Location.City.Replace(" ", "").Contains(location) || string.IsNullOrEmpty(location)) &&
                (_tourRequestService.IsTouristNumberLessThanMaximum(tourRequest, touristNumber) || string.IsNullOrEmpty(touristNumber)) &&
                (_tourRequestService.IsContainingLanguageWords(tourRequest, languageWords) || string.IsNullOrEmpty(language)) &&
                (_tourRequestService.IsWithinDateRange(tourRequest, startDate, endDate) || startDate==DateTime.MinValue || endDate==DateTime.MaxValue))
                    result = true;

            return result;
        }

        public void Select_OnClick(object sender, RoutedEventArgs e)
        {
            TourRequest selectedData = GetSelectedDataFromComplexDataGrid();

            if (selectedData.Status != RequestStatusEnum.Waiting)
            {
                System.Windows.MessageBox.Show("Part of complex tour request has to have waiting status!", "Status", MessageBoxButton.OK);
                return;
            }

            if (selectedData != null)
            {
                PickDate pickDate = new PickDate(selectedData);
                pickDate.Show();
            }

        }
    }
    
}
