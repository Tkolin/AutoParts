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
    /// Логика взаимодействия для PageZap.xaml
    /// </summary>
    public partial class PageZap : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public PageZap()
        {
            InitializeComponent();
           


            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            dataGrid.ItemsSource = autoPartsBDEntities.Запчасть.ToList();
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
            this.NavigationService.Navigate(new PageAddZap());
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                {
                    Запчасть запчасть = dataGrid.SelectedItems[i] as Запчасть;
                    autoPartsBDEntities.Запчасть.Remove(запчасть);
                }
                MessageBox.Show("Деталь удалена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                autoPartsBDEntities.SaveChanges();
                DataGridUpdate();
            }
            else
            {
                MessageBox.Show("В таблице нет данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TXBsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXBsearch.Text.Length > 0)
            {
                string str = TXBsearch.Text;
                dataGrid.ItemsSource = autoPartsBDEntities.Запчасть.Where(x => x.Наименование.StartsWith(str)).ToList();
            }
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = autoPartsBDEntities.Запчасть.OrderBy(x => x.Наименование).ToList();
        }

        private void redak_Click(object sender, RoutedEventArgs e)
        {
            Запчасть p = dataGrid.SelectedItem as Запчасть;
            this.NavigationService.Navigate(new EditZap());
            
            
        }
    }
}
