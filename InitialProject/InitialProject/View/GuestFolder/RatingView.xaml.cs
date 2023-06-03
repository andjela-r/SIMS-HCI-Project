using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RatingView.xaml
    /// </summary>
    public partial class RatingView : Window, INotifyPropertyChanged
    {   
        private AccommodationReservationRepository _reservationRepository;
        private OwnerAndAccommodationRatingRepository _ratingRepository;
        private readonly AccommodationRepository _accomodationRepository;
        public User Guest { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public RatingView(AccommodationReservation reservation, User guest)
        {
            InitializeComponent();
            DataContext = this;
            this.Guest = guest;
            _accomodationRepository = new AccommodationRepository();
            _reservationRepository = new AccommodationReservationRepository();
            _ratingRepository = new OwnerAndAccommodationRatingRepository();
            Reservation = reservation;
            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/rating.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private int _test2;
        public int Test2
        {
            get
            {
                return _test2;
            }
            set
            {
                if (value != _test2)
                {
                    _test2 = value;
                    OnPropertyChanged("Test2");
                }
            }
        }

        private int _test1;
        public int Test1
        {
            get
            {
                return _test1;
            }
            set
            {
                if (value != _test1)
                {
                    _test1 = value;
                    OnPropertyChanged("Test1");
                }
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int cleanliness = int.Parse(CleanButton.Text);
            int communication = int.Parse(CommunicationButton.Text);
            string comment = AdditionalCommentButton.Text;
            string pictures = PictureButton.Text;
            char[] delimiters = { ',', ';' };
            string[] stringArray = pictures.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            List<string> stringList = new List<string>(stringArray);

            if (cleanliness > 5 || cleanliness < 1 || communication > 5 || communication < 1) 
            {
                CleanButton.Clear();
                CommunicationButton.Clear();
                AdditionalCommentButton.Clear();
                MessageBox.Show("Ratings must be between 1 and 5");
                return;
            }
            var accommodation = _accomodationRepository.FindById(Reservation.AccommodationId);
            OwnerAndAccommodationRating rating = new OwnerAndAccommodationRating(cleanliness, communication, comment, accommodation.OwnerId, Reservation.AccommodationId, stringList);
            _ratingRepository.Save(rating);
            AccommodationReservation status = _reservationRepository.FindById(Reservation.Id);
            status.IsRated = true;
            _reservationRepository.Update(status);
            MessageBox.Show("Successfuly rated!");
            Close();

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RenovationSuggestionView renovationSuggestion = new RenovationSuggestionView(Reservation);
            renovationSuggestion.Show();
        }
    }
}
