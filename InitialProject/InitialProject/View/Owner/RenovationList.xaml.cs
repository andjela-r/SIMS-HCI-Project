using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for RenovationList.xaml
    /// </summary>
    public partial class RenovationList : Window
    {

        private readonly RenovationRepository _renovationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        public static ObservableCollection<Renovation> Renovations { get; set; }

        public static ObservableCollection<Renovation> AllRenovations { get; set; }

        public static ObservableCollection<Renovation> RenovationsFinal { get; set; }
        public static ObservableCollection<Accommodation> YourAccommodations { get; set; }
        public User User { get; set; }

        public RenovationList(User owner)
        {
            InitializeComponent();
            this.DataContext = this;
            _renovationRepository = new RenovationRepository();
            _accommodationRepository = new AccommodationRepository();
            this.User = owner;
            Renovations = new ObservableCollection<Renovation>();
            AllRenovations = new ObservableCollection<Renovation>(_renovationRepository.FindAll());  //ovo su sva renoviranja
            YourAccommodations = new ObservableCollection<Accommodation>(_accommodationRepository.FindByOwnerId(owner.Id)); //ovo je lista svih apartmana od Tijane
            for (int i = 0; i < YourAccommodations.Count; i++)
            {
                for (int j = 0; j < AllRenovations.Count; j++)
                {
                    if (AllRenovations[j].AccommodationId==YourAccommodations[i].Id)
                    {
                        Renovations.Add(AllRenovations[j]);
                    }

                }

            }
            RenovationsFinal = new ObservableCollection<Renovation>(Renovations);
        }

        private int _renovationId;
        public int RenovationId
        {
            get => _renovationId;
            set
            {
                if (value != _renovationId)
                {
                    _renovationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }


        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }


        private int _expectedDuration;
        public int ExpectedDuration
        {
            get => _expectedDuration;
            set
            {
                if (value != _expectedDuration)
                {
                    _expectedDuration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _accommodationId;     //ovo je za Renovation klasu
        public int AccommodationId
        {
            get => _accommodationId;
            set
            {
                if (value != _accommodationId)
                {
                    _accommodationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _cancelingId;
        public int CancelingId
        {
            get => _cancelingId;
            set
            {
                if (value != _cancelingId)
                {
                    _cancelingId = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private void RenovationSchedule_Click(object sender, RoutedEventArgs e)
        {
            RenovationSchedule renovationSchedule = new RenovationSchedule(User);
            renovationSchedule.Show();
            Close();
        }

        private void RenovationCancel_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < RenovationsFinal.Count; i++)
            {
                if (RenovationsFinal[i].Id == _cancelingId)
                {
                    var today = DateTime.Today;
                    var startDate = RenovationsFinal[i].StartDate;
                    var difference=startDate - today;
                    if (difference.Days > 5)
                    {
                        Renovation renovationCancel = RenovationsFinal[i];
                        _renovationRepository.Delete(renovationCancel);
                        MessageBox.Show("Successfully canceled a renovation!");
                    } else {
                        MessageBox.Show("Less than 5 days until the start of the renovation!"); 
                    }
                } else {
                    MessageBox.Show("That renovation does not exist!");
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
