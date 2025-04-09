using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace c9692
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=client_schedule;User Id=sqlUser;Password=Passw0rd!;Port=3306;";
            string name = textBoxName.Text.Trim();
            string phone = textBoxPhone.Text.Trim();
            string address = textBoxAddress.Text.Trim();
            string address2 = textBoxAddress2.Text.Trim();
            string postalCode = textBoxPostalCode.Text.Trim();
            string country = textBoxCountry.Text.Trim();
            string city = textBoxCity.Text.Trim();

            // Validate fields
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(city) || string.IsNullOrEmpty(postalCode) || string.IsNullOrEmpty(country))
            {
                MessageBox.Show("Name, phone, address, city, postal code, and country fields cannot be empty.");
                return;
            }

            if (!Regex.IsMatch(phone, @"^[\d-]+$"))
            {
                MessageBox.Show("Phone number can only contain digits and dashes.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert into address table
                    string addressQuery = "INSERT INTO address (address, address2, postalCode, phone, cityId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@address, @address2, @postalCode, @phone, @cityId, NOW(), 'admin', NOW(), 'admin')";
                    MySqlCommand addressCmd = new MySqlCommand(addressQuery, conn, transaction);
                    addressCmd.Parameters.AddWithValue("@address", address);
                    addressCmd.Parameters.AddWithValue("@address2", address2);
                    addressCmd.Parameters.AddWithValue("@postalCode", postalCode);
                    addressCmd.Parameters.AddWithValue("@phone", phone);
                    addressCmd.Parameters.AddWithValue("@cityId", 1); // Default cityId for now
                    addressCmd.ExecuteNonQuery();
                    int addressId = (int)addressCmd.LastInsertedId;

                    // Insert into customer table
                    string customerQuery = "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@name, @addressId, 1, NOW(), 'admin', NOW(), 'admin')";
                    MySqlCommand customerCmd = new MySqlCommand(customerQuery, conn, transaction);
                    customerCmd.Parameters.AddWithValue("@name", name);
                    customerCmd.Parameters.AddWithValue("@addressId", addressId);
                    customerCmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Customer added successfully!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}