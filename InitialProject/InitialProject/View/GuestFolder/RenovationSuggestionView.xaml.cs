using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RenovationSuggestionView.xaml
    /// </summary>
    public partial class RenovationSuggestionView : Window
    {
        private readonly RenovationSuggestionRepository _renovationSuggestionRepository;
        public AccommodationReservation Reservation { get; set; }
        public static ObservableCollection<RenovationSuggestion> Renovations { get; set; }

        public RenovationSuggestionView(AccommodationReservation reservation)
        {
            InitializeComponent();
            DataContext = this;
            _renovationSuggestionRepository = new RenovationSuggestionRepository();
            this.Reservation = reservation;
            Renovations = new ObservableCollection<RenovationSuggestion>(_renovationSuggestionRepository.FindAll());

            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/rating.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("--Level 1 - it would be nice to renovate some small things, \r\n but everything works as it should without it \r\n--Level 2 - small gripes with the accommodation that \r\nif removed would make it perfect\r\n--Level 3 - a few things that really bothered me should be\r\n renovated\r\n--Level 4 - there are a lot of bad things and renovation is \r\nreally necessary\r\n--Level 5 - the accommodation is in a very bad condition \r\nand is not worth renting at all unless it is renovated");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string suggestion = SuggestionTextBox.Text;
            int level = int.Parse(LevelTextBox.Text);
            if (level < 5 && level > 1)
            {
                RenovationSuggestion renovation1 = new RenovationSuggestion(Reservation.Id, level, suggestion);
                _renovationSuggestionRepository.Save(renovation1);
                MessageBox.Show("Successfuly rated!");
                Close();
            }
            else
            {
                MessageBox.Show("The level must be number between 1 and 5!");
                return;
            }
        }
    }
}
