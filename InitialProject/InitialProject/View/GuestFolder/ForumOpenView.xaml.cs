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

namespace InitialProject.View.Guest
{
    /// <summary>
    /// Interaction logic for ForumOpenView.xaml
    /// </summary>
    public partial class ForumOpenView : Window
    {
        public ForumOpenView()
        {
            InitializeComponent();
            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/forum.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
