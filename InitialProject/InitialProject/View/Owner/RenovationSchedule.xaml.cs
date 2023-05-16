using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for RenovationSchedule.xaml
    /// </summary>
    public partial class RenovationSchedule : Window
    {
        private readonly RenovationRepository _renovationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        public static ObservableCollection<Renovation> Renovations { get; set; }
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public static ObservableCollection<Accommodation> YourAccommodations { get; set; }
        public RequestStatus SelectedAccommodation { get; set; }
        public User User { get; set; }

        public RenovationSchedule(User owner)
        {
            InitializeComponent();
            this.DataContext = this;
            _renovationRepository = new RenovationRepository();
            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            
            this.User = owner;
            Renovations = new ObservableCollection<Renovation>();
            YourAccommodations = new ObservableCollection<Accommodation>(_accommodationRepository.FindByOwnerId(User.Id));
            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationRepository.FindByAccommodationId(_accId));
        }

        private int _accId;  
        public int AccId
        {
            get => _accId;
            set
            {
                if (value != _accId)
                {
                    _accId = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }


        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _expectedDuration;
        public int ExpectedDuration
        {
            get => _expectedDuration;
            set
            {
                if (value != _expectedDuration)
                {
                    _expectedDuration = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void HomePage_OnClick(object sender, RoutedEventArgs e)
        {
            OwnerHomePage ownerHomePage = new OwnerHomePage(User);
            ownerHomePage.Show();
            Close();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)StartDatePicker.SelectedDate;
            DateTime endDate = (DateTime)EndDatePicker.SelectedDate;
            int probCounter = 0;

            for (int i=0; i<Reservations.Count; i++)
            {
                if (((startDate < Reservations[i].StartDate) && (endDate > Reservations[i].StartDate)) || ((startDate < Reservations[i].EndDate) && (endDate > Reservations[i].EndDate)))
                {
                    probCounter++;
                    
                }
            }

            if (probCounter > 0)
            {
                MessageBox.Show("You can not make a schedule because of a reservation!");
            } else
            {
                  Renovation renovation = new Renovation();
                  RenovationService renovationService = new RenovationService();
                  renovation.StartDate = Convert.ToDateTime(startDate);
                  renovation.EndDate = Convert.ToDateTime(endDate);
                  renovation.ExpectedDuration=Convert.ToInt32(ExpectedDuration);
                  renovation.Description = Convert.ToString(Description);
                  renovation.AccommodationId = Convert.ToInt32(AccId);
                  renovationService.CreateRenovation(renovation);
                  MessageBox.Show("Renovation successfully scheduled!");
            }
                
            }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    }
