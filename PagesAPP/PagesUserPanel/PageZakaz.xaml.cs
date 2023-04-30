using AutoParts.Data;
using Microsoft.Office.Interop.Excel;
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

namespace AutoParts.PagesAPP.PagesUserPanel
{
    /// <summary>
    /// Логика взаимодействия для PageZakaz.xaml
    /// </summary>
    public partial class PageZakaz : System.Windows.Controls.Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        Клиент pageClient;
        public PageZakaz(Клиент клиент)
        {
            InitializeComponent();
            pageClient = клиент;
            DGClient.Visibility = Visibility.Collapsed;
            DataGridUpdate();
        }
        public PageZakaz()
        {
            InitializeComponent();
            DGClient.Visibility = Visibility.Visible;
            new DateTime(DateTime.Now.Year, 1, 1);
            DataGridUpdate();
        }
        private void DataGridUpdate()
        {
            if(pageClient != null)
                dataGrid.ItemsSource = autoPartsBDEntities.Заказ.Where(x => x.Id_Клиент == pageClient.Id).ToList();
            else
                dataGrid.ItemsSource = autoPartsBDEntities.Заказ.ToList();

        }
        private void RefreshDG_Click(object sender, RoutedEventArgs e)
        {
            DataGridUpdate();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                autoPartsBDEntities.SaveChanges();
                MessageBox.Show("Успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DataGridUpdate();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(pageClient == null)
                this.NavigationService.Navigate(new PageAddZakaz());
            else
                this.NavigationService.Navigate(new PageAddZakaz(pageClient));
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                Заказ zak = ((Заказ)dataGrid.SelectedItem);
                
                if ((zak.Запчасть.кол_во - (zak.кол_во)) > 0)
                {
                    zak.Запчасть.кол_во += -((int)zak.кол_во);
                }

                try
                {
                    autoPartsBDEntities.Заказ.Remove(zak);

                    MessageBox.Show("Заказ удален!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    autoPartsBDEntities.SaveChanges();
                    DataGridUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("В таблице нет данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Otchet_Click(object sender, RoutedEventArgs e)
        {
            List<Заказ> zakList;
            if (pageClient != null)
                zakList = autoPartsBDEntities.Заказ.Where(x => x.Id_Клиент == pageClient.Id).ToList();
            else
                zakList = autoPartsBDEntities.Заказ.ToList();

            DateTime dStart, dEnd;

            if (DpicerStart.SelectedDate != null && DpicerEnd.SelectedDate != null &&
                DpicerStart.SelectedDate.Value >= DpicerEnd.SelectedDate.Value)
            {
                dStart = DpicerStart.SelectedDate.Value;
                dEnd = DpicerEnd.SelectedDate.Value;
            }
            else
            {
                DpicerEnd.SelectedDate = null;
                DpicerStart.SelectedDate = null;
                MessageBox.Show("Установите верный промежуток времени!","Не корректная дата!");
                return;
            }

    
            zakList.Where(x => x.Дата_заказа > dStart && x.Дата_заказа < dEnd);

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;
            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];

            //форматирование текста
            ws.StandardWidth = 9;

            ws.Range["A2:F2"].Merge();
            ws.Range["A2"].Value = "Отчёт по товарообороту";
            ws.Range["A2"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.Range["A2"].Font.Size = 16; ws.Range["A2"].Font.Bold = true;

            if (pageClient != null)
            {
                ws.Range["A3"].Value = "Покупатель:"; ws.Range["B3:C3"].Merge();
                ws.Range["B3"].Value = pageClient.ФИО;
            }
            ws.Range["A4"].Value = "Дата";
            ws.Range["B4"].Value = "от:";
            ws.Range["C4"].Value = dStart;
            ws.Range["D4"].Value = "до:";
            ws.Range["E4"].Value = dEnd;

            ws.Range["A6:H6"].Font.Bold = true; ws.Range["A6:H6"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.Range["A6:H6"].RowHeight = 30; ws.Range["A6:H6"].WrapText = true;
            ws.Range["A6"].Value = "Номер записи";
            ws.Range["B6"].Value = "Дата";
            ws.Range["C6"].Value = "Запчасть";
            ws.Range["D6"].Value = "Сотрудник";
            ws.Range["E6"].Value = "Клиент";
            ws.Range["F6"].Value = "Кол-во";
            ws.Range["G6"].Value = "Цена";
            ws.Range["H6"].Value = "Сумма";
            //



            int numberStart = 7;
            int numberEnd = numberStart;
            foreach (Заказ t in zakList)
            {

                ws.Range["A" + numberEnd].Value = t.Id;
                ws.Range["B" + numberEnd].Value = t.Дата_заказа;
                ws.Range["C" + numberEnd].Value = t.Запчасть.Наименование;
                ws.Range["D" + numberEnd].Value = t.Сотрудник.ФИО;
                if(t.Клиент!=null)
                ws.Range["E" + numberEnd].Value = t.Клиент.ФИО;
                ws.Range["F" + numberEnd].Value = t.кол_во;
                ws.Range["G" + numberEnd].Value = t.Запчасть.цена;
                ws.Range["H" + numberEnd].Formula = "=F" + numberEnd + "*G" + numberEnd;

                numberEnd++;
            }
            app.Calculation = XlCalculation.xlCalculationAutomatic;
            ws.Calculate();
        }
    }
}
