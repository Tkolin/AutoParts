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
    /// Логика взаимодействия для PageClient.xaml
    /// </summary>
    public partial class PageClient : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public PageClient()
        {
            InitializeComponent();
            


            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            dataGrid.ItemsSource = autoPartsBDEntities.Клиент.ToList();
        }

        private void RefreshDG_Click(object sender, RoutedEventArgs e)
        {
            DataGridUpdate();
        }



        private void Save_Click(object sender, RoutedEventArgs e)
        {
            autoPartsBDEntities.SaveChanges();
            DataGridUpdate();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddClient());
            
        }

        private void BthSelectStudent_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate
                (new PageZakaz((sender as Button).DataContext as Клиент));
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                {
                    Клиент клиент = dataGrid.SelectedItems[i] as Клиент;
                    autoPartsBDEntities.Клиент.Remove(клиент);
                }
                MessageBox.Show("Пользователь удален!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                autoPartsBDEntities.SaveChanges();
                DataGridUpdate();
            }
            else
            {
                MessageBox.Show("В таблице нет данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = autoPartsBDEntities.Клиент.OrderBy(x => x.ФИО).ToList();
        }
        private void TXBsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXBsearch.Text.Length > 0)
            {
                string str = TXBsearch.Text;
                dataGrid.ItemsSource = autoPartsBDEntities.Клиент.Where(x => x.ФИО.StartsWith(str)).ToList();
            }
        }
    }
}
