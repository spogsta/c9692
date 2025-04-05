using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace c9692
{
    public partial class AddAppointmentForm : Form
    {
        private int customerId;
        private string connectionString = "Server=localhost;Database=client_schedule;User Id=sqlUser;Password=Passw0rd!;Port=3306;";

        public AddAppointmentForm(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO appointment (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES (@customerId, @userId, @title, @description, @location, @contact, @type, @url, @start, @end, NOW(), @createdBy, NOW(), @lastUpdateBy)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@userId", 1); // Assuming userId is 1 for now
                cmd.Parameters.AddWithValue("@title", textBoxTitle.Text);
                cmd.Parameters.AddWithValue("@description", textBoxDescription.Text);
                cmd.Parameters.AddWithValue("@location", textBoxLocation.Text);
                cmd.Parameters.AddWithValue("@contact", textBoxContact.Text);
                cmd.Parameters.AddWithValue("@type", textBoxType.Text);
                cmd.Parameters.AddWithValue("@url", textBoxUrl.Text);
                cmd.Parameters.AddWithValue("@start", dateTimePickerStart.Value);
                cmd.Parameters.AddWithValue("@end", dateTimePickerEnd.Value);
                cmd.Parameters.AddWithValue("@createdBy", "admin"); // Assuming createdBy is "admin" for now
                cmd.Parameters.AddWithValue("@lastUpdateBy", "admin"); // Assuming lastUpdateBy is "admin" for now

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Appointment added successfully!");
                this.Close();
            }
        }
    }
}