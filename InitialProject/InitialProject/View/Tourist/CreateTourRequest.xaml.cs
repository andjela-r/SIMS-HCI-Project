using System;
using InitialProject.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.Service;
using System.Windows.Markup;
using InitialProject.Repository;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for CreateTourRequest.xaml
    /// </summary>
    public partial class CreateTourRequest : Window, INotifyPropertyChanged
    {
        public User User { get; set; }
        public TourRequestService tourRequestService = new TourRequestService();
        public LocationRepository locationRepository = new LocationRepository();

        public CreateTourRequest(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
            this.StartDate = DateTime.Now.ToString("MM/dd/yyyy");
            this.EndDate = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void Home_OnClick(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage(User);
            home.Show();
            Close();
        }
        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            Search search = new Search(User);
            search.Show();
            Close();
        }
        private void Requests_OnClick(object sender, RoutedEventArgs e)
        {
            Requests requests = new Requests(User);
            requests.Show();
            Close();
        }
        private void Vouchers_OnClick(object sender, RoutedEventArgs e)
        {
            Vouchers vouchers = new Vouchers(User);
            vouchers.Show();
            Close();
        }
        private void RegisteredTours_OnClick(object sender, RoutedEventArgs e)
        {
            RegisteredTours registeredTours = new RegisteredTours(User);
            registeredTours.Show();
            Close();
        }
        private void SentRequests_OnClick(object sender, RoutedEventArgs e)
        {
            SentRequests sentRequests = new SentRequests(User);
            sentRequests.Show();
            Close();
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language;
        public string TourLanguage
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;

        public string StartDate
        {
            get => _startDate.ToString();
            set
            {
                if (value != _startDate.ToString())
                {
                    _startDate = Convert.ToDateTime(value);
                    OnPropertyChanged();
                }
            }
        } 

        private DateTime _endDate;
        public string EndDate
        {
            get => _endDate.ToString();
            set
            {
                if (value != _endDate.ToString())
                {
                    _endDate = Convert.ToDateTime(value);
                    OnPropertyChanged();
                }
            }
        }
        public int LocationId { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void Send_OnClick(object sender, RoutedEventArgs e)
        {
            if (locationRepository.FindByCity(City) == null)
            {
                Location location = new Location();
                location.City = City;
                location.Country = Country;
                locationRepository.Save(location);
                LocationId = location.Id;
            }
            else
            {
                LocationId = locationRepository.FindByCity(City).Id;
            }

            TourRequest request = new TourRequest();

            request.TouristId = User.Id;
            request.Status = RequestStatusEnum.Waiting;
            request.Description = Description;
            request.Language = TourLanguage;
            request.StartDate = Convert.ToDateTime(StartDate);
            request.EndDate = Convert.ToDateTime(EndDate);
            request.MaxTourists = (int)Slider.Value;
            request.LocationId = LocationId;

            tourRequestService.CreateRequest(request);
            MessageBox.Show("Tour Request successfully created", "Status", MessageBoxButton.OK);
            SentRequests sentRequests = new SentRequests(User);
            sentRequests.Show();
            Close();
        }
    }
}
