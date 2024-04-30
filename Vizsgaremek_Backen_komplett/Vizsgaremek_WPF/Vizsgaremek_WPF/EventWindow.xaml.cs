using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Vizsgaremek_WPF
{
    public partial class EventWindow : Window
    {
        private const string connectionString = "server=localhost;database=esemenytar;user=root;password=;sslmode=none";

        public EventWindow()
        {
            InitializeComponent();
            LoadEventsFromDatabase();
        }

        private void LoadEventsFromDatabase()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT esemeny_id, cim FROM esemenyek";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid eventId = reader.GetGuid("esemeny_id");
                            string title = reader.GetString("cim");
                            comboBoxEvents.Items.Add(new Event { Id = eventId, Title = title });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt az események betöltése során: " + ex.Message);
            }
        }

        private void ComboBoxEvents_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxEvents.SelectedItem != null)
            {
                Event selectedEvent = (Event)comboBoxEvents.SelectedItem;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxEvents.SelectedItem != null)
            {
                try
                {
                    Event selectedEvent = (Event)comboBoxEvents.SelectedItem;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        string query = "DELETE FROM esemenyek WHERE esemeny_id = @eventId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@eventId", selectedEvent.Id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Az esemény sikeresen törölve lett.", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                            comboBoxEvents.Items.Remove(selectedEvent); // Remove from combobox
                            comboBoxEvents.SelectedItem = null;
                        }
                        else
                        {
                            MessageBox.Show("Az esemény törlése sikertelen.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt az esemény törlése során: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva esemény.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
