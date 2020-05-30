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

namespace AZS.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShiftInfoPage.xaml
    /// </summary>
    public partial class ShiftInfoPage : Page
    {
        public ShiftInfoPage()
        {
            InitializeComponent();

            ShiftsData.ItemsSource = DbHelper.GetContext().WorkShift.ToList();
        }
    }
}
