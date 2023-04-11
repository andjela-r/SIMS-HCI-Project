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
using InitialProject.View.Tourist.Components;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public User User { get; set; }

        private void dataGridAccommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private TourRepository _tourRepository = new TourRepository();
        private string _tour;

        public string Tour
        {
            get
            {
                return _tour;
            }
            set
            {
                if (value != _tour)
                {
                    _tour = value;
                }
            }
        }

        /*public void LoadTours()
        {
            var tours = _tourRepository.FindAll();
            for (int i = 0; i < tours.Count; i++)
            {
                TourComponent component = AddTourComponent(i);
            }

        }*/

        /*public TourComponent AddTourComponent(int i)
        {
            var tours = _tourRepository.FindAll();
            TourComponent component = new TourComponent();
            component.Title = tours[i].Name;
            component.Description = tours[i].Description;
            component.ImagePath = tours[i].Pictures[0];

            return component;
        }*/


        public Search(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
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
