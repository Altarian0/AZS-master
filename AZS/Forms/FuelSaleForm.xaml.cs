using AZS.Database;
using AZS.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для FuelSaleForm.xaml
    /// </summary>
    public partial class FuelSaleForm : Window
    {

        public Sale Sale { get; set; } = new Sale();

        public FuelSaleForm(decimal amount, string type, User user, FuelColumn fuelColumn, Fuel fuel)
        {
            InitializeComponent();

            PayType.Items.Add("Наличные");
            PayType.Items.Add("Карта");
            PayType.Items.Add("Наличные");
            PayType.Items.Add("Тех. прокачка");

            Sale.FuelID = fuel.ID;
            Sale.FuelColumnID = fuelColumn.ID;
            Sale.Datetime = DateTime.Now;

            if (type == "По литрам")
            {
                OrderFuel.Text = amount.ToString() + " Л.";
                OrderPrice.Text = (amount * fuel.Price).ToString() + " Руб.";
                Sale.Sum = (amount * fuel.Price);
                Sale.FuelAmount = amount;
            }
            else if (type == "По сумме")
            {
                OrderPrice.Text = amount.ToString() + " Руб.";
                OrderFuel.Text = (amount / fuel.Price).ToString() + " Л.";
                Sale.Sum = amount;
                Sale.FuelAmount = amount / fuel.Price;
            }
            else
            {
                OrderPrice.Text = (amount * fuel.Price).ToString() + " Руб.";
                OrderFuel.Text = "Полн. бак";
                Sale.Sum = (amount * fuel.Price);
                Sale.FuelAmount = amount;
            }

            Sale.UserID = user.ID;
            Sale.SaleTypeID = DbHelper.GetContext().SaleType.Where(n => n.Name == type).Single().ID;

            Sale.User = user;
            Sale.Fuel = fuel;
            Sale.FuelColumn = fuelColumn;
            Sale.SaleType = DbHelper.GetContext().SaleType.Where(n => n.Name == type).Single();

            DataContext = this;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PayBox.Text == "" || decimal.Parse(PayBox.Text, CultureInfo.InvariantCulture) < Sale.Sum)
                {
                    MessageBox.Show("Недостаточная сумма для совершения платежа!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Введите корректное значение!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            DbHelper.GetContext().Sale.Add(Sale);
            Sale.Fuel.Balance = Sale.Fuel.Balance - Sale.FuelAmount;
            DbHelper.GetContext().SaveChanges();

            OperatorHelper.OperatorForm.ReloadPage();

            var orderList = DbHelper.GetContext().Sale.ToList();
            var orderShift = orderList.Where(n => n.Datetime.DayOfYear == DateTime.Now.DayOfYear);
            int orderNum = orderShift.Count() + 1;

            string shift = $"  Время: {DateTime.Now.ToString("t")} \n Регистрация розницы \n" +
                           $" Заказ №{orderNum} Пост: {Sale.FuelColumn.Name} \n" +
                           $" Продукт: \"{Sale.Fuel.Name}\" Цена: {Sale.Fuel.Price} Руб.\n" +
                           $" Заказано: {Sale.FuelAmount} л (на {Sale.Sum} Руб.)\n" +
                           $" Платеж: Наличные\n" +
                           $" Получено: {PayBox.Text} Руб.\n" +
                           $" Отгружено: {Sale.FuelAmount} л ({Sale.Sum} Руб.)\n" +
                           $" Сдача: {decimal.Parse(PayBox.Text, CultureInfo.InvariantCulture) - Sale.Sum} Руб.\n" +
                           $"_____________________________________________";
            OperatorHelper.OperatorForm.WorkShiftList.Items.Add(shift);

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
