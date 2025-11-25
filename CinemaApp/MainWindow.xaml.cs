using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TicketsButton_Click(object sender, RoutedEventArgs e)
        {
            TicketsWindow window = new();
            Hide();
            window.ShowDialog();
            Show();
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}