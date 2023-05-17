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
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for ToursView.xaml
    /// </summary>
    public partial class ToursView : Window
    {
        public User User { get; set; }
        private string _tour;
        public TourRepository _tourRepository = new TourRepository();

        public string TourName
        {
            get => _tour;
            set
            {
                if (value != _tour) _tour = value;
            }
        }

        public string ImagePath
        {
            get => _tour;
            set
            {
                if (value != _tour) _tour = value;
            }
        }

        public ToursView(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
            User = user;
            usernameLabel.Content = "Hello, \n" + user.Username + "!";

            var turica = _tourRepository.FindById(1);
            TourName = turica.Name;
            ImagePath = turica.Pictures[0];
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void CreateTour_OnClick(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour(User);
            createTour.Show();
            Close();
        }

        private void Statistics_OnClick(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics(User);
            statistics.Top = this.Top;
            statistics.Show();
            Close();
        }

        private void Requests_OnClick(object sender, RoutedEventArgs e)
        {
            Requests requests = new Requests(User);
            requests.Show();
            Close();
        }

        private void MyProfile_OnClick(object sender, RoutedEventArgs e)
        {
            MyProfile myProfile = new MyProfile(User);
            myProfile.Show();
            Close();
        }

        private void Tours_OnClick(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new ToursView(User);
            toursView.Show();
            Close();
        }
    }
}
