using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for Reviews_and_forums.xaml
    /// </summary>
    public partial class ReviewsAndForums : Window
    {
        public User User { get; set; }
        public OwnerAndAccommodationRating SelectedOwnerAndAccommodationRating { get; set; }

        private readonly OwnerAndAccommodationRatingRepository _ownerAndAccommodationRatingRepository;

        public static ObservableCollection<OwnerAndAccommodationRating> OwnerAndAccommodationRatings { get; set; }

        float AverageRating { get; set; }

        public ReviewsAndForums(User owner)
        {
            InitializeComponent();
            this.DataContext = this;
            this.User = owner;
            _ownerAndAccommodationRatingRepository = new OwnerAndAccommodationRatingRepository();
            OwnerAndAccommodationRatings = new ObservableCollection<OwnerAndAccommodationRating>(_ownerAndAccommodationRatingRepository.FindByOwnerId(owner.Id));
        }

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void HomePage_OnClick(object sender, RoutedEventArgs e)
        {
            OwnerHomePage ownerHomePage = new OwnerHomePage(User);
            ownerHomePage.Show();
            Close();

        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void AverageRating_OnClick(object sender, RoutedEventArgs e)
        {
            float SumCleanliness = 0;
            float SumHospitality = 0;
            float AverageCleanliness = 0;
            float AverageHospitality = 0;
           
            for (int i = 0; i < OwnerAndAccommodationRatings.Count; i++)
            {
                SumCleanliness = SumCleanliness + OwnerAndAccommodationRatings[i].Cleanliness;
                SumHospitality = SumHospitality + OwnerAndAccommodationRatings[i].OwnerHospitality;
            }
            AverageCleanliness = SumCleanliness / OwnerAndAccommodationRatings.Count;
            AverageHospitality = SumHospitality / OwnerAndAccommodationRatings.Count;
            AverageRating = (AverageCleanliness + AverageHospitality) / 2;
            MessageBox.Show(AverageRating.ToString());

        }
        private void Title_OnClick(object sender, RoutedEventArgs e)
        {
            string Status;
            if (AverageRating >= 4.5)
            {
                Status = "Super-owner";
            }
            else Status = "Owner";
            MessageBox.Show(Status);
        }

    }
}
