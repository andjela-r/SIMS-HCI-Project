using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InitialProject.View.GuestFolder
{
    /// <summary>
    /// Interaction logic for ForumListView.xaml
    /// </summary>
    public partial class ForumListView : Window
    {
        private readonly ForumRepository _forumRepository;
        private readonly LocationRepository _locationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _reservationRepository;
        public Location SelectedLocation { get; set; }
        public Forum SelectedForum { get; set; }
        public User Guest { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Forum> Forums { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public ObservableCollection<Forum> FilteredForums { get; set; }
        public string Symbol;
        public int brojac;
        Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/forum.png", UriKind.RelativeOrAbsolute);
        

        public ForumListView(User guest, Location location)
        {
            InitializeComponent();
            DataContext = this;
            _locationRepository = new LocationRepository();
            _accommodationRepository = new AccommodationRepository();
            _forumRepository = new ForumRepository();
            _reservationRepository = new AccommodationReservationRepository();
            FilteredForums = new ObservableCollection<Forum>();
            Accommodations = new ObservableCollection<Accommodation>();
            Reservations = new ObservableCollection<AccommodationReservation>(_reservationRepository.FindAll());
            Forums = new ObservableCollection<Forum>(_forumRepository.FindAll());
            this.Guest = guest;
            SelectedLocation = location;
            FilteredForums.Clear();
            StatusLabel.Visibility = Visibility.Collapsed;

            this.Icon = BitmapFrame.Create(iconUri);

            foreach (Forum forum in Forums)
            {
                if (forum.LocationIntId == location.Id)
                {
                    if (!FilteredForums.Contains(forum))
                        FilteredForums.Add(forum);
                    ForumDataGrid.ItemsSource = FilteredForums;
                }

                if (forum.WasThere)
                {
                    forum.Symbol = "★";

                }
                else
                {
                    forum.Symbol = "";
                }
                if (forum.WasThere && forum.LocationIntId == location.Id)
                    brojac++;
            }
            //BrojacLabel.Content = brojac;
            ForumDataGrid.ItemsSource = FilteredForums;

            Forum g = _forumRepository.FindByGuestId(Guest.Id);
            if (g.GuestId == Guest.Id)
                CloseButton.Visibility = Visibility.Visible;
            else
                CloseButton.Visibility = Visibility.Collapsed;

            if (brojac >= 10)
            {
                StatusLabel.Visibility = Visibility.Visible;
            }

            List<Forum> f = _forumRepository.FindByLocationId(location.Id);
            foreach (Forum forum in f)

                if (!forum.IsOpen)
                {
                    CommentButton.Background = Brushes.DarkGray;
                    CommentTextBox.IsEnabled = false;
                    CommentButton.IsEnabled = false;
                    CloseButton.IsEnabled = false;
                    OpisLabel.Content = "Sorry, forum is closed for futher comments!";
                }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                    break;

                }
                else
                {
                    Forum question2 = new Forum(question, Guest.Id, SelectedLocation.Id, false, true, 1);
                    _forumRepository.Save(question2);
                    break;
                }
            }
            MessageBox.Show("Successfuly commented 2!");

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            foreach (Forum forum in Forums)
            {
                if (forum.LocationIntId == SelectedLocation.Id)
                {
                    Forum status = _forumRepository.FindById(forum.Id);
                    status.IsOpen = false;
                    _forumRepository.Update(status);
                }
            }
            MessageBox.Show("Successfuly closed!");
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
