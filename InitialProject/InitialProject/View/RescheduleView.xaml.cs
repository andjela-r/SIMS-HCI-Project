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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RescheduleView.xaml
    /// </summary>
    public partial class RescheduleView : Window
    {
        private readonly AccommodationReservationRepository _reservationRepository;
        private readonly OwnerAndAccommodationRatingRepository _ratingRepository;
        private readonly RequestStatusRepository _requestStatus;
        public AccommodationReservation Reservation { get; set; }

        public RescheduleView(AccommodationReservation reservation)
        {
            InitializeComponent();
            _reservationRepository = new AccommodationReservationRepository();
            _ratingRepository = new OwnerAndAccommodationRatingRepository();
            _requestStatus = new RequestStatusRepository();
            Reservation = reservation;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)StartDatePicker.SelectedDate;
            DateTime endDate = (DateTime)EndDatePicker.SelectedDate;

            if (endDate < startDate)
            {
                MessageBox.Show("Start date can not be smaller than end date. Try again!");
                return;
            }
            RequestStatus request = new RequestStatus(startDate, endDate, "", RequestStatusEnum.Waiting, Reservation.Id);
            _requestStatus.Save(request); 
            MessageBox.Show("Successfuly requested! Now wait for the response.");
            Close();

        }
    }
}
