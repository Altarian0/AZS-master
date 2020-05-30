using AZS.Database;
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
    /// Логика взаимодействия для SaleTypeForm.xaml
    /// </summary>
    public partial class SaleTypeForm : Window
    {
        public FuelColumn FuelColumn { get; set; }
        public Fuel Fuel { get; set; }
        public User User { get; set; }

        public SaleTypeForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ValueBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789.".IndexOf(e.Text) < 0;
        }

        private void FullTankRB_Checked(object sender, RoutedEventArgs e)
        {
            ValueBox.IsEnabled = false;
            ValueBox.Text = "60";
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string type = FuelAmountRB.IsChecked == true ? "По литрам" : MoneyRB.IsChecked == true ? "По сумме" : "Полный бак";
                if (FuelAmountRB.IsChecked == false && FullTankRB.IsChecked == false && MoneyRB.IsChecked == false)
                {
                    MessageBox.Show("Выберите тип залива!");
                    return;
                }

                if (decimal.Parse(ValueBox.Text) > Fuel.Balance && type == "По литрам")
                {
                    MessageBox.Show("В баке нет столько топлива!");
                    return;
                }
                else if((decimal.Parse(ValueBox.Text)/Fuel.Price) > Fuel.Balance && type == "По сумме")
                {
                    MessageBox.Show("В баке нет столько топлива!");
                    return;
                }

                decimal amount = 0;

                if (type != "Полный бак")
                {

                    amount = decimal.Parse(ValueBox.Text, CultureInfo.InvariantCulture);
                }
                else
                {
                    amount = 60;
                }

                FuelSaleForm fuelSaleForm = new FuelSaleForm(amount, type, User, FuelColumn, Fuel);
                fuelSaleForm.ShowDialog();
                this.Close();
            }
            catch
            { 

                MessageBox.Show("Введите корректное значение!");

            }
        }
    }
}
