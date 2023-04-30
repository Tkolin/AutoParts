using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using AutoParts.Data;

namespace AutoParts.PagesAPP.PagesUserPanel
{
    /// <summary>
    /// Логика взаимодействия для ChekPage.xaml
    /// </summary>
    public partial class ChekPage : Page
    {
        List<Заказ> _zakazData;
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public ChekPage(List<Заказ> заказ)
        {
            InitializeComponent();
            _zakazData = заказ;
        }
        int summ;
        int countItem;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            T1.Content += _zakazData[0].Id.ToString();
            T3.Content += _zakazData[0].Дата_заказа.ToString();
            T4.Content += _zakazData.Count.ToString();

            T6.Content += _zakazData[0].Сотрудник.ФИО;

            N1.Content += _zakazData[0].Id_Клиент.ToString();
            N2.Content += _zakazData[0].Клиент.ФИО;
            N3.Content += _zakazData[0].Клиент.Дата.ToString();
            N4.Content += _zakazData[0].Клиент.Телефон;
            N5.Content += _zakazData[0].Клиент.Паспорт;
            N6.Content += _zakazData[0].Клиент.Пол.Наименование;

            DataTable dt = CreateDataTable<Заказ>(_zakazData);
            dt.Columns.Add("Сумма", typeof(System.Int32));
            
            foreach (DataRow row in dt.Rows)
            {
       
                int i = (int)row["кол_во"];
                row["кол_во"] = i*-1;
                countItem += i*-1;
                int s = (int)row["кол_во"] * (int)((Запчасть)row["Запчасть"]).цена;
                row["Сумма"] = s;
                summ += s;
            }

            dataGrid.ItemsSource = dt.DefaultView;
            T5.Content += countItem.ToString() + " ед."; ;

            T7.Content += summ + " Руб.";
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            backBtn.Visibility = Visibility.Collapsed;
            saveBtn.Visibility = Visibility.Collapsed;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(chekGrid, "Чек " + _zakazData[0].Id.ToString());
            }

            NavigationService.GoBack();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
