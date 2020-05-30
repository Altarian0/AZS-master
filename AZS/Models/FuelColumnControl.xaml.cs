using AZS.Database;
using AZS.Forms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AZS.Models
{
    /// <summary>
    /// Логика взаимодействия для FuelColumnControl.xaml
    /// </summary>
    public partial class FuelColumnControl : UserControl
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


        public FuelColumnControl()
        {
            InitializeComponent();
        }

        public void Reload()
        {
            this.DataContext = this;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += (s, r) =>
            {
                if (Indicator.Background == Brushes.Red)
                {
                    Indicator.Background = null;
                }
                else
                {
                    Indicator.Background = Brushes.Red;
                }
            };

            if (FillValue >= 80)
            {
                Indicator.Background = Brushes.Green;
            }
            else if (FillValue >= 60)
            {
                Indicator.Background = Brushes.Yellow;
            }
            else if (FillValue >= 30)
            {
                Indicator.Background = Brushes.Orange;
            }
            else if (FillValue < 10)
            {
                timer.Start();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void FuelInfoButton_Click(object sender, RoutedEventArgs e)
        {
            FuelInfoForm fuelInfoForm = new FuelInfoForm { FuelColumnControl = this };
            fuelInfoForm.ShowDialog();
        }

    }
}
