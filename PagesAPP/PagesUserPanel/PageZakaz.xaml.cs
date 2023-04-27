using AutoParts.Data;
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
    public partial class PageZakaz : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        Клиент pageClient;
        public PageZakaz(Клиент клиент)
        {
            InitializeComponent();
            pageClient = клиент;
            N2.ItemsSource = autoPartsBDEntities.Запчасть.ToList();
            N2.DisplayMemberPath = "Наименование";
            N2.SelectedValuePath = "Id";
            N1.ItemsSource = autoPartsBDEntities.Сотрудник.ToList();
            N1.DisplayMemberPath = "ФИО";
            N1.SelectedValuePath = "Id";

            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            dataGrid.ItemsSource = autoPartsBDEntities.Заказ.Where(x => x.Id_Клиент == pageClient.Id).ToList();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("./PagesAdmin/PageClient.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Заказ заказ = new Заказ()
            {
                Id_Клиент = pageClient.Id,
                Id_Сотрудник = int.Parse(N1.SelectedValue.ToString()),
                Id_Запчасть = int.Parse(N2.SelectedValue.ToString()),
            };
            autoPartsBDEntities.Заказ.Add(заказ);
            autoPartsBDEntities.SaveChanges();
            DataGridUpdate();
            MessageBox.Show("Успешно");
        }

        private void BthSelectStudent_Click_1(object sender, RoutedEventArgs e)
        {
            Заказ заказ = (sender as Button).DataContext as Заказ;
            autoPartsBDEntities.Заказ.Remove(заказ);
            autoPartsBDEntities.SaveChanges();
            DataGridUpdate();
        }
    }
}
