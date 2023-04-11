using InitialProject.Model;
using InitialProject.Repository;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RatingView.xaml
    /// </summary>
    public partial class RatingView : Window
    {   
        private AccommodationReservationRepository _reservationRepository;
        private OwnerAndAccommodationRatingRepository _ratingRepository;
        private readonly AccommodationRepository _accomodationRepository;
        public User Guest { get; set; }
        public AccommodationReservation Reservation { get; set; }

        public RatingView(AccommodationReservation reservation, User guest)
        {
            InitializeComponent();
            DataContext = this;
            this.Guest = guest;
            _accomodationRepository = new AccommodationRepository();
            _reservationRepository = new AccommodationReservationRepository();
            _ratingRepository = new OwnerAndAccommodationRatingRepository();
            Reservation = reservation;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int cleanliness = int.Parse(CleanButton.Text);
            int communication = int.Parse(CommunicationButton.Text);
            string comment = AdditionalCommentButton.Text;

            if(cleanliness > 5 || cleanliness < 1 || communication > 5 || communication < 1) 
            {
                CleanButton.Clear();
                CommunicationButton.Clear();
                AdditionalCommentButton.Clear();
                MessageBox.Show("Ratings must be between 1 and 5");
                return;
            }
            var accommodation = _accomodationRepository.FindById(Reservation.AccommodationId);
            OwnerAndAccommodationRating rating = new OwnerAndAccommodationRating(cleanliness, communication, comment, accommodation.OwnerId, Reservation.AccommodationId);
            _ratingRepository.Save(rating);
            MessageBox.Show("Successfuly rated!");
            Close();

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
