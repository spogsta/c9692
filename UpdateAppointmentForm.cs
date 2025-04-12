using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace c9692
{
    public partial class UpdateAppointmentForm : Form
    {
        private int appointmentId;
        private string connectionString = "Server=localhost;Database=client_schedule;User Id=sqlUser;Password=Passw0rd!;Port=3306;";

        // Event to notify when an appointment is updated
        public event Action AppointmentUpdated;

        public UpdateAppointmentForm(int appointmentId)
        {
            InitializeComponent();
            this.appointmentId = appointmentId;
            LoadAppointmentData();
        }

        private void LoadAppointmentData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    title, description, location, contact, type, url, start, end
                FROM 
                    appointment
                WHERE 
                    appointmentId = @appointmentId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBoxTitle.Text = reader["title"].ToString();
                        textBoxDescription.Text = reader["description"].ToString();
                        textBoxLocation.Text = reader["location"].ToString();
                        textBoxContact.Text = reader["contact"].ToString();
                        textBoxType.Text = reader["type"].ToString();
                        textBoxUrl.Text = reader["url"].ToString();

                        // Convert stored EST times to local time for display
                        TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                        DateTime estStart = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(reader["start"]), estTimeZone);
                        DateTime estEnd = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(reader["end"]), estTimeZone);

                        DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(estStart, TimeZoneInfo.Local);
                        DateTime localEnd = TimeZoneInfo.ConvertTimeFromUtc(estEnd, TimeZoneInfo.Local);

                        dateTimePickerStart.Value = localStart;
                        dateTimePickerEnd.Value = localEnd;

                        numericUpDownStartHour.Value = localStart.Hour;
                        numericUpDownStartMinute.Value = localStart.Minute;
                        numericUpDownEndHour.Value = localEnd.Hour;
                        numericUpDownEndMinute.Value = localEnd.Minute;
                    }
                }
            }
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
                MessageBox.Show("The appointment overlaps with an existing appointment. Please choose a different time.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                UPDATE appointment
                SET title = @title, description = @description, location = @location, contact = @contact, type = @type, url = @url, start = @start, end = @end, lastUpdate = NOW(), lastUpdateBy = @lastUpdateBy
                WHERE appointmentId = @appointmentId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", textBoxTitle.Text);
                cmd.Parameters.AddWithValue("@description", textBoxDescription.Text);
                cmd.Parameters.AddWithValue("@location", textBoxLocation.Text);
                cmd.Parameters.AddWithValue("@contact", textBoxContact.Text);
                cmd.Parameters.AddWithValue("@type", textBoxType.Text);
                cmd.Parameters.AddWithValue("@url", textBoxUrl.Text);
                cmd.Parameters.AddWithValue("@start", estStart);
                cmd.Parameters.AddWithValue("@end", estEnd);
                cmd.Parameters.AddWithValue("@lastUpdateBy", "admin"); // Assuming lastUpdateBy is "admin" for now
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Appointment updated successfully!");

                // Notify that the appointment was updated
                AppointmentUpdated?.Invoke();

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
                    WHERE appointmentId != @appointmentId
                    AND ((@start >= start AND @start < end) OR (@end > start AND @end <= end) OR (@start < start AND @end > end))";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                cmd.Parameters.AddWithValue("@start", estStart);
                cmd.Parameters.AddWithValue("@end", estEnd);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
}