using AZS.Database;
using AZS.Forms;
using AZS.Helper;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AZS.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        public AuthPage()
        {
            InitializeComponent();

            UserComboBox.ItemsSource = DbHelper.GetContext().User.ToList();

            _timer.Interval = new TimeSpan(0, 0, 1);

            TimeLabel.Content = DateTime.Now.ToString("hh:mm:ss");
            _timer.Tick += (s, e) =>
            {
                TimeLabel.Content = DateTime.Now.ToString("hh:mm:ss");
            };

            _timer.Start();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
