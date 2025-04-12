using MySql.Data.MySqlClient;
using System;
using System.Data;
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
            // Get times entered by the user in local time
            DateTime localStart = dateTimePickerStart.Value.Date
                .AddHours((double)numericUpDownStartHour.Value)
                .AddMinutes((double)numericUpDownStartMinute.Value);
            DateTime localEnd = dateTimePickerEnd.Value.Date
                .AddHours((double)numericUpDownEndHour.Value)
                .AddMinutes((double)numericUpDownEndMinute.Value);

            // Convert local times to Eastern Standard Time (EST)
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime estStart = TimeZoneInfo.ConvertTime(localStart, localTimeZone, estTimeZone);
            DateTime estEnd = TimeZoneInfo.ConvertTime(localEnd, localTimeZone, estTimeZone);

            // Validate business hours in EST
            if (!IsWithinBusinessHours(estStart) || !IsWithinBusinessHours(estEnd))
            {
                MessageBox.Show("Appointments must be scheduled during business hours (9:00 a.m. to 5:00 p.m., Monday–Friday, EST).");
                return;
            }

            // Check for overlapping appointments in EST
            if (IsOverlappingAppointment(estStart, estEnd))
            {
                MessageBox.Show("The appointment overlaps with an existing appointment.");
                return;
            }

            // Save the appointment to the database in EST
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
                cmd.Parameters.AddWithValue("@start", estStart);
                cmd.Parameters.AddWithValue("@end", estEnd);
                cmd.Parameters.AddWithValue("@createdBy", "admin"); // Assuming createdBy is "admin" for now
                cmd.Parameters.AddWithValue("@lastUpdateBy", "admin"); // Assuming lastUpdateBy is "admin" for now

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Appointment added successfully!");
                this.Close();
            }
        }

        private bool IsWithinBusinessHours(DateTime estDateTime)
        {
            // Check if within business hours in EST
            if (estDateTime.DayOfWeek == DayOfWeek.Saturday || estDateTime.DayOfWeek == DayOfWeek.Sunday)
                return false;

            TimeSpan startBusinessHours = new TimeSpan(9, 0, 0);
            TimeSpan endBusinessHours = new TimeSpan(17, 0, 0);

            return estDateTime.TimeOfDay >= startBusinessHours && estDateTime.TimeOfDay <= endBusinessHours;
        }

        private bool IsOverlappingAppointment(DateTime estStart, DateTime estEnd)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                        SELECT COUNT(*)
                        FROM appointment
                        WHERE customerId = @customerId
                        AND ((@start >= start AND @start < end) OR (@end > start AND @end <= end) OR (@start < start AND @end > end))";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@start", estStart);
                cmd.Parameters.AddWithValue("@end", estEnd);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void AddAppointmentForm_Load(object sender, EventArgs e)
        {
            // Any initialization logic can go here
        }

        private void labelStartHour_Click(object sender, EventArgs e)
        {

        }
    }
}