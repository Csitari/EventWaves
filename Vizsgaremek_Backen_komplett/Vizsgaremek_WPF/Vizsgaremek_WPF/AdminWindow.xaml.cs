using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace Vizsgaremek_WPF
{
    public partial class AdminWindow : Window
    {
        private const string connectionString = "server=localhost;database=esemenytar;user=root;password=;ssl mode=none;";

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void ShowEventsButton_Click(object sender, RoutedEventArgs e)
        {
            // Az események ablak megnyitása
            EventWindow eventWindow = new EventWindow();
            eventWindow.Show();
        }

        private void ShowUsersButton_Click(object sender, RoutedEventArgs e)
        {
            // A felhasználók ablak megnyitása
            UserWindow userWindow = new UserWindow();
            userWindow.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Kijelentkezési műveletek, például bezárás vagy visszalépés a bejelentkező ablakhoz
            MessageBox.Show("Kijelentkezés...");
            Application.Current.Shutdown(); // Az alkalmazás bezárása
        }
    }
}
