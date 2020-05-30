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

namespace AZS.Models
{
    /// <summary>
    /// Логика взаимодействия для FuelControl.xaml
    /// </summary>
    public partial class FuelControl : UserControl
    {
        public FuelColumn FuelColumn { get; set; }
        public User User { get; set; }
        public FuelControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FuelList.ItemsSource = FuelColumn.FuelOfFuelColumn.ToList();
        }

        private void FuelSaleButton_Click(object sender, RoutedEventArgs e)
        {
             var fuel = ((FuelOfFuelColumn)((Button)sender).DataContext).Fuel;
             var fuelColumn = ((FuelOfFuelColumn)((Button)sender).DataContext).FuelColumn;

            SaleTypeForm saleTypeForm = new SaleTypeForm { Fuel = fuel, FuelColumn = FuelColumn, User = User  };
            saleTypeForm.ShowDialog();

            
        }
    }
}
