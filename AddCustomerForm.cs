using MySql.Data.MySqlClient;
using System;
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
            string name = textBoxName.Text;
            string phone = textBoxPhone.Text;
            string address = textBoxAddress.Text;
            string address2 = textBoxAddress2.Text;
            string postalCode = textBoxPostalCode.Text; // Added postalCode
            string country = textBoxCountry.Text;
            string city = textBoxCity.Text;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Ensure country exists and get countryId
                    string countryQuery = "SELECT countryId FROM country WHERE country = @country";
                    MySqlCommand countryCmd = new MySqlCommand(countryQuery, conn, transaction);
                    countryCmd.Parameters.AddWithValue("@country", country);
                    object countryIdObj = countryCmd.ExecuteScalar();

                    int countryId;
                    if (countryIdObj == null)
                    {
                        // Insert new country if it doesn't exist
                        string insertCountryQuery = "INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@country, NOW(), 'admin', NOW(), 'admin')";
                        MySqlCommand insertCountryCmd = new MySqlCommand(insertCountryQuery, conn, transaction);
                        insertCountryCmd.Parameters.AddWithValue("@country", country);
                        insertCountryCmd.ExecuteNonQuery();
                        countryId = (int)insertCountryCmd.LastInsertedId;
                    }
                    else
                    {
                        countryId = Convert.ToInt32(countryIdObj);
                    }

                    // Insert into city table
                    string cityQuery = "INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@city, @countryId, NOW(), 'admin', NOW(), 'admin')";
                    MySqlCommand cityCmd = new MySqlCommand(cityQuery, conn, transaction);
                    cityCmd.Parameters.AddWithValue("@city", city);
                    cityCmd.Parameters.AddWithValue("@countryId", countryId);
                    cityCmd.ExecuteNonQuery();
                    int cityId = (int)cityCmd.LastInsertedId;

                    // Insert into address table
                    string addressQuery = "INSERT INTO address (address, address2, postalCode, phone, cityId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@address, @address2, @postalCode, @phone, @cityId, NOW(), 'admin', NOW(), 'admin')";
                    MySqlCommand addressCmd = new MySqlCommand(addressQuery, conn, transaction);
                    addressCmd.Parameters.AddWithValue("@address", address);
                    addressCmd.Parameters.AddWithValue("@address2", address2);
                    addressCmd.Parameters.AddWithValue("@postalCode", postalCode); // Added postalCode parameter
                    addressCmd.Parameters.AddWithValue("@phone", phone);
                    addressCmd.Parameters.AddWithValue("@cityId", cityId);
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