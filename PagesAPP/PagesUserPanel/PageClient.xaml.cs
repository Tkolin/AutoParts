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
                try
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

        private void red_Click(object sender, RoutedEventArgs e)
        {
            TXBsearch.Text = null;
            DataGridUpdate();
        }
        private void TXBsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXBsearch.Text.Length > 0)
            {
                string str = TXBsearch.Text;
                dataGrid.ItemsSource = autoPartsBDEntities.Клиент.Where(x => x.ФИО.ToLower().Contains(str.ToLower())).ToList();
            }
        }

        private void chek_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && Dpicer.SelectedDate != null )
            {

                Клиент клиент = dataGrid.SelectedItem as Клиент;
                DateTime date = Dpicer.SelectedDate.Value;

                List<Заказ> заказs = autoPartsBDEntities.Заказ.
                    Where(x => x.Id_Клиент == клиент.Id &&
                    ((DateTime)x.Дата_заказа) == date
                    ).ToList();
                
                if(заказs.Count > 0)
                NavigationService.Navigate(new ChekPage(заказs)) ;
            }
        }
    }
}
