using AZS.Helper;
using AZS.Models;
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
    /// Логика взаимодействия для FuelInfoForm.xaml
    /// </summary>
    public partial class FuelInfoForm : Window
    {
        public FuelColumnControl FuelColumnControl { get; set; }

        public bool FuelVisual
        {
            get
            {
                return FuelColumnControl.FillValue < 10;
            }
        }
        public FuelInfoForm()
        {
            InitializeComponent();



            DataContext = this;
        }

        private void NextFuel_Click(object sender, RoutedEventArgs e)
        {
            this.FuelColumnControl = new FuelColumnControl { Fuel = DbHelper.GetContext().Fuel.Where(n => n.ID == FuelColumnControl.Fuel.ID + 1).Single() };
            DataContext = null;
            DataContext = this;
        }

        private void PrevFuel_Click(object sender, RoutedEventArgs e)
        {
            this.FuelColumnControl = new FuelColumnControl { Fuel = DbHelper.GetContext().Fuel.Where(n => n.ID == FuelColumnControl.Fuel.ID - 1).Single() };
            DataContext = null;
            DataContext = this;
        }
    }
}
