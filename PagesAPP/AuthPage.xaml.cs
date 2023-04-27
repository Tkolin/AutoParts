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

namespace AutoParts.PagesAPP
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        AutoPartsBDEntities autoPartsBDEntities = new AutoPartsBDEntities();
        public AuthPage()
        {
            InitializeComponent();
        }

        //private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    IconPasswordN1.Visibility = Visibility.Hidden;
        //    IconPasswordN2.Visibility = Visibility.Visible;
        //    PasswordTextBox.Text = Password.Password;
        //    Password.Visibility = Visibility.Hidden;
        //    PasswordTextBox.Visibility = Visibility.Visible;
        //}

        //private void IconPasswordN1_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    IconPasswordN2.Visibility = Visibility.Hidden;
        //    IconPasswordN1.Visibility = Visibility.Visible;
        //    Password.Visibility = Visibility.Visible;
        //    PasswordTextBox.Visibility = Visibility.Hidden;
        //}

        //private void IconPasswordN2_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    IconPasswordN2.Visibility = Visibility.Hidden;
        //    IconPasswordN1.Visibility = Visibility.Visible;
        //    Password.Visibility = Visibility.Visible;
        //    PasswordTextBox.Visibility = Visibility.Hidden;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text.Length == 0)
            {
                MessageBox.Show("Логин пуст");
                return;
            }
            if (Password.Password.Length == 0)
            {
                MessageBox.Show("Пароль пуст");
                return;
            }
            if (autoPartsBDEntities.User.Where(x => x.Login == login.Text).ToList().Count == 0)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }
            if (autoPartsBDEntities.User.Where(x => x.Login == login.Text && x.Password == Password.Password).ToList().Count > 0)
            {
                switch (autoPartsBDEntities.User.Where(x => x.Login == login.Text && x.Password == Password.Password).ToList()[0].Id_Role)
                {
                    case 1:
                        MessageBox.Show("Добро пожаловать, пользователь");
                        this.NavigationService.Navigate(new Uri("./PagesAPP/UserPanel.xaml", UriKind.Relative));
                        break;
                    case 2:
                        MessageBox.Show("Добро пожаловать, администратор");
                        this.NavigationService.Navigate(new Uri("./PagesAPP/AdminPanel.xaml", UriKind.Relative));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный пароль");
            }
        }
    }
}
