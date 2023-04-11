using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View.Tourist;
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
    /// Interaction logic for GuestRatings.xaml
    /// </summary>
    public partial class GuestRatings : Window
    {

        private readonly GuestRatingRepository _guestRatingRepository;
        private readonly GuestRepository _guestRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        public User User { get; set; }
        public ObservableCollection<AccommodationReservation> Guests { get; }

        private int _guestId;
        public int GuestId
        {
            get => _guestId;
            set
            {
                if (value != _guestId)
                {
                    _guestId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _cleanliness;
        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _obedience;
        public int Obedience
        {
            get => _obedience;
            set
            {
                if (value != _obedience)
                {
                    _obedience = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestRatings(User owner)
        {
            InitializeComponent();
            this.DataContext = this;
            _guestRatingRepository = new GuestRatingRepository();
            _guestRepository=new GuestRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            this.User = owner;
            Guests = new ObservableCollection<AccommodationReservation>(_accommodationReservationRepository.FindAll());
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

        private void GuestRating_OnClick(object sender, RoutedEventArgs e)
        {
            User guest = _guestRepository.FindById(_guestId);
            if (guest == null)
            {
                MessageBox.Show("That guest does not exist!");
            } else if (_cleanliness<1 || _cleanliness > 5 || Obedience<1 || Obedience>5)
                {
                    MessageBox.Show("Rate it on a scale from 1 to 5!");
                } else {
                GuestRating guestRating = new GuestRating();
                GuestRatingService guestRatingService= new GuestRatingService();
                guestRating.GuestId=Convert.ToInt32(_guestId);
                guestRating.Cleanliness = Convert.ToInt32(_cleanliness);
                guestRating.Obedience = Convert.ToInt32(_obedience);
                guestRating.Comment = Comment;
                guestRatingService.CreateGuestRating(guestRating);
                MessageBox.Show("Successfully rated a guest!");
            }
            

        }
    }
}
