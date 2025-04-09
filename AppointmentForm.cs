using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace c9692
{
    public partial class AppointmentForm : Form
    {
        string connectionString = "Server=localhost;Database=client_schedule;User Id=sqlUser;Password=Passw0rd!;Port=3306;";

        public AppointmentForm()
        {
            InitializeComponent();
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            LoadAppointmentData();
        }

        private void LoadAppointmentData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            a.appointmentId,
                            a.title AS 'Title',
                            a.description AS 'Description',
                            a.location AS 'Location',
                            a.contact AS 'Contact',
                            a.type AS 'Type',
                            a.url AS 'URL',
                            a.start AS 'Start',
                            a.end AS 'End',
                            c.customerName AS 'Customer',
                            u.userName AS 'User'
                        FROM 
                            appointment a
                        JOIN 
                            customer c ON a.customerId = c.customerId
                        JOIN 
                            user u ON a.userId = u.userId";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Directly bind the data without converting times
                    dataGridViewAppointments.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading appointment data: " + ex.Message);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Add button click logic
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the appointment: " + ex.Message);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewAppointments.SelectedRows.Count > 0)
                {
                    int selectedAppointmentId = Convert.ToInt32(dataGridViewAppointments.SelectedRows[0].Cells["appointmentId"].Value);
                    UpdateAppointmentForm updateAppointmentForm = new UpdateAppointmentForm(selectedAppointmentId);
                    updateAppointmentForm.ShowDialog();
                    LoadAppointmentData();
                }
                else
                {
                    MessageBox.Show("Please select an appointment to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the appointment: " + ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewAppointments.SelectedRows.Count > 0)
                {
                    int selectedAppointmentId = Convert.ToInt32(dataGridViewAppointments.SelectedRows[0].Cells["appointmentId"].Value);

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM appointment WHERE appointmentId = @appointmentId";
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@appointmentId", selectedAppointmentId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        LoadAppointmentData();
                    }
                }
                else
                {
                    MessageBox.Show("Please select an appointment to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the appointment: " + ex.Message);
            }
        }

        private void buttonCalendar_Click(object sender, EventArgs e)
        {
            CalendarForm calendarForm = new CalendarForm();
            calendarForm.ShowDialog();
        }
        private bool isAdjustedToLocal = false; // Tracks if times are adjusted to local
        private bool isRevertedToOriginal = true; // Tracks if times are reverted to UTC
        private void buttonAdjustToLocal_Click(object sender, EventArgs e)
        {
            if (isAdjustedToLocal) return; // Prevent multiple conversions to local

            foreach (DataGridViewRow row in dataGridViewAppointments.Rows)
            {
                if (row.Cells["Start"]?.Value != null && row.Cells["End"]?.Value != null &&
                    !string.IsNullOrWhiteSpace(row.Cells["Start"].Value.ToString()) &&
                    !string.IsNullOrWhiteSpace(row.Cells["End"].Value.ToString()))
                {
                    DateTime originalStart = DateTime.SpecifyKind(Convert.ToDateTime(row.Cells["Start"].Value), DateTimeKind.Utc);
                    DateTime originalEnd = DateTime.SpecifyKind(Convert.ToDateTime(row.Cells["End"].Value), DateTimeKind.Utc);

                    // Convert to local timezone
                    DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(originalStart, TimeZoneInfo.Local);
                    DateTime localEnd = TimeZoneInfo.ConvertTimeFromUtc(originalEnd, TimeZoneInfo.Local);

                    row.Cells["Start"].Value = localStart;
                    row.Cells["End"].Value = localEnd;
                }
            }

            isAdjustedToLocal = true;
            isRevertedToOriginal = false;
        }

        private void buttonRevertToOriginal_Click(object sender, EventArgs e)
        {
            if (isRevertedToOriginal) return; // Prevent multiple conversions to UTC

            foreach (DataGridViewRow row in dataGridViewAppointments.Rows)
            {
                if (row.Cells["Start"]?.Value != null && row.Cells["End"]?.Value != null &&
                    !string.IsNullOrWhiteSpace(row.Cells["Start"].Value.ToString()) &&
                    !string.IsNullOrWhiteSpace(row.Cells["End"].Value.ToString()))
                {
                    DateTime localStart = DateTime.SpecifyKind(Convert.ToDateTime(row.Cells["Start"].Value), DateTimeKind.Local);
                    DateTime localEnd = DateTime.SpecifyKind(Convert.ToDateTime(row.Cells["End"].Value), DateTimeKind.Local);

                    // Convert back to UTC
                    DateTime originalStart = TimeZoneInfo.ConvertTimeToUtc(localStart, TimeZoneInfo.Local);
                    DateTime originalEnd = TimeZoneInfo.ConvertTimeToUtc(localEnd, TimeZoneInfo.Local);

                    row.Cells["Start"].Value = originalStart;
                    row.Cells["End"].Value = originalEnd;
                }
            }

            isRevertedToOriginal = true;
            isAdjustedToLocal = false;
        }

    }
}