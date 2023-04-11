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

        private string _name;
        private string _description;
        private string _picture;
        private DateTime _date;
        private string _voucher;

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
                
            }
        }

        public BookWindow(User user, int tourId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
            this.TourId = tourId;
            _voucherRepository = new VoucherRepository();
            Vouchers = new ObservableCollection<Voucher>(_voucherRepository.FindByTouristId(user.Id));

            var tour = _tourRepository.FindById(TourId);
            TourName = tour.Name;
            TourDescription = tour.Description;
            PicturePath = tour.Pictures[0];

        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Book_OnClick(object sender, RoutedEventArgs e)
        {

            newReservation.NumberOfTourists = (int)Slider.Value;
            newReservation.TourId = TourId;
            newReservation.TouristId = this.User.Id;

            /*if (appointment.TouristIds.Count() < tour.MaxTourists)
            {
                //It's possible to make a reservation
                if (newReservation.NumberOfTourists <= seatsLeft)
                {
                    var touristId = 1;
                    var updatedAppointment = Book(newReservation, touristId);
                    seatsLeft = tour.MaxTourists - updatedAppointment.TouristsId.Count;
                    //Console.WriteLine("Successfully booked tour!\nFree seats left: {0}", seatsLeft);
                }
                else
                {
                    //Console.WriteLine("Free seats left: {0}", seatsLeft);
                    //Update number of tourists
                    //Console.WriteLine("Would you like to change the number of tourists? (y/n) ");
                    //var answer = Console.ReadLine();
                    // switch (answer)
                    //{
                    //  case "y":
                    //Console.WriteLine("Enter new number of tourists: ");
                    //Console.WriteLine("Enter '0' to return");
                    var newTouristNumber = -1;
                    while (newTouristNumber == -1 || newTouristNumber > tour.MaxTourists)
                    {
                        newTouristNumber = Convert.ToInt32(Console.ReadLine());
                        if (newTouristNumber == 0)
                            return;
                    }

                    newReservation.NumberOfTourists = newTouristNumber;
                         //  break;
                        case "n":
                            return;
                    default:
                            //Console.WriteLine("Option does not exist");
                            break;
                }

                //var updatedAppointment = Book(newReservation);
                //seatsLeft = tour.MaxTourists - updatedAppointment.TouristsId.Count();
                //Console.WriteLine("Successfully booked tour!\nFree seats left: {0}", seatsLeft);
            }
        }
            else
            {
                //Console.WriteLine(
                    //"Unfortunately, the tour you've chosen doesn't have any seats left.\nWould you like to pick another tour? (y/n) ");
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "y":
                        //Console.WriteLine("Tours on the same location: ");
                        var locationId = tour.LocationId;
        var app = appointmentRepository.FindByLocation(Convert.ToInt32(locationId));
                        //Program.PrintAppointments(app);

                        break;
                    case "n":
                        return;
                    default:
                        //Console.WriteLine("Option does not exist");
                        break;
                }*/


            _tourReservationService.Book(newReservation);
            _voucherRepository.Delete(SelectedVoucher);
            Close();
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
