using System;
using System.Linq;
using System.Text;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using BCrypt.Net;
using MySql.Data.MySqlClient;

namespace Vizsgaremek_WPF
{
    public partial class MainWindow : Window
    {
        private const string connectionString = "server=localhost;database=esemenytar;user=root;password=;ssl mode=none;";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (AuthenticateAdmin(username, password))
            {
                MessageBox.Show("Sikeres bejelentkezés az admin felületbe!", "Bejelentkezés", MessageBoxButton.OK, MessageBoxImage.Information);

                // Az admin ablak létrehozása és megjelenítése
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();

                // A jelenlegi ablak bezárása
                this.Close();
            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó vagy nem rendelkezel admin jogosultsággal!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AuthenticateAdmin(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT szerep_id, jelszo_hash FROM felhasznalok WHERE felhasznalonev = @username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int role_id = reader.GetInt32("szerep_id");
                    string hashedPasswordFromDB = reader.GetString("jelszo_hash");
                    reader.Close();

                    if (role_id == 1 && BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDB))
                    {
                        return true; // Az admin jogosultságú felhasználó sikeresen bejelentkezett
                    }
                }
                return false; // Hibás felhasználónév, jelszó vagy nem rendelkezik admin jogosultsággal
            }
        }
    }
}