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
using InitialProject.Service;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for MyProfile.xaml
    /// </summary>
    public partial class MyProfile : Window
    {
        public User User { get; set; }

        private readonly GuideRepository _guideRepository;
        private GuideService _guideService = new GuideService();
        public MyProfile(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;

            _guideRepository = new GuideRepository();
            usernameLabel.Content = "Hello, \n" + user.Username + "!";
            nameLabel.Content = user.Username;
            if (_guideService.CheckSuperGuideCriteria(user.Id))
            {
                superGuideLabel.Content = "-Super Guide-";
            }
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

        private void GiveResignation_OnClick(Object sender, RoutedEventArgs e)
        {
            _guideService.GiveResignation(User.Id);

            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void Check_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
