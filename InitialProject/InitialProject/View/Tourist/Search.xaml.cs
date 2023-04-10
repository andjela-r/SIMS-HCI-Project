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
using InitialProject.Repository;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public TourRepository _tourRepository = new TourRepository(); 
        public Search()
        {
            InitializeComponent();
        }
        private void Home_OnClick(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage();
            home.Show();
            Close();
        }
        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.Show();
            Close();
        }
        private void Requests_OnClick(object sender, RoutedEventArgs e)
        {
            Requests requests = new Requests();
            requests.Show();
            Close();
        }
        private void Vouchers_OnClick(object sender, RoutedEventArgs e)
        {
            Vouchers vouchers = new Vouchers();
            vouchers.Show();
            Close();
        }
        private void RegisteredTours_OnClick(object sender, RoutedEventArgs e)
        {
            RegisteredTours registeredTours = new RegisteredTours();
            registeredTours.Show();
            Close();
        }
        private void SentRequests_OnClick(object sender, RoutedEventArgs e)
        {
            SentRequests sentRequests = new SentRequests();
            sentRequests.Show();
            Close();
        }
        private void BookBase_OnClick(object sender, RoutedEventArgs e)
        {
            BookWindow bookWindow = new BookWindow();
            bookWindow.Show();
        }
    }
}
