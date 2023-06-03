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

namespace InitialProject.View.GuestFolder
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : Window
    {
        public User User { get; set; }

        public WelcomeView(User user)
        {
            InitializeComponent();
            this.User = user;
            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/home.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            GuestView guest = new GuestView(User);
            guest.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            guest.Show();
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            TutorialView tutorial = new TutorialView(User);
            tutorial.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            tutorial.Show();
            Close();
        }
    }
}
