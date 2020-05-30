using AZS.Database;
using AZS.Forms;
using AZS.Helper;
using AZS.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AZS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            UserComboBox.ItemsSource = DbHelper.GetContext().User.ToList();

            _timer.Interval = new TimeSpan(0, 0, 1);

            TimeLabel.Content = DateTime.Now.ToString("T");
            _timer.Tick += (s, e) =>
            {
                TimeLabel.Content = DateTime.Now.ToString("T");
            };

            _timer.Start();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserComboBox.SelectedItem is User currentUser)
                {
                    if (PasswordBox.Password == currentUser.Password)
                    {
                        WorkShift workShift = new WorkShift { UserID = currentUser.ID, Datetime = DateTime.Now };

                        DbHelper.GetContext().WorkShift.Add(workShift);
                        DbHelper.GetContext().SaveChanges();
                        OperatorForm operatorForm = new OperatorForm (currentUser );
                        operatorForm.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неправильные логин или пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите оператора!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {

            }
        }
    }
}
