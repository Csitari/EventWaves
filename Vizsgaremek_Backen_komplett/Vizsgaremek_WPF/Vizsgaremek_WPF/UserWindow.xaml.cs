using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Vizsgaremek_WPF
{
    public partial class UserWindow : Window
    {
        private const string connectionString = "server=localhost;database=esemenytar;user=root;password=;sslmode=none";

        public UserWindow()
        {
            InitializeComponent();
            LoadUsersFromDatabase();
        }

        private void ListBoxUsers_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsersFromDatabase();
        }

        private void LoadUsersFromDatabase()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT felhasznalo_id, felhasznalonev, email, szerep_id FROM felhasznalok";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid userId = reader.GetGuid("felhasznalo_id");
                            string name = reader.GetString("felhasznalonev");
                            string email = reader.GetString("email");
                            int roleId = reader.GetInt32("szerep_id");
                            User user = new User { Id = userId, Name = name, Email = email, RoleId = roleId };
                            listBoxUsers.Items.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a felhasználók betöltése során: " + ex.Message);
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxUsers.SelectedItem != null)
            {
                try
                {
                    User selectedUser = (User)listBoxUsers.SelectedItem;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        string query = "DELETE FROM felhasznalok WHERE felhasznalo_id = @userId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@userId", selectedUser.Id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("A felhasználó sikeresen törölve lett.", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                            listBoxUsers.Items.Remove(selectedUser); // Remove from listbox
                        }
                        else
                        {
                            MessageBox.Show("A felhasználó törlése sikertelen.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt a felhasználó törlése során: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva felhasználó.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MakeAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxUsers.SelectedItem != null)
            {
                try
                {
                    User selectedUser = (User)listBoxUsers.SelectedItem;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        string query = "UPDATE felhasznalok SET szerep_id = 1 WHERE felhasznalo_id = @userId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@userId", selectedUser.Id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("A felhasználó sikeresen adminná lett.", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                            selectedUser.RoleId = 1; // Update in memory
                        }
                        else
                        {
                            MessageBox.Show("A felhasználó adminná tétel sikertelen.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt a felhasználó adminná tétele során: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva felhasználó.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}