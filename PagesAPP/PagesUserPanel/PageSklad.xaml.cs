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
    /// Логика взаимодействия для PageSklad.xaml
    /// </summary>
    public partial class PageSklad : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public PageSklad()
        {
            InitializeComponent();
           


            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            dataGrid.ItemsSource = autoPartsBDEntities.Склад.ToList();
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

            this.NavigationService.Navigate(new PageAddSklad());
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                {
                    Склад склад = dataGrid.SelectedItems[i] as Склад;
                    autoPartsBDEntities.Склад.Remove(склад);
                }
                MessageBox.Show("Склад удален!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                autoPartsBDEntities.SaveChanges();
                DataGridUpdate();
            }
            else
            {
                MessageBox.Show("В таблице нет данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
