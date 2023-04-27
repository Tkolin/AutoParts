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
    /// Логика взаимодействия для PageAddZap.xaml
    /// </summary>
    public partial class PageAddZap : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public PageAddZap(Запчасть запчасть)
        {
            InitializeComponent();
            N7.ItemsSource = autoPartsBDEntities.Склад.ToList();
            N7.DisplayMemberPath = "Наименование";
            N7.SelectedValuePath = "Id";
        }

        public PageAddZap()
        {
        }

        private void BTNbackus_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void BTNadduser_Click(object sender, RoutedEventArgs e)
        {
            try { 
            Запчасть запчасть = new Запчасть()
            {
                Наименование = N1.Text,
                Марка_авто = N2.Text,
                Модель_авто = N3.Text,
                Производитель = N4.Text,
                кол_во = int.Parse(N5.Text),
                цена = int.Parse(N6.Text),
                Id_Склад = int.Parse(N7.SelectedValue.ToString()),

            };
            autoPartsBDEntities.Запчасть.Add(запчасть);
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
