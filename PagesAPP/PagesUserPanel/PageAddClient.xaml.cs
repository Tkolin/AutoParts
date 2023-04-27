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
    /// Логика взаимодействия для PageAddClient.xaml
    /// </summary>
    public partial class PageAddClient : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public PageAddClient()
        {
            InitializeComponent();
            N5.ItemsSource = autoPartsBDEntities.Пол.ToList();
            N5.DisplayMemberPath = "Наименование";
            N5.SelectedValuePath = "Id";
        }

        private void BTNbackus_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void BTNadduser_Click(object sender, RoutedEventArgs e)
        {
            try {
                Клиент клиент = new Клиент()
                {
                    ФИО = N1.Text,
                    Телефон = N2.Text,
                    Дата = N3.SelectedDate.Value,
                    Паспорт = N4.Text,
                    Id_Пол = int.Parse(N5.SelectedValue.ToString()),

                };
                autoPartsBDEntities.Клиент.Add(клиент);
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
