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
    /// Interaction logic for TutorialView.xaml
    /// </summary>
    public partial class TutorialView : Window
    {
        public User User { get; set; }

        public TutorialView(User user)
        {
            InitializeComponent();
            this.User = user;
            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/videoTutorial.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GuestView guest = new GuestView(User);
            guest.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            guest.Show();
            Close();
        }
    }
}
