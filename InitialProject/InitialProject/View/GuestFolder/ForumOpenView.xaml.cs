using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InitialProject.View.Guest
{
    /// <summary>
    /// Interaction logic for ForumOpenView.xaml
    /// </summary>
    public partial class ForumOpenView : Window
    {
        private readonly ForumRepository _forumRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _reservationRepository;
        public Location SelectedLocation { get; set; }
        public Forum SelectedForum { get; set; }
        public User Guest { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Forum> Forums { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public ForumOpenView(User guest, Location location)
        {
            InitializeComponent();
            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/forum.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            CommLabel.IsEnabled = false;
            CommentTextBox.IsEnabled = false;
            OpenButton.IsEnabled = false;

            _accommodationRepository = new AccommodationRepository();
            _forumRepository = new ForumRepository();
            _reservationRepository = new AccommodationReservationRepository();
            Accommodations = new ObservableCollection<Accommodation>();
            Reservations = new ObservableCollection<AccommodationReservation>(_reservationRepository.FindAll());
            Forums = new ObservableCollection<Forum>(_forumRepository.FindAll());
            this.Guest = guest;
            SelectedLocation = location;
            OpenButton.Background = Brushes.DarkGray;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string question = CommentTextBox.Text;
            List<Accommodation> acc = _accommodationRepository.FindByLocation(SelectedLocation.Id);
            List<AccommodationReservation> resv1 = _reservationRepository.FindAll();
            List<AccommodationReservation> resv = _reservationRepository.GetMatchingReservations(resv1, acc);

            foreach (AccommodationReservation reservaton in resv)
            {
                if (Guest.Id == reservaton.GuestId)
                {
                    Forum question1 = new Forum(question, Guest.Id, SelectedLocation.Id, true, true, 1);
                    _forumRepository.Save(question1);
                    MessageBox.Show("Successfuly commented 1!");
                    break;

                }
                else
                {
                    Forum question2 = new Forum(question, Guest.Id, SelectedLocation.Id, false, true, 1);
                    _forumRepository.Save(question2);
                    MessageBox.Show("Successfuly commented 2!");
                    break;
                }
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            OpenButton.Background = Brushes.Gainsboro;
            CommLabel.IsEnabled = true;
            CommentTextBox.IsEnabled = true;
            OpenButton.IsEnabled = true;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
