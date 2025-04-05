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
                        c.customerName AS 'Name',
                        a.phone AS 'Phone',
                        a.address AS 'Address',
                        a.address2 AS 'Address 2',
                        a.postalCode AS 'Postal Code', -- Added postalCode
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
            // Update customer logic
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Delete customer logic
        }
    }
}