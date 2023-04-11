using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
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
using InitialProject.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<Voucher> Vouchers { get; set; }

        public Voucher SelectedVoucher { get; set; }
        public User User { get; set; }
        public int TourId { get; set; }

        private TourRepository _tourRepository = new TourRepository();
        private TourReservationService _tourReservationService = new TourReservationService();
        private TourReservation newReservation = new TourReservation();
        private readonly VoucherRepository _voucherRepository;

        private TourAppointmentService _tourAppointmentService = new TourAppointmentService();
        private TourAppointmentRepository _tourAppointmentRepository = new TourAppointmentRepository();

        private string _name;
        private string _status;
        private string _description;
        private string _picture;
        private DateTime _date;
        private string _voucher;
        private int _maxTourists;
        private int _availableSeats;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #region NotifyProperty
        public string VoucherName
        {
            get => _voucher;
            set
            {
                if (value != _voucher)
                {
                    _voucher = value;
                    OnPropertyChanged("VoucherName");
                }
            }
        }
        #endregion


        public string TourName
        {
            get => _name;
            set
            {
                if (value != _name) _name = value;
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (value != _status) _status = value;
            }
        }

        public string TourDescription
        {
            get => _description;
            set
            {
                if (value != _description) _description = value;
            }
        }

        public string PicturePath
        {
            get => _picture;
            set
            {
                if (value != _picture) _picture = value;
            }
        }

        public string Date

        {
            get => _date.ToString();
            set
            {
                if (value != _date.ToString()) _date = Convert.ToDateTime(value);
            }
        }

        public int MaxTourists
        {
            get => _maxTourists;
            set
            {
                if (value != _maxTourists) _maxTourists = value;
            }
        }

        public int AvailableSeats
        {
            get => _availableSeats;
            set
            {
                if (value != _availableSeats) _availableSeats = value;
            }
        }

        public BookWindow(User user, int appointmentId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
            this.TourId = appointmentId;
            _voucherRepository = new VoucherRepository();
            Vouchers = new ObservableCollection<Voucher>(_voucherRepository.FindByTouristId(user.Id));

            var appointment = _tourAppointmentRepository.FindById(appointmentId);
            var tour = _tourRepository.FindById(appointment.TourId);
            TourName = tour.Name;
            TourDescription = tour.Description;
            PicturePath = tour.Pictures[0];
            Date = appointment.StartTime.ToString("D");
            MaxTourists = tour.MaxTourists;
            AvailableSeats = appointment.AvailableSeats;
            Status = "Can be booked";
            if (appointment.AvailableSeats == 0)
            {
                Status = "Tour is full :(";
            }
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Book_OnClick(object sender, RoutedEventArgs e)
        {
            var appointment = _tourAppointmentRepository.FindById(TourId);

            newReservation.NumberOfTourists = (int)Slider.Value;
            newReservation.TourId = TourId;
            newReservation.TouristId = this.User.Id;

            if (appointment.AvailableSeats == 0)
            {
                var result = MessageBox.Show("Unfortunately, the tour you've chosen doesn't have any seats left.\nWould you like to pick another tour?", "Status", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Showing available tours on the same location", "Status", MessageBoxButton.OK);
                }
                else
                {
                    Close();
                }
            }
            else
            {
                if (appointment.AvailableSeats >= newReservation.NumberOfTourists)
                {
                    _tourReservationService.Book(newReservation);
                    if (SelectedVoucher != null) _voucherRepository.Delete(SelectedVoucher);
                    MessageBox.Show("Successfully booked tour!", "Status", MessageBoxButton.OK);
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "The number of tourists is larger than the number of available seats.\nPlease change the number of tourists or cancel.",
                        "Status", MessageBoxButton.OK);
                }
            }

            /*

                    //"Unfortunately, the tour you've chosen doesn't have any seats left.\nWould you like to pick another tour? (y/n) ");
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "y":
                        //Console.WriteLine("Tours on the same location: ");
                        var locationId = tour.LocationId;
                        var app = appointmentRepository.FindByLocation(Convert.ToInt32(locationId));

                }*/
        }

        private void Vouchers_OnClick(object sender, RoutedEventArgs e)
        {
            VoucherGrid.Visibility = Visibility.Visible;
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            VoucherName = SelectedVoucher.Name;
            VoucherGrid.Visibility = Visibility.Hidden;
        }
    }
}
