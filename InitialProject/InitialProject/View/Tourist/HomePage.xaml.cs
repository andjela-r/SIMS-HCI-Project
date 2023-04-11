using System;
using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.Generic;
using System.Windows;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public TourRepository _tourRepository = new TourRepository();
        public TourAppointmentRepository _tourAppointmentRepository = new TourAppointmentRepository();

        public User User { get; set; }

        private string _tour;
        private string _tour2;
        private string _tour3;
        private int _tourId;
        private int _tourId1;
        private int _tourId2;

        private string _tour11;
        private string _tour21;
        private string _tour31;

        private string _tour20;
        private string _tour22;
        private string _tour32;

        private List<Tour> _tours;

        public string TourName
        {
            get => _tour;
            set
            {
                if (value != _tour) _tour = value;
            }
        }

        public string TourDescription
        {
            get => _tour2;
            set
            {
                if (value != _tour2) _tour2 = value;
            }
        }

        public string ImagePath
        {
            get => _tour3;
            set
            {
                if (value != _tour3) _tour3 = value;
            }
        }

        public int TourId
        {
            get => _tourId;
            set
            {
                if (value != _tourId) _tourId = value;
            }
        }
        public int TourId1
        {
            get => _tourId1;
            set
            {
                if (value != _tourId1) _tourId1 = value;
            }
        }
        public int TourId2
        {
            get => _tourId2;
            set
            {
                if (value != _tourId2) _tourId2 = value;
            }
        }

        public string TourName1
        {
            get => _tour11;
            set
            {
                if (value != _tour11) _tour11 = value;
            }
        }

        public string TourDescription1
        {
            get => _tour21;
            set
            {
                if (value != _tour21) _tour21 = value;
            }
        }

        public string ImagePath1
        {
            get => _tour31;
            set
            {
                if (value != _tour31) _tour31 = value;
            }
        }

        public string TourName2
        {
            get => _tour20;
            set
            {
                if (value != _tour20) _tour20 = value;
            }
        }

        public string TourDescription2
        {
            get => _tour22;
            set
            {
                if (value != _tour22) _tour22 = value;
            }
        }

        public string ImagePath2
        {
            get => _tour32;
            set
            {
                if (value != _tour32) _tour32 = value;
            }
        }

        public HomePage(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
            User = user;

            var turica = _tourRepository.FindById(1);
            var turicaa = _tourAppointmentRepository.FindById(1);
            TourName = turica.Name;
            TourDescription = turica.Description;
            ImagePath = turica.Pictures[0];
            TourId = turicaa.Id;

            var turica1 = _tourRepository.FindById(2);
            var turicaa1 = _tourAppointmentRepository.FindById(2);
            TourName1 = turica1.Name;
            TourDescription1 = turica1.Description;
            ImagePath1 = turica1.Pictures[0];
            TourId1 = turicaa1.Id;

            var turica2 = _tourRepository.FindById(3);
            var turicaa2 = _tourAppointmentRepository.FindById(3);
            TourName2 = turica2.Name;
            TourDescription2 = turica2.Description;
            ImagePath2 = turica2.Pictures[0];
            TourId2 = turicaa2.Id;

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

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void Link_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
