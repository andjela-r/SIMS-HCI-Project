using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationStatistics.xaml
    /// </summary>
    /// 
    public class Statistics
    {
        public int Year { get; set; }
        public int NumRes { get; set; }

        public int NumCanc { get; set; }

        public int NumMov { get; set; }

        public int NumSug { get; set; }

        public Statistics(int year, int numRes, int numCanc, int numMov, int numSug)
        {
            this.Year = year;
            this.NumRes = numRes;
            this.NumCanc = numCanc;
            this.NumMov= numMov;
            this.NumSug = numSug;
        }

        public Statistics() { }
    }

    public partial class AccommodationStatistics : Window
    {
        private readonly RequestStatusRepository _requestStatusRepository;
        private readonly RenovationSuggestionRepository _renovationSuggestionRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        public static ObservableCollection<RequestStatus> Requests { get; set; }
        public static ObservableCollection<RenovationSuggestion> Suggestions { get; set; }
        public static ObservableCollection<RenovationSuggestion> YourSuggestions { get; set; }
        public static ObservableCollection<RequestStatus> RequestsFinal { get; set; }
        public static ObservableCollection<Statistics> ListStatistics { get; set; }
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public static ObservableCollection<AccommodationReservation> YourReservations { get; set; }
        public static ObservableCollection<Accommodation> OwnerAccommodations { get; set; }
        public RequestStatus SelectedAccommodation { get; set; }
        public User User { get; set; }
        public int Year { get; set; }
        public Statistics obj { get; set; }


        public AccommodationStatistics(User owner)
        {
            InitializeComponent();
            this.DataContext = this;
            _requestStatusRepository = new RequestStatusRepository();
            _renovationSuggestionRepository = new RenovationSuggestionRepository();
            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            TableStatistics.Visibility = Visibility.Collapsed;
            this.User = owner;
            Suggestions=new ObservableCollection<RenovationSuggestion>(_renovationSuggestionRepository.FindAll());
            Requests = new ObservableCollection<RequestStatus>();
            YourReservations = new ObservableCollection<AccommodationReservation>();
            YourSuggestions = new ObservableCollection<RenovationSuggestion>();
            OwnerAccommodations = new ObservableCollection<Accommodation>(_accommodationRepository.FindByOwnerId(User.Id));
            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationRepository.FindByAccommodationId(AccommodationId));
            for (int i = 0; i < Reservations.Count; i++)
            {
                for (int j = 0; j < Requests.Count; j++)
                {
                    if (Requests[j].ReservationId == Reservations[i].Id)
                    {
                        Requests.Add(Requests[j]);
                    }

                }

            }
            RequestsFinal = new ObservableCollection<RequestStatus>(Requests);

            for (int i = 0; i < Suggestions.Count; i++)
            {
                for (int j = 0; j < Reservations.Count; j++)
                {
                    if (Reservations[j].Id == Suggestions[i].ReservationId)
                    {
                        YourSuggestions.Add(Suggestions[i]);
                        YourReservations.Add(Reservations[j]);
                    }

                }

            }
    }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _accommodationId;
        public int AccommodationId
        {
            get => _accommodationId;
            set
            {
                if (value != _accommodationId)
                {
                    _accommodationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _numRes;
        public int NumRes
        {
            get => _numRes;
            set
            {
                if (value != _numRes)
                {
                    _numRes = value;
                    OnPropertyChanged();
                }
            }
        }


        private int _numCanc;
        public int NumCanc
        {
            get => _numCanc;
            set
            {
                if (value != _numCanc)
                {
                    _numCanc = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _numMov;
        public int NumMov
        {
            get => _numMov;
            set
            {
                if (value != _numMov)
                {
                    _numMov = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _numSug;
        public int NumSug
        {
            get => _numSug;
            set
            {
                if (value != _numSug)
                {
                    _numSug = value;
                    OnPropertyChanged();
                }
            }
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


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            int counterRes2022 = 0;
            int counterRes2023 = 0;
            int counterMov2022 = 0;
            int counterMov2023 = 0;
            int counterSug2022 = 0;
            int counterSug2023 = 0;

            ListStatistics = new ObservableCollection<Statistics>();
            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationRepository.FindByAccommodationId(AccommodationId));
            
            for (int i=0; i<Reservations.Count; i++)
            {
                if (Reservations[i].StartDate.Year == 2023)
                {
                    counterRes2023++;
                }
            }

            for (int i = 0; i < YourReservations.Count; i++)
            {
                if (YourReservations[i].StartDate.Year == 2023)
                {
                    counterSug2023++;
                }
            }

            for (int i = 0; i < RequestsFinal.Count; i++)
            {
                if (RequestsFinal[i].StartDate.Year == 2023)
                {
                    counterMov2023++;
                }
            }
            Year = 2023;
            NumRes = counterRes2023;
            NumMov = counterMov2023;
            NumSug = counterSug2023;

            for (int i = 0; i < 1; i++)
            {
                obj = new Statistics(Year, NumRes, NumCanc, NumMov, NumSug);
                ListStatistics.Add(obj);

            }

            for (int i = 0; i < Reservations.Count; i++)
            {
                if (Reservations[i].StartDate.Year == 2022)
                {
                    counterRes2022++;
                }
            }

            for (int i = 0; i < YourReservations.Count; i++)
            {
                if (YourReservations[i].StartDate.Year == 2022)
                {
                    counterSug2022++;
                }
            }

            for (int i = 0; i < RequestsFinal.Count; i++)
            {
                if (RequestsFinal[i].StartDate.Year == 2022)
                {
                    counterMov2022++;
                }
            }
            Year = 2022;
            NumRes = counterRes2022;
            NumMov = counterMov2022;
            NumSug = counterSug2022;

            for (int i = 0; i < 1; i++)
            {
                obj = new Statistics(Year, NumRes, NumCanc, NumMov, NumSug);
                ListStatistics.Add(obj);
            }

            TableStatistics.ItemsSource = ListStatistics;
            TableStatistics.Visibility = Visibility.Visible;
           
        }
    }
}
