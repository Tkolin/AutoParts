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

            CBoxMarkaUpdate();

            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            dataGrid.ItemsSource = autoPartsBDEntities.Запчасть.ToList();
        }
        private void CBoxMarkaUpdate()
        {
            CBsearch.SelectedValuePath = "Марка_авто";
            CBsearch.DisplayMemberPath = "Марка_авто";
            CBsearch.ItemsSource = autoPartsBDEntities.Запчасть.GroupBy(p=>p.Марка_авто).ToList();
        
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

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddZap());
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                try
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
        private void DataGridFilter()
        {
            List<Запчасть> itemList = autoPartsBDEntities.Запчасть.ToList();

            if (TXBsearch.Text.Length > 0)
            {
                string str = TXBsearch.Text;
                itemList = itemList.Where(x => 
                x.Наименование.ToLower().Contains(str.ToLower()) ||
                x.Производитель.ToLower().Contains(str.ToLower()) ||
                x.Модель_авто.ToLower().Contains(str.ToLower())
                ).ToList();
            }
            if(CBsearch.SelectedItem != null)
            {
                string str = CBsearch.Text;
                itemList = itemList.Where(x =>
                x.Марка_авто.ToLower().Contains(str.ToLower())
                ).ToList();
            }
            dataGrid.ItemsSource = itemList;
        }
        private void TXBsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridFilter();

        }
        private void CBsearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridFilter();
        }
        private void red_Click(object sender, RoutedEventArgs e)
        {
            TXBsearch.Text = null;
            CBsearch.SelectedItem = null;
            DataGridUpdate();
        }

        private void redak_Click(object sender, RoutedEventArgs e)
        {
            Запчасть p = dataGrid.SelectedItem as Запчасть;
            this.NavigationService.Navigate(new PageAddZakaz());
        }

    }
}
