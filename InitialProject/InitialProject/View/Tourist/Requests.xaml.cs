using System;
using System.Collections.Generic;
using InitialProject.Model;
using System.Windows;
using InitialProject.Service;
using System.Collections.ObjectModel;
using InitialProject.Repository;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Window
    {
        public TourRequestStatisticsService StatisticsService = new TourRequestStatisticsService();
        public User User { get; set; }
        public TourRequestRepository tourRequestRepository { get; set; }
        public static ObservableCollection<TourRequest> TourRequests { get; set; }
        private double _oar;
        public double OAR
        {
            get => _oar;
            set
            {
                if (value != _oar) _oar = value;
            }
        }

        private double _odr;
        public double ODR
        {
            get => _odr;
            set
            {
                if (value != _odr) _odr = value;
            }
        }

        private double _ar23;
        public double AR23
        {
            get => _ar23;
            set
            {
                if (value != _ar23) _ar23 = value;
            }
        }

        private double _dr23;
        public double DR23
        {
            get => _dr23;
            set
            {
                if (value != _dr23) _dr23 = value;
            }
        }

        private double _ar22;
        public double AR22
        {
            get => _ar22;
            set
            {
                if (value != _ar22) _ar22 = value;
            }
        }

        private double _dr22;
        public double DR22
        {
            get => _dr22;
            set
            {
                if (value != _dr22) _dr22 = value;
            }
        }

        private double _ar21;
        public double AR21
        {
            get => _ar21;
            set
            {
                if (value != _ar21) _ar21 = value;
            }
        }

        private double _dr21;
        public double DR21
        {
            get => _dr21;
            set
            {
                if (value != _dr21) _dr21 = value;
            }
        }

        private double _oavg;
        public double OAVG
        {
            get => _oavg;
            set
            {
                if (value != _oavg) _oavg = value;
            }
        }

        private double _avg23;
        public double AVG23
        {
            get => _avg23;
            set
            {
                if (value != _avg23) _avg23 = value;
            }
        }

        private double _avg22;
        public double AVG22
        {
            get => _avg22;
            set
            {
                if (value != _avg22) _avg22 = value;
            }
        }

        private double _avg21;
        public double AVG21
        {
            get => _avg21;
            set
            {
                if (value != _avg21) _avg21 = value;
            }
        }

        private int _serbian;
        public int Serbian
        {
            get => _serbian;
            set
            {
                if (value != _serbian) _serbian = value;
            }
        }

        private int _english;
        public int English
        {
            get => _english;
            set
            {
                if (value != _english) _english = value;
            }
        }

        private int _ns;
        public int NS
        {
            get => _ns;
            set
            {
                if (value != _ns) _ns = value;
            }
        }

        private int _kop;
        public int KOP
        {
            get => _kop;
            set
            {
                if (value != _kop) _kop = value;
            }
        }

        public Requests(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
            tourRequestRepository = new TourRequestRepository();
            TourRequests = new ObservableCollection<TourRequest>(tourRequestRepository.FindByTouristId(User.Id));
            OAR = StatisticsService.GetStatisticsByStatus(User.Id, Convert.ToInt32(RequestStatusEnum.Accepted));
            ODR = StatisticsService.GetStatisticsByStatus(User.Id, Convert.ToInt32(RequestStatusEnum.Denied));
            AR23 = StatisticsService.GetStatisticsByStatus(User.Id, Convert.ToInt32(RequestStatusEnum.Accepted), Convert.ToDateTime("1/1/2023"));
            DR23 = StatisticsService.GetStatisticsByStatus(User.Id, Convert.ToInt32(RequestStatusEnum.Denied), Convert.ToDateTime("1/1/2023"));
            AR22 = StatisticsService.GetStatisticsByStatus(User.Id, Convert.ToInt32(RequestStatusEnum.Accepted), Convert.ToDateTime("1/1/2022"));
            DR22 = StatisticsService.GetStatisticsByStatus(User.Id, Convert.ToInt32(RequestStatusEnum.Denied), Convert.ToDateTime("1/1/2022"));
            AR21 = StatisticsService.GetStatisticsByStatus(User.Id, Convert.ToInt32(RequestStatusEnum.Accepted), Convert.ToDateTime("1/1/2021"));
            DR21 = StatisticsService.GetStatisticsByStatus(User.Id, Convert.ToInt32(RequestStatusEnum.Denied), Convert.ToDateTime("1/1/2021"));
            OAVG = StatisticsService.GetAverageNumberOfTouristsInAcceptedTourRequestsByYear(User.Id);
            AVG23 = StatisticsService.GetAverageNumberOfTouristsInAcceptedTourRequestsByYear(User.Id, Convert.ToDateTime("1/1/2023"));
            AVG22 = StatisticsService.GetAverageNumberOfTouristsInAcceptedTourRequestsByYear(User.Id, Convert.ToDateTime("1/1/2022"));
            AVG21 = StatisticsService.GetAverageNumberOfTouristsInAcceptedTourRequestsByYear(User.Id, Convert.ToDateTime("1/1/2021"));
            Serbian = StatisticsService.GetRequestCountByLanguage(User.Id, "Serbian");
            English = StatisticsService.GetRequestCountByLanguage(User.Id, "English");
            NS = StatisticsService.GetRequestCountByLocation(User.Id, 1);
            KOP = StatisticsService.GetRequestCountByLocation(User.Id, 2);
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

        private void TourRequests_OnClick(object sender, RoutedEventArgs e)
        {
            CreateTourRequest createTourRequest = new CreateTourRequest(User);
            createTourRequest.Show();
            Close();
        }

        //public Dictionary<int, DateTime> Years = new Dictionary<int, DateTime>()
        //   { { 2021, new DateTime(2021, 1, 1) } , { 2022, new DateTime(2022, 1, 1) } , { 2023, new DateTime(2023, 1, 1) }};

        
    }
}
