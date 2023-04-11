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

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for tour.xaml
    /// </summary>
    public partial class tour : Window
    {
        public tour(User user)
        {
            InitializeComponent();
        }

        private void SelectTourComponent_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
