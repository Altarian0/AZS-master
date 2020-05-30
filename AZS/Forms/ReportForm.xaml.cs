using AZS.Helper;
using AZS.Pages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace AZS.Forms
{
    /// <summary>
    /// Логика взаимодействия для ReportForm.xaml
    /// </summary>
    public partial class ReportForm : Window
    {
        public ReportForm()
        {
            InitializeComponent();


        }

        private void MagazineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MagazineComboBox.SelectedIndex == 0)
            {
                InfoFrame.Navigate(new SaleInfoPage());
            }
            else if (MagazineComboBox.SelectedIndex == 1)
            {
                InfoFrame.Navigate(new DeliveryInfoPage());

            }
            else if (MagazineComboBox.SelectedIndex == 2)
            {
                InfoFrame.Navigate(new ShiftInfoPage());

            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExcelRB.IsChecked == false && WordRB.IsChecked == false)
            {
                MessageBox.Show("Выберите тип экспорта!");
                return;
            }

            if (MagazineComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите необходимую сводку!");
                return;
            }

            if (MagazineDate.SelectedDate != null && DayRB.IsChecked == false && MonthRB.IsChecked == false && YearRB.IsChecked == false)
            {
                if (MessageBox.Show("Если вы не выберите время сводки, то сводка будет выполнена за все время. \nЖелаете продолжить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }
            }

            if (ExcelRB.IsChecked == true)
            {
                ExportExcel();
            }
            else if (WordRB.IsChecked == true)
            {
                ExportWord();
            }


        }

        private void ExportWord()
        {

        }

        /// <summary>
        /// Экспорт выбранного журнала в документ Excel 
        /// </summary>
        private void ExportExcel()
        {
            var application = new Excel.Application();

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            int startRowIndex = 2;
            if (MagazineComboBox.SelectedIndex == 0)
            {
                var sales = DbHelper.GetContext().Sale.ToList();

                if (MagazineDate.SelectedDate != null)
                {
                    if (DayRB.IsChecked == true)
                    {
                        sales = sales.Where(n => n.Datetime.Date == ((DateTime)MagazineDate.SelectedDate).Date).ToList();
                    }
                    else if (MonthRB.IsChecked == true)
                    {
                        sales = sales.Where(n => n.Datetime.Month == ((DateTime)MagazineDate.SelectedDate).Month).ToList();
                    }
                    else if (YearRB.IsChecked == true)
                    {
                        sales = sales.Where(n => n.Datetime.Year == ((DateTime)MagazineDate.SelectedDate).Year).ToList();
                    }
                }

                Excel.Worksheet worksheet = application.Worksheets[1];
                worksheet.Name = "Отчет по продажам";

                worksheet.Cells[1][startRowIndex] = "Топливо";
                worksheet.Cells[2][startRowIndex] = "Колонка";
                worksheet.Cells[3][startRowIndex] = "Оператор";
                worksheet.Cells[4][startRowIndex] = "Дата и время";
                worksheet.Cells[5][startRowIndex] = "Количество топлива";
                worksheet.Cells[6][startRowIndex] = "Тип залива";
                worksheet.Cells[7][startRowIndex] = "Сумма";

                foreach (var sale in sales)
                {
                    startRowIndex++;

                    worksheet.Cells[1][startRowIndex] = sale.Fuel.Name;
                    worksheet.Cells[2][startRowIndex] = sale.FuelColumn.Name;
                    worksheet.Cells[3][startRowIndex] = sale.User.Lastname;
                    worksheet.Cells[4][startRowIndex] = sale.Datetime.ToString("dd.MM.yyyy") + " " + sale.Datetime.ToString("t");
                    worksheet.Cells[5][startRowIndex] = sale.FuelAmount;
                    worksheet.Cells[6][startRowIndex] = sale.SaleType.Name;
                    worksheet.Cells[7][startRowIndex] = sale.Sum;

                }
                worksheet.Cells[7][startRowIndex + 1] = sales.Select(n => n.Sum).Sum();
                worksheet.Cells[6][startRowIndex + 1] = "Всего:";
            }
            else if (MagazineComboBox.SelectedIndex == 1)
            {
                var deliveries = DbHelper.GetContext().FuelDelivery.ToList();


                Excel.Worksheet worksheet = application.Worksheets[1];
                worksheet.Name = "Отчет по завозам";

                worksheet.Cells[1][startRowIndex] = "Топливо";
                worksheet.Cells[2][startRowIndex] = "Дата и время";
                worksheet.Cells[3][startRowIndex] = "Объем";

                foreach (var delivery in deliveries)
                {
                    startRowIndex++;

                    worksheet.Cells[1][startRowIndex] = delivery.Fuel.Name;
                    worksheet.Cells[2][startRowIndex] = delivery.Date;
                    worksheet.Cells[3][startRowIndex] = delivery.Volume;
                }

            }
            else if (MagazineComboBox.SelectedIndex == 2)
            {
                var shifts = DbHelper.GetContext().WorkShift.ToList();


                Excel.Worksheet worksheet = application.Worksheets[1];
                worksheet.Name = "Отчет по завозам";

                worksheet.Cells[1][startRowIndex] = "Фамилия";
                worksheet.Cells[2][startRowIndex] = "Имя";
                worksheet.Cells[3][startRowIndex] = "Дата и время";

                foreach (var shift in shifts)
                {
                    startRowIndex++;

                    worksheet.Cells[1][startRowIndex] = shift.User.Lastname;
                    worksheet.Cells[2][startRowIndex] = shift.User.Name;
                    worksheet.Cells[3][startRowIndex] = shift.Datetime;
                }

            }

            application.Visible = true;
        }
    }
}
