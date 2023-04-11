using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.View.Guide;
using InitialProject.View.Tourist;
using InitialProject.View.Owner;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    switch (user.Role)
                    {
                        case Role.Tourist:
                            HomePage homePage = new HomePage(user);
                            homePage.Show();
                            Close();
                            break;
                        case Role.Guest:
                            GuestView guest = new GuestView(user);
                            guest.Show();
                            Close();
                            break;
                        case Role.Guide:
                            tour tour = new tour(user);
                            tour.Show();
                            Close();
                            break;
                        case Role.Owner:
                            OwnerHomePage ownerHomePage = new OwnerHomePage(user);
                            ownerHomePage.Show();
                            Close();
                            break;
                    }
;
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}
