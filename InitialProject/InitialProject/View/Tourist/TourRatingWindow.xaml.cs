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

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourRatingWindow.xaml
    /// </summary>
    public partial class TourRatingWindow : Window
    {
        User User { get; set; }

        public TourRatingWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = user;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage(User);
            home.Show();
            Close();
        }
    }
}
