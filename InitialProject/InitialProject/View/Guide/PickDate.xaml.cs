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
using InitialProject.Model;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for PickDate.xaml
    /// </summary>
    public partial class PickDate : Window
    {
        public PickDate()
        {
            InitializeComponent();
        }

        public PickDate(TourRequest selectedData)
        {
            InitializeComponent();
            this.DataContext = this;

            DateBox.DisplayDateStart = selectedData.StartDate;
            DateBox.DisplayDateEnd = selectedData.EndDate;
        }

        public void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
