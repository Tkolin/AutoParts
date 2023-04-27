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
    /// Логика взаимодействия для PageEmployee.xaml
    /// </summary>
    public partial class PageEmployee : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public PageEmployee()
        {
            InitializeComponent();
           


            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            dataGrid.ItemsSource = autoPartsBDEntities.Сотрудник.ToList();
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
           
        }

        private void AddEmployee_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddEmployee());
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                {
                    Сотрудник сотрудник = dataGrid.SelectedItems[i] as Сотрудник;
                    autoPartsBDEntities.Сотрудник.Remove(сотрудник);
                }
                MessageBox.Show("Сотрудник удален!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
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
