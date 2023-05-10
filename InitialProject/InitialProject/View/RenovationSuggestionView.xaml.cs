using InitialProject.Model;
using InitialProject.Repository;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RenovationSuggestionView.xaml
    /// </summary>
    public partial class RenovationSuggestionView : Window
    {
        private RenovationSuggestionRepository _renovationRepository;
        public AccommodationReservation Reservation;

        public RenovationSuggestionView(AccommodationReservation reservation)
        {
            InitializeComponent();
            DataContext = this;
            Reservation = reservation;
            _renovationRepository = new RenovationSuggestionRepository();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("--Level 1 - it would be nice to renovate some small things, \r\n but everything works as it should without it \r\n--Level 2 - small gripes with the accommodation that \r\nif removed would make it perfect\r\n--Level 3 - a few things that really bothered me should be\r\n renovated\r\n--Level 4 - there are a lot of bad things and renovation is \r\nreally necessary\r\n--Level 5 - the accommodation is in a very bad condition \r\nand is not worth renting at all unless it is renovated");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string suggestion = SuggestionTextBox.Text;
            int level = int.Parse(LevelTextBox.Text);
           // RenovationSuggestion renovation1 = new RenovationSuggestion(1, 1, "fevf");
           // _renovationRepository.Save(renovation1);
            MessageBox.Show("Successfuly rated!");
            Close();

        }
    }
}
