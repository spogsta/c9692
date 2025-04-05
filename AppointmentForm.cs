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
                dataGridViewAppointments.DataSource = dataTable;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Add button click logic
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // Update button click logic
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Delete button click logic
        }
    }
}