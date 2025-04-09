using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace c9692
{
    public partial class CalendarForm : Form
    {
        string connectionString = "Server=localhost;Database=client_schedule;User Id=sqlUser;Password=Passw0rd!;Port=3306;";

        public CalendarForm()
        {
            InitializeComponent();
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = e.Start;
            LoadAppointmentDataForDate(selectedDate);
        }

        private void LoadAppointmentDataForDate(DateTime date)
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
                            user u ON a.userId = u.userId
                        WHERE 
                            DATE(a.start) = @date";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@date", date.Date);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // No conversion to local time; keep times in UTC
                    dataGridViewAppointments.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading appointment data: " + ex.Message);
            }
        }
    }
}