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
    /// Логика взаимодействия для PageAddZakaz.xaml
    /// </summary>
    public partial class PageAddZakaz : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        Клиент _клиент;
        public PageAddZakaz(Клиент клиент)
        {
            InitializeComponent();



        

            this._клиент = клиент;
        }

        public PageAddZakaz()
        {
            InitializeComponent();
            clientBox.Visibility = Visibility.Collapsed;
        }

        private void BTNbackus_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void BTNadduser_Click(object sender, RoutedEventArgs e)
        {
            if (N2.SelectedItem == null || N3.SelectedItem == null ||
                N4.Text.Length < 1 || N7.SelectedDate == null)
                return;

            Запчасть запчасть = autoPartsBDEntities.Запчасть.Where(x=> x.Id == ((Запчасть)N3.SelectedItem).Id).First() ;
            int count = Convert.ToInt32(N4.Text);

            Заказ заказ;
            //Убыток
            if (_клиент != null)
            {
          

                if (запчасть.кол_во < count)
                {
                    MessageBox.Show("Не хватает предметов на складе!");
                    N4.Text = null;
                    return;
                }
                запчасть.кол_во -= count;
                заказ = new Заказ();
                {
                    заказ.Id_Клиент = _клиент.Id;
                    заказ.Id_Сотрудник = Convert.ToInt32(N2.SelectedValue);
                    заказ.Id_Запчасть = запчасть.Id;
                    заказ.кол_во = (-count);
                    заказ.Дата_заказа = N7.SelectedDate;
                }
 
            }

            //Поступление
            else
            {
                запчасть.кол_во += count;
                заказ = new Заказ();
                {
                    заказ.Id_Сотрудник = Convert.ToInt32(N2.SelectedValue);
                    заказ.Id_Запчасть = запчасть.Id;
                    заказ.кол_во = count;
                    заказ.Дата_заказа = N7.SelectedDate;
                }

            }


           
                autoPartsBDEntities.Заказ.Add(заказ);
                autoPartsBDEntities.SaveChanges();
                MessageBox.Show("Успешно");
  
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            N1.ItemsSource = autoPartsBDEntities.Клиент.ToList();
            N1.DisplayMemberPath = "ФИО";
            N1.SelectedValuePath = "Id";

            N2.ItemsSource = autoPartsBDEntities.Сотрудник.ToList();
            N2.DisplayMemberPath = "ФИО";
            N2.SelectedValuePath = "Id";

            N3.ItemsSource = autoPartsBDEntities.Запчасть.ToList();
            N3.DisplayMemberPath = "Наименование";
            N3.SelectedValuePath = "Id";

            if (_клиент != null)
            {
                N1.IsEnabled = false;
                N1.SelectedItem = _клиент;


            }
        }
    }
}
