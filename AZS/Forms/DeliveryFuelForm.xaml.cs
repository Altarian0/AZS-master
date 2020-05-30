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
    /// Логика взаимодействия для DeliveryFuelForm.xaml
    /// </summary>
    public partial class DeliveryFuelForm : Window
    {
        public Fuel Fuel { get; set; }

        public int Reception
        {
            get
            {
                return (int)Math.Round((double)Fuel.MaxValue * 0.95);
            }
        }

        public int SaleValue
        {
            get
            {
                Decimal value = Fuel.Sale.Select(n => n.FuelAmount).Sum();
                return (int)value;
            }
        }

        public int Shipment
        {
            get
            {
                return (int)Fuel.Balance - 400;
            }
        }

        public int Balance
        {
            get
            {
                return (int)Fuel.Balance;
            }
        }

        public int FillValue
        {
            get
            {
                Decimal percent = (Fuel.Balance * 95) / Reception;
                return (int)percent;
            }
        }
        public bool FuelVisual
        {
            get
            {
                return FillValue < 10;
            }
        }

        public DeliveryFuelForm()
        {
            InitializeComponent();
            FuelComboBox.ItemsSource = DbHelper.GetContext().Fuel.ToList();
        }

        /// <summary>
        /// Оформление завоза топлива
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal fuelValue = decimal.Parse(FuelValueBox.Text, CultureInfo.InvariantCulture);

                if (fuelValue + Fuel.Balance > Fuel.MaxValue)
                {
                    MessageBox.Show("В баке не помещается столько топлива!");
                    return;
                }

                Fuel.Balance += fuelValue;

                DbHelper.GetContext().FuelDelivery.Add(new FuelDelivery { Date = DateTime.Now, FuelID = Fuel.ID, Volume = fuelValue });
                
                DbHelper.GetContext().SaveChanges();


                this.Close();
            }
            catch
            {
                MessageBox.Show("Введите корректное значение");
            }
        }

        /// <summary>
        /// Обработка выбора топлива в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fuel = (Fuel)FuelComboBox.SelectedItem;
            Fuel = fuel;
            DataContext = null;
            DataContext = this;
            ConfirmButton.IsEnabled = true;
        }

        /// <summary>
        /// Отмена действий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
