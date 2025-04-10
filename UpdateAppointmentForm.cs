﻿using MySql.Data.MySqlClient;
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

                        // Directly set the DateTime values without timezone conversion
                        DateTime start = Convert.ToDateTime(reader["start"]);
                        DateTime end = Convert.ToDateTime(reader["end"]);

                        dateTimePickerStart.Value = start;
                        dateTimePickerEnd.Value = end;

                        numericUpDownStartHour.Value = start.Hour;
                        numericUpDownStartMinute.Value = start.Minute;
                        numericUpDownEndHour.Value = end.Hour;
                        numericUpDownEndMinute.Value = end.Minute;
                    }
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Get times directly from the controls
            DateTime start = dateTimePickerStart.Value.Date.AddHours((double)numericUpDownStartHour.Value).AddMinutes((double)numericUpDownStartMinute.Value);
            DateTime end = dateTimePickerEnd.Value.Date.AddHours((double)numericUpDownEndHour.Value).AddMinutes((double)numericUpDownEndMinute.Value);

            // Check if the appointment is within business hours
            if (!IsWithinBusinessHours(start) || !IsWithinBusinessHours(end))
            {
                MessageBox.Show("Appointments must be scheduled during business hours (9:00 a.m. to 5:00 p.m., Monday–Friday, EST).");
                return;
            }

            // Check if the appointment overlaps with an existing one
            if (IsOverlappingAppointment(start, end))
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
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
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

        private bool IsWithinBusinessHours(DateTime dateTime)
        {
            // Convert to Eastern Standard Time
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime estDateTime = TimeZoneInfo.ConvertTime(dateTime, est);

            // Check if within business hours
            if (estDateTime.DayOfWeek == DayOfWeek.Saturday || estDateTime.DayOfWeek == DayOfWeek.Sunday)
                return false;

            TimeSpan startBusinessHours = new TimeSpan(9, 0, 0);
            TimeSpan endBusinessHours = new TimeSpan(17, 0, 0);

            return estDateTime.TimeOfDay >= startBusinessHours && estDateTime.TimeOfDay <= endBusinessHours;
        }

        private bool IsOverlappingAppointment(DateTime start, DateTime end)
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
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
}