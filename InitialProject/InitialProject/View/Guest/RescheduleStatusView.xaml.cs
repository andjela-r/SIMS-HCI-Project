using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RescheduleStatusView.xaml
    /// </summary>
    public partial class RescheduleStatusView : Window
    {
        private readonly RequestStatusRepository _requestRepository;
        public ObservableCollection<RequestStatus> Requests { get; set; }

        public RescheduleStatusView(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            _requestRepository = new RequestStatusRepository();
            Requests = new ObservableCollection<RequestStatus>(_requestRepository.FindAll());
            foreach (RequestStatus request in Requests)
            {
                if (selectedReservation.Id == request.ReservationId)
                {
                    if(request.Request == RequestStatusEnum.Accepted)
                    {
                        StatusLabel.Foreground = Brushes.Green;
                        StatusLabel.Content = request.Request;    
                    }
                    else if (request.Request == RequestStatusEnum.Waiting)
                    {
                        StatusLabel.Foreground = Brushes.Yellow;
                        StatusLabel.Content = request.Request;
                    }
                    else
                    {
                        StatusLabel.Foreground = Brushes.Red;
                        StatusLabel.Content = request.Request;  
                    }
                    CommentLabel.Content = request.Comment;
                }
            }
            
        }
    }
}
