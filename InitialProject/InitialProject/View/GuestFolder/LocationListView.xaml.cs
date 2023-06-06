using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View.Guest;
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

namespace InitialProject.View.GuestFolder
{
    /// <summary>
    /// Interaction logic for LocationListView.xaml
    /// </summary>
    public partial class LocationListView : Window
    {
        private readonly LocationRepository _locationRepository;
        private readonly ForumRepository _forumRepository;
        public Location SelectedLocation { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Forum> Forums { get; set; }
        public ObservableCollection<Location> FilteredLocations { get; set; }
        public User Guest { get; set; }

        public LocationListView(User user)
        {
            InitializeComponent();
            DataContext = this;
            _locationRepository = new LocationRepository();
            _forumRepository = new ForumRepository();
            Locations = new ObservableCollection<Location>(_locationRepository.FindAll());
            Forums = new ObservableCollection<Forum>(_forumRepository.FindAll());
            FilteredLocations = new ObservableCollection<Location>();
            this.Guest = user;

            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/location.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            FilteredLocations.Clear();
            foreach (Location location in Locations)
            {
                if (!FilteredLocations.Contains(location))
                    FilteredLocations.Add(location);
                LocationsDataGrid.ItemsSource = FilteredLocations;
            }
            LocationsDataGrid.ItemsSource = FilteredLocations;

        }

        private void MyDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach(Forum forum in Forums) {

                if (SelectedLocation.Id == forum.LocationIntId)
                {
                    //otvori forum koji vec postoji
                }
                else
                {
                    ForumOpenView forumOpen = new ForumOpenView();
                    forumOpen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    forumOpen.Show();
                }
                   
            }
        }
    }
}
