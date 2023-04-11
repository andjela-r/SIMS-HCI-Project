using System;
using System.Windows;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for BookingRequests.xaml
    /// </summary>
    public partial class BookingRequests : Window
    {
        public BookingRequests()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void HomePage_OnClick(object sender, RoutedEventArgs e)
        {
            OwnerHomePage ownerHomePage = new OwnerHomePage();
            ownerHomePage.Show();
            Close();

        }







    }
}
