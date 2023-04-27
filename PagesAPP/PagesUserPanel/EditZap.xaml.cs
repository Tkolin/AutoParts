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

namespace AutoParts.PagesAPP
{
    /// <summary>
    /// Логика взаимодействия для EditZap.xaml
    /// </summary>
    public partial class EditZap : Page
    {
        int N;
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();

        public EditZap()
        {
        }

        public EditZap(Запчасть запчасть)
        {
            InitializeComponent();
            N7.ItemsSource = autoPartsBDEntities.Запчасть.ToList();
            N7.DisplayMemberPath = "Наименование";
            N7.SelectedValuePath = "Id";

            N = запчасть.Id;

            N1.Text = запчасть.Наименование;
            N2.Text = запчасть.Марка_авто;
            N3.Text = запчасть.Модель_авто;
            N4.Text = запчасть.Производитель;
            запчасть.кол_во = int.Parse(N5.Text);
            запчасть.цена = int.Parse(N6.Text);
            запчасть.Id_Склад = int.Parse(N7.SelectedValue.ToString());

        }

        private void BTNadduser_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
