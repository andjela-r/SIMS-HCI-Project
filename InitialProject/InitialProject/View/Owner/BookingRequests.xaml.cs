using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for BookingRequests.xaml
    /// </summary>
    public partial class BookingRequests : Window
    {
        private readonly RequestStatusRepository _requestStatusRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        public static ObservableCollection<RequestStatus> Requests { get; set; }
        public RequestStatus SelectedRequest { get; set; }
        public User User { get; set; }

        private int _requestId;
        public int RequestId
        {
            get => _requestId;
            set
            {
                if (value != _requestId)
                {
                    _requestId = value;
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


        private int _reservationId;
        public int ReservationId
        {
            get => _reservationId;
            set
            {
                if (value != _reservationId)
                {
                    _reservationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _addedComment;
        public string AddedComment
        {
            get => _addedComment;
            set
            {
                if (value != _addedComment)
                {
                    _addedComment = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public BookingRequests(User owner)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = owner;
            _requestStatusRepository = new RequestStatusRepository();
            _accommodationReservationRepository=new AccommodationReservationRepository();
            Requests = new ObservableCollection<RequestStatus>(_requestStatusRepository.FindAll());
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

        private void AcceptRequest(object sender, RoutedEventArgs e)
        {
            RequestStatus request = _requestStatusRepository.FindById(_requestId);
            
            if (request == null)
            {
                MessageBox.Show("That request does not exist!");
            } else
            {
                AccommodationReservation accReservation = new AccommodationReservation();
                accReservation = _accommodationReservationRepository.FindById(request.ReservationId);
                request.Request = RequestStatusEnum.Accepted;
                _requestStatusRepository.Update(request);
                accReservation.StartDate=Convert.ToDateTime(request.StartDate);
                accReservation.EndDate=Convert.ToDateTime(request.EndDate);
               _accommodationReservationRepository.Update(accReservation);
               MessageBox.Show("Request accepted!");
            }
        }

        private void DenyRequest(object sender, RoutedEventArgs e)
        {
            RequestStatus request = _requestStatusRepository.FindById(_requestId);
            if (request == null)
            {
                MessageBox.Show("That request does not exist!");
            }
            else
            {
                request.Request = RequestStatusEnum.Denied;
                request.Comment = _addedComment.ToString();
                _requestStatusRepository.Update(request);
                MessageBox.Show("Request denied!");
            }

        }
    }
}
