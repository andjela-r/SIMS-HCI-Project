using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationView.xaml
    /// </summary>
    public partial class AccommodationReservationView : Window
    {
        private readonly AccommodationReservationRepository _reservationRepository;
        private readonly AccommodationReservationService _reservationService;
        private readonly GuestRepository _guestRepository;
        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public static ObservableCollection<Model.Guest> Guests { get; set; }
        public ObservableCollection<Tuple<DateTime, DateTime>> AvailableDatesPair { get; set; }
        public Tuple<DateTime, DateTime> SelectedAvailableDatePair { get; set; }
        public User Guest { get; set; }

        private DateTime _checkInDate;
        private DateTime _checkOutDate;

        int brojac;

        public AccommodationReservationView(Accommodation selectedAccommodation, User user)
        {
            InitializeComponent();
            DataContext = this;

            _reservationRepository = new AccommodationReservationRepository();
            _reservationService = new AccommodationReservationService();
            _guestRepository = new GuestRepository();
            this.Guest = user;
            Reservations = new ObservableCollection<AccommodationReservation>(_reservationRepository.FindAll());
            Guests = new ObservableCollection<Model.Guest>(_guestRepository.FindAll());
            SelectedAccommodation = selectedAccommodation;
            StartDatePicker.DisplayDateStart = DateTime.Today;
            EndDatePicker.DisplayDateStart = DateTime.Today;
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);
            AvailableDatesPair = new ObservableCollection<Tuple<DateTime, DateTime>>();

            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/home.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            DatesDataGrid.Visibility = Visibility.Collapsed;
            ReserveButton.Visibility = Visibility.Collapsed;
            NumGuestLabel.Visibility = Visibility.Collapsed;
            GuestNumberBox.Visibility = Visibility.Collapsed;

            foreach (AccommodationReservation reservation in Reservations)
            {
                if (reservation.GuestId == user.Id && reservation.EndDate >= oneYearAgo)
                    brojac++;
            }
            PointsLabel.Visibility = Visibility.Collapsed;
            foreach (Model.Guest guest in Guests)
            {
                if (guest.UserId == user.Id && brojac >= 10)
                {
                    if (guest.IsSuperGuest)
                    {
                        PointsLabel.Visibility = Visibility.Visible;
                        PointsLabel.Content = "You have:" + guest.BonusPoints + "point(s)";
                        return;
                    }
                    else
                    {
                        PointsLabel.Visibility = Visibility.Visible;
                        Model.Guest super = _guestRepository.FindByUserId(guest.UserId);
                        super.IsSuperGuest = true;
                        super.BonusPoints = 5;
                        _guestRepository.Update(super);
                        PointsLabel.Content = "You have:" + super.BonusPoints + "point(s)";
                    }
                }
            }
        }

        public DateTime CheckInDate
        {
            get => _checkInDate;
            set 
            { 
                if(value != _checkInDate)
                {
                    _checkInDate = value;
                    OnPropertyChanged();
                } 
            }
        }

        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                if (value != _checkOutDate)
                {
                    _checkOutDate = value;
                    OnPropertyChanged();
                }
            }
        }           

        public string this[string columnName] => throw new NotImplementedException();
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<DateTime> FindOccupiedDates()
        {
            List<DateTime> reservedDates = new List<DateTime>();
            foreach (AccommodationReservation reservation in Reservations) 
            {
                if(SelectedAccommodation.Id == reservation.AccommodationId)
                {
                    DateTime startDate = reservation.StartDate;
                    DateTime endDate = reservation.EndDate;
                    for (DateTime dates = startDate; dates <= endDate; dates = dates.AddDays(1))
                        reservedDates.Add(dates);
                }
            }
            return reservedDates;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)StartDatePicker.SelectedDate;
            DateTime endDate = (DateTime)EndDatePicker.SelectedDate;
            int stayLength = int.Parse(StayLengthBox.Text);

            if (endDate < startDate)
            {
                AvailableDatesPair.Clear();
                MessageBox.Show("Start date can not be smaller than end date. Try again!");
                return;
            }
            if (stayLength < SelectedAccommodation.MinStay)
            {
                AvailableDatesPair.Clear();
                MessageBox.Show($"Minimum days for reservation: {SelectedAccommodation.MinStay}");
                return;
            }
            DatesDataGrid.Visibility = Visibility.Visible;
            ReserveButton.Visibility = Visibility.Visible;
            NumGuestLabel.Visibility = Visibility.Visible;
            GuestNumberBox.Visibility = Visibility.Visible;
            HideButton.Visibility = Visibility.Visible;
            ContinueButton.Visibility = Visibility.Collapsed;
            GoBackButton.Visibility = Visibility.Collapsed;

            List<DateTime> reservatedDates = FindOccupiedDates();
            List<DateTime> availableDates = new List<DateTime>();
            AvailableDatesPair.Clear();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (!reservatedDates.Contains(date))
                {
                    availableDates.Add(date);
                }
                else
                {
                    availableDates.Clear();
                }
                if (availableDates.Count == stayLength)
                {
         
                        AvailableDatesPair.Add(Tuple.Create(availableDates[0].Date, availableDates[availableDates.Count - 1].Date));
                    availableDates.RemoveAt(0);
                }
            }
            availableDates.Clear();
            DateTime reccommendedStartDate = startDate;
            DateTime reccommendedEndDate = endDate;

            if (AvailableDatesPair.Count == 0)
            {
                MessageBox.Show("All dates in given rangde are taken. We can reccommend you this dates.");
                while (!(AvailableDatesPair.Count >= 5))
                {
                    reccommendedEndDate = reccommendedEndDate.AddDays(1);
                    reccommendedStartDate = reccommendedStartDate.Equals(DateTime.Today) ? reccommendedStartDate : reccommendedStartDate.AddDays(-1);

                    availableDates.Clear();
                    for (DateTime date = reccommendedStartDate; date <= reccommendedEndDate; date = date.AddDays(1))
                    {
                        if (!reservatedDates.Contains(date))
                            availableDates.Add(date);
                        else 
                            availableDates.Clear();
                        if (availableDates.Count == stayLength)
                        {
                            if (!AvailableDatesPair.Contains(Tuple.Create(availableDates[0].Date, availableDates[availableDates.Count - 1].Date)))
                                AvailableDatesPair.Add(Tuple.Create(availableDates[0].Date, availableDates[availableDates.Count - 1].Date));
                            availableDates.RemoveAt(0);

                        }
                    }
                }
            }
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAvailableDatePair != null)
            {
                if (int.Parse(GuestNumberBox.Text) > SelectedAccommodation.MaxGuests)
                {
                    MessageBox.Show($"Maximum number of guests for this accommodation: {SelectedAccommodation.MaxGuests} ");
                    return;
                }
                if (int.Parse(GuestNumberBox.Text) < 1)
                {
                    MessageBox.Show("Number of guests must be greater than 1! ");
                    return;
                }
                CheckInDate = SelectedAvailableDatePair.Item1;
                CheckOutDate = SelectedAvailableDatePair.Item2;

                foreach (Model.Guest guest in Guests)
                {
                    if (guest.UserId == Guest.Id && guest.IsSuperGuest)
                    {
                        int bonusPoints = guest.BonusPoints - 1;
                        Model.Guest points = _guestRepository.FindByUserId(guest.UserId);
                        points.BonusPoints = bonusPoints;
                        _guestRepository.Update(points);
                        AccommodationReservation reservation = new AccommodationReservation(SelectedAccommodation.Id, Guest.Id, CheckInDate, CheckOutDate, int.Parse(StayLengthBox.Text), int.Parse(GuestNumberBox.Text));
                        _reservationRepository.Save(reservation);
                        MessageBox.Show("Successfuly reserved! Now,you have: " + bonusPoints + " points");
                        Close();
                    }
                }

            }
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            DatesDataGrid.Visibility = Visibility.Collapsed;
            ReserveButton.Visibility = Visibility.Collapsed;
            NumGuestLabel.Visibility = Visibility.Collapsed;
            GuestNumberBox.Visibility = Visibility.Collapsed;
            HideButton.Visibility = Visibility.Collapsed;
            ContinueButton.Visibility = Visibility.Visible;
            GoBackButton.Visibility = Visibility.Visible;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
