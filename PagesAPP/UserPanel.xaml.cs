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
    /// Логика взаимодействия для UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Page
    {
        public UserPanel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Uri("../PagesAPP/PagesUserPanel/PageClient.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Uri("../PagesAPP/PagesUserPanel/PageSklad.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Uri("../PagesAPP/PagesUserPanel/PageZap.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Uri("../PagesAPP/PagesUserPanel/PageEmployee.xaml", UriKind.Relative));
        }
    }
}
