using InitialProject.Model;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuestView.xaml
    /// </summary>
    public partial class GuestView : Window
    {
        public User User { get; set; }

        public GuestView(User guest)
        {
            InitializeComponent();
            this.User = guest;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerAndAccommodationRatingView rating = new OwnerAndAccommodationRatingView(User);
            rating.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AccommodationView accView = new AccommodationView(User);
            accView.Show();
            Close();
        }
    }
}
