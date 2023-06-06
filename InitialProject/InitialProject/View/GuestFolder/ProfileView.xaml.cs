using InitialProject.Model;
using InitialProject.Repository;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        public User User { get; set; }
        public ObservableCollection<AccommodationReservation> FilteredAccommodations { get; set; }
        private readonly AccommodationReservationRepository _reservationRepository;
        private readonly GuestRepository _guestRepository;
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public static ObservableCollection<Model.Guest> Guests { get; set; }
        public ObservableCollection<GuestRating> FilteredRatings { get; set; }
        private readonly GuestRatingRepository _ratingRepository;
        public static ObservableCollection<GuestRating> Rating { get; set; }
        int brojac;

        public ProfileView(User user)
        {
            InitializeComponent();
            this.User = user;
            FilteredAccommodations = new ObservableCollection<AccommodationReservation>();
            FilteredAccommodations.Clear();
            Uri iconUri = new Uri("C:/Users/Dell/Desktop/projekatSims/SIMS-HCI-Project/InitialProject/InitialProject/Resources/Images/profile.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            _reservationRepository = new AccommodationReservationRepository();
            _guestRepository = new GuestRepository();
            Reservations = new ObservableCollection<AccommodationReservation>(_reservationRepository.FindAll());
            NameLabel.Content = user.Username;
            Image.Visibility = Visibility.Collapsed;
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);
            Guests = new ObservableCollection<Model.Guest>(_guestRepository.FindAll());
            _ratingRepository = new GuestRatingRepository();
            Rating = new ObservableCollection<GuestRating>(_ratingRepository.FindAll());
            FilteredRatings = new ObservableCollection<GuestRating>();
            //
            CartesianChart chart = new CartesianChart();
            chart.Width = 200;
            chart.Height = 200;
            List<int> id = new List<int>();
            List<int> ocenePonasanja = new List<int>();

            ChartValues<int> oceneXValues = new ChartValues<int>(id);
            ChartValues<int> oceneYValues = new ChartValues<int>(ocenePonasanja);

            foreach (GuestRating rating in Rating)
            {
                if (rating.GuestId == user.Id)
                {
                    id.Add(rating.Id);
                    ocenePonasanja.Add(rating.Obedience);
                }
     
            }
            for (int i = 0; i < id.Count; i++)
            {
                oceneXValues.Add(id[i]);
                oceneYValues.Add(ocenePonasanja[i]);
            }
            ColumnSeries scatterSeries = new ColumnSeries
            {
                Title = "Ocene",
                Fill = Brushes.Green,
                Values = new ChartValues<ObservablePoint>()
            };
            for (int i = 0; i < id.Count; i++)
            {
                scatterSeries.Values.Add(new ObservablePoint(oceneXValues[i], oceneYValues[i]));
            }
            chart.AxisX.Add(new Axis
            {
                Title = "Rezervacija",
                Foreground = Brushes.Black,
                Labels = new string[] { }
        });
            chart.AxisY.Add(new Axis
            {
                MinValue = 1,
                MaxValue = 5,
                Title = "Ocena za kulturnost",
                Foreground = Brushes.Black
            });

            chart.FontSize = 12;
            chart.Series.Add(scatterSeries);
            chart.BorderBrush = Brushes.Black;
            Ime1.Children.Add(chart);
            Grid.SetRow(chart, 1);    
            Grid.SetColumn(chart, 2);
            //
            CartesianChart chart1 = new CartesianChart();
            chart1.Width = 200;
            chart1.Height = 200;
            List<int> id1 = new List<int>();
            List<int> oceneCistoce = new List<int>();

            ChartValues<int> oceneXValues1 = new ChartValues<int>(id1);
            ChartValues<int> oceneYValues1 = new ChartValues<int>(oceneCistoce);

            foreach (GuestRating rating in Rating)
            {
                if (rating.GuestId == user.Id)
                {
                    oceneCistoce.Add(rating.Cleanliness);
                    id1.Add(rating.Id);
                }

            }
            for (int i = 0; i < oceneCistoce.Count; i++)
            {
                oceneXValues1.Add(id1[i]);
                oceneYValues1.Add(oceneCistoce[i]);
            }
            ColumnSeries scatterSeries1 = new ColumnSeries
            {
                Title = "Ocene",
                Fill = Brushes.Purple,
                Values = new ChartValues<ObservablePoint>()
            };
            for (int i = 0; i < oceneCistoce.Count; i++)
            {
                scatterSeries1.Values.Add(new ObservablePoint(oceneXValues1[i], oceneYValues1[i]));
            }
            chart1.AxisX.Add(new Axis
            {
                Labels = new string[] { },
                Title = "Rezervacija",
                Foreground = Brushes.Black
            });
            chart1.AxisY.Add(new Axis
            {
                MinValue = 1,
                MaxValue = 5,
                Title = "Ocena za cistocu",
                Foreground = Brushes.Black
            });
            chart1.FontSize = 12;
            chart1.Series.Add(scatterSeries1);
            chart1.BorderBrush = Brushes.Black;
            Ime2.Children.Add(chart1);
            Grid.SetRow(chart1, 1);
            Grid.SetColumn(chart1, 2);


            foreach (GuestRating rating in Rating)
            {
                if (rating.GuestId == user.Id)
                {
                    if (!FilteredRatings.Contains(rating))
                        FilteredRatings.Add(rating);
                    dataGridRatings.ItemsSource = FilteredRatings;
                }
            }
            dataGridRatings.ItemsSource = FilteredRatings;



            foreach (AccommodationReservation reservation in Reservations)
            {
                if (reservation.GuestId == user.Id && reservation.EndDate >= oneYearAgo)
                    brojac++;
            }

            foreach(Model.Guest guest in Guests)
            {
               if(guest.UserId == user.Id && brojac >= 10)
               {   
                    Image.Visibility = Visibility.Visible;
                    SuperGuestLabel.Content = "Super guest";
                    SuperGuestLabel.Foreground = Brushes.Goldenrod;
                    if (guest.IsSuperGuest)
                    {
                        return;
                    }
                    else
                    {
                        Model.Guest super = _guestRepository.FindById(guest.Id);
                        super.IsSuperGuest = true;
                        super.BonusPoints = 5;
                        _guestRepository.Update(super);
                    }                         
               }
               else
               {
                   SuperGuestLabel.Content = "Guest";
                   Model.Guest super1 = _guestRepository.FindByUserId(guest.UserId);
                   super1.IsSuperGuest = false;
                   super1.BonusPoints = 0;
                   _guestRepository.Update(super1);
               }

            }

        }

        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            GuestView guest = new GuestView(User);
            guest.Show();
        }
    }
}
