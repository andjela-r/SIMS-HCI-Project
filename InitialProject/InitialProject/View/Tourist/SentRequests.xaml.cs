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
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for SentRequests.xaml
    /// </summary>
    public partial class SentRequests : Window
    {
        public User User { get; set; }
        public TourRequestRepository tourRequestRepository { get; set; }
        public static ObservableCollection<TourRequest> TourRequests { get; set; }

        public SentRequests(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
            tourRequestRepository = new TourRequestRepository();
            TourRequests = new ObservableCollection<TourRequest>(tourRequestRepository.FindByTouristId(User.Id));
        }
        private void Home_OnClick(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage(User);
            home.Show();
            Close();
        }
        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            Search search = new Search(User);
            search.Show();
            Close();
        }
        private void Requests_OnClick(object sender, RoutedEventArgs e)
        {
            Requests requests = new Requests(User);
            requests.Show();
            Close();
        }
        private void Vouchers_OnClick(object sender, RoutedEventArgs e)
        {
            Vouchers vouchers = new Vouchers(User);
            vouchers.Show();
            Close();
        }
        private void RegisteredTours_OnClick(object sender, RoutedEventArgs e)
        {
            RegisteredTours registeredTours = new RegisteredTours(User);
            registeredTours.Show();
            Close();
        }
        private void SentRequests_OnClick(object sender, RoutedEventArgs e)
        {
            SentRequests sentRequests = new SentRequests(User);
            sentRequests.Show();
            Close();
        }
    }
}
