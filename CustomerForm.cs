using MySql.Data.MySqlClient;
using System;
using System.Data;
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
                MessageBox.Show("Please select a customer to add an appointment.");
            }
        }
    }
}