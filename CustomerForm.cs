﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace c9692
{
    public partial class CustomerForm : Form
    {
        string connectionString = "Server=localhost;Database=client_schedule;User Id=sqlUser;Password=Passw0rd!;Port=3306;";

        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        c.customerId,
                        c.customerName AS 'Name',
                        a.phone AS 'Phone',
                        a.address AS 'Address',
                        a.address2 AS 'Address 2',
                        a.postalCode AS 'Postal Code',
                        ci.city AS 'City',
                        co.country AS 'Country'
                    FROM 
                        customer c
                    JOIN 
                        address a ON c.addressId = a.addressId
                    JOIN 
                        city ci ON a.cityId = ci.cityId
                    JOIN 
                        country co ON ci.countryId = co.countryId";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridViewCustomers.DataSource = dataTable;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.ShowDialog();
            LoadCustomerData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                int selectedCustomerId = Convert.ToInt32(dataGridViewCustomers.SelectedRows[0].Cells["customerId"].Value);
                UpdateCustomerForm updateCustomerForm = new UpdateCustomerForm(selectedCustomerId);
                updateCustomerForm.ShowDialog();
                LoadCustomerData();
            }
            else
            {
                MessageBox.Show("Please select a customer to update.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                int selectedCustomerId = Convert.ToInt32(dataGridViewCustomers.SelectedRows[0].Cells["customerId"].Value);

                var confirmResult = MessageBox.Show("Are you sure to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // Delete customer from customer table
                            string deleteCustomerQuery = "DELETE FROM customer WHERE customerId = @customerId";
                            MySqlCommand deleteCustomerCmd = new MySqlCommand(deleteCustomerQuery, conn, transaction);
                            deleteCustomerCmd.Parameters.AddWithValue("@customerId", selectedCustomerId);
                            deleteCustomerCmd.ExecuteNonQuery();

                            // Commit transaction
                            transaction.Commit();
                            MessageBox.Show("Customer deleted successfully!");
                            LoadCustomerData();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }

        private void buttonAppointment_Click(object sender, EventArgs e)
        {
            AppointmentForm appointmentForm = new AppointmentForm();
            appointmentForm.ShowDialog();
        }

        private void buttonCustomerAppointment_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                int selectedCustomerId = Convert.ToInt32(dataGridViewCustomers.SelectedRows[0].Cells["customerId"].Value);
                AddAppointmentForm addAppointmentForm = new AddAppointmentForm(selectedCustomerId);
                addAppointmentForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a customer first.", "No Customer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonGenerateReports_Click(object sender, EventArgs e)
        {
            var appointmentTypesByMonth = GetAppointmentTypesByMonth();
            var userSchedules = GetUserSchedules();
            var appointmentsPerCustomer = GetAppointmentsPerCustomer();

            StringBuilder reportBuilder = new StringBuilder();

            reportBuilder.AppendLine("Appointment Types by Month:");
            foreach (var item in appointmentTypesByMonth)
            {
                reportBuilder.AppendLine($"{item.Key}: {item.Value}");
            }

            reportBuilder.AppendLine("\nUser Schedules:");
            foreach (var user in userSchedules)
            {
                reportBuilder.AppendLine($"{user.Key}:");
                foreach (var appointment in user.Value)
                {
                    reportBuilder.AppendLine($"  {appointment}");
                }
            }

            reportBuilder.AppendLine("\nAppointments per Customer:");
            foreach (var item in appointmentsPerCustomer)
            {
                reportBuilder.AppendLine($"{item.Key}: {item.Value}");
            }

            MessageBox.Show(reportBuilder.ToString(), "Generated Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Dictionary<string, int> GetAppointmentTypesByMonth()
        {
            var appointmentTypesByMonth = new Dictionary<string, int>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        MONTHNAME(start) AS Month, 
                        type, 
                        COUNT(*) AS Count
                    FROM 
                        appointment
                    GROUP BY 
                        Month, type";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string month = reader["Month"].ToString();
                        string type = reader["type"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);

                        string key = $"{month} - {type}";
                        appointmentTypesByMonth[key] = count;
                    }
                }
            }

            return appointmentTypesByMonth;
        }

        private Dictionary<string, List<string>> GetUserSchedules()
        {
            var userSchedules = new Dictionary<string, List<string>>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        u.userName, 
                        a.title, 
                        a.start, 
                        a.end
                    FROM 
                        appointment a
                    JOIN 
                        user u ON a.userId = u.userId
                    ORDER BY 
                        u.userName, a.start";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string userName = reader["userName"].ToString();
                        string title = reader["title"].ToString();
                        DateTime start = Convert.ToDateTime(reader["start"]);
                        DateTime end = Convert.ToDateTime(reader["end"]);

                        string appointmentDetails = $"{title}: {start} - {end}";

                        if (!userSchedules.ContainsKey(userName))
                        {
                            userSchedules[userName] = new List<string>();
                        }

                        userSchedules[userName].Add(appointmentDetails);
                    }
                }
            }

            return userSchedules;
        }

        private Dictionary<string, int> GetAppointmentsPerCustomer()
        {
            var appointmentsPerCustomer = new Dictionary<string, int>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        c.customerName, 
                        COUNT(*) AS Count
                    FROM 
                        appointment a
                    JOIN 
                        customer c ON a.customerId = c.customerId
                    GROUP BY 
                        c.customerName";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string customerName = reader["customerName"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);

                        appointmentsPerCustomer[customerName] = count;
                    }
                }
            }

            return appointmentsPerCustomer;
        }
    }
}