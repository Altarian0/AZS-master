using AZS.Database;
using AZS.Helper;
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

namespace AZS.Forms
{
    /// <summary>
    /// Логика взаимодействия для OperatorForm.xaml
    /// </summary>
    public partial class OperatorForm : Window
    {
        private User _user = new User();

        public OperatorForm(User user)
        {
            InitializeComponent();

            _user = user;

            OperatorHelper.OperatorForm = this;

            TodayLabel.Content = $"Сегодня: {DateTime.Now.ToString("dd.MM.yyyy")} ({DateTime.Now.DayOfWeek})";
            OperatorLabel.Content = $"Оператор - {user.Lastname}";

            ReloadPage();

            FirstColumn.FuelColumn = DbHelper.GetContext().FuelColumn.Where(n => n.Name == "№1").Single();
            SecondColumn.FuelColumn = DbHelper.GetContext().FuelColumn.Where(n => n.Name == "№2").Single();
            ThirdColumn.FuelColumn = DbHelper.GetContext().FuelColumn.Where(n => n.Name == "№3").Single();
            FourthColumn.FuelColumn = DbHelper.GetContext().FuelColumn.Where(n => n.Name == "№4").Single();

            FirstColumn.User = user;
            SecondColumn.User = user;
            ThirdColumn.User = user;
            FourthColumn.User = user;

            var shiftList = DbHelper.GetContext().WorkShift.ToList();
            var todayShift = shiftList.Where(n => n.Datetime.DayOfYear == DateTime.Now.DayOfYear);

            int shiftNum = todayShift.Count() + 1;
            WorkShiftList.Items.Add($"  Время: {DateTime.Now.ToString("t")} \n Открытие смены \n Оператор : {user.Lastname} \n Смена №{shiftNum} \n _____________________________________________");

        }



        public void ReloadPage()
        {
            DbHelper.GetContext().ChangeTracker.Entries().ToList().ForEach(n => n.Reload());

            DieselFuel.Fuel = DbHelper.GetContext().Fuel.Where(n => n.Name == "ДТ").Single();
            Au92Fuel.Fuel = DbHelper.GetContext().Fuel.Where(n => n.Name == "Аи92").Single();
            Au95Fuel.Fuel = DbHelper.GetContext().Fuel.Where(n => n.Name == "Аи95").Single();
            A76Fuel.Fuel = DbHelper.GetContext().Fuel.Where(n => n.Name == "А76").Single();

            DieselFuel.DataContext = null;
            Au92Fuel.DataContext = null;
            Au95Fuel.DataContext = null;
            A76Fuel.DataContext = null;

            DieselFuel.Reload();
            Au92Fuel.Reload();
            Au95Fuel.Reload();
            A76Fuel.Reload();
        }

        public void ReloadFuel()
        {
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.ShowDialog();
        }

        private void DeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            DeliveryFuelForm deliveryFuelForm = new DeliveryFuelForm();
            deliveryFuelForm.ShowDialog();
            ReloadPage();
        }
    }
}
