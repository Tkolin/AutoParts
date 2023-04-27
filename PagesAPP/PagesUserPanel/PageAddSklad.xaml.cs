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
using AutoParts.Data;

namespace AutoParts.PagesAPP.PagesUserPanel
{
    /// <summary>
    /// Логика взаимодействия для PageAddSklad.xaml
    /// </summary>
    public partial class PageAddSklad : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public PageAddSklad()
        {
            InitializeComponent();
        }

        private void BTNbackus_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void BTNadduser_Click(object sender, RoutedEventArgs e)
        {
            try { 
            Склад склад = new Склад()
            {
                Наименование = N1.Text,
                Адрес = N2.Text,
                Телефон = N3.Text,


            };
            autoPartsBDEntities.Склад.Add(склад);
            autoPartsBDEntities.SaveChanges();
            MessageBox.Show("Успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
