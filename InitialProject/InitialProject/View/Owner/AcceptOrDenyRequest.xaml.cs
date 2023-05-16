using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for AcceptOrDenyRequest.xaml
    /// </summary>
    public partial class AcceptOrDenyRequest : Window
    {
        private readonly RequestStatusRepository _requestStatusRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        public static ObservableCollection<RequestStatus> Requests { get; set; }
        public User User { get; set; }
        public static int id { get; set; }

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

        public AcceptOrDenyRequest(User owner)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = owner; 
            _requestStatusRepository = new RequestStatusRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
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
                var id = int.Parse(IdBlock.Text);
                RequestStatus request = _requestStatusRepository.FindById(id);
                AccommodationReservation accReservation = new AccommodationReservation();
                accReservation = _accommodationReservationRepository.FindById(request.ReservationId);
                request.Request = RequestStatusEnum.Accepted;
                request.IsChanged = true;
                _requestStatusRepository.Update(request);
                accReservation.StartDate = Convert.ToDateTime(request.StartDate);
                accReservation.EndDate = Convert.ToDateTime(request.EndDate);
                _accommodationReservationRepository.Update(accReservation);
                MessageBox.Show("Request accepted!");
        }

        private void DenyRequest(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(IdBlock.Text);
            RequestStatus request = _requestStatusRepository.FindById(id);
            if (request == null)
            {
                MessageBox.Show("That request does not exist!");
            }
            else if (_addedComment == null)
            {
                MessageBox.Show("Please leave an explanation.");
            } else 
            {
                request.Request = RequestStatusEnum.Denied;
                request.Comment = _addedComment.ToString();
                request.IsChanged = true;
                _requestStatusRepository.Update(request);
                MessageBox.Show("Request denied!");
            }

        }
    }
}
