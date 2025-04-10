﻿using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace c9692
{
    public partial class UpdateCustomerForm : Form
    {
        private int customerId;
        private string connectionString = "Server=localhost;Database=client_schedule;User Id=sqlUser;Password=Passw0rd!;Port=3306;";

        public UpdateCustomerForm(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        c.customerName,
                        a.phone,
                        a.address,
                        a.address2,
                        a.postalCode,
                        ci.city,
                        co.country
                    FROM 
                        customer c
                    JOIN 
                        address a ON c.addressId = a.addressId
                    JOIN 
                        city ci ON a.cityId = ci.cityId
                    JOIN 
                        country co ON ci.countryId = co.countryId
                    WHERE 
                        c.customerId = @customerId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBoxName.Text = reader["customerName"].ToString();
                        textBoxPhone.Text = reader["phone"].ToString();
                        textBoxAddress.Text = reader["address"].ToString();
                        textBoxAddress2.Text = reader["address2"].ToString();
                        textBoxPostalCode.Text = reader["postalCode"].ToString();
                        textBoxCity.Text = reader["city"].ToString();
                        textBoxCountry.Text = reader["country"].ToString();
                    }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text.Trim();
            string phone = textBoxPhone.Text.Trim();
            string address = textBoxAddress.Text.Trim();
            string address2 = textBoxAddress2.Text.Trim();
            string postalCode = textBoxPostalCode.Text.Trim();
            string city = textBoxCity.Text.Trim();
            string country = textBoxCountry.Text.Trim();

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
                    // Ensure country exists and get countryId
                    string countryQuery = "SELECT countryId FROM country WHERE country = @country";
                    MySqlCommand countryCmd = new MySqlCommand(countryQuery, conn, transaction);
                    countryCmd.Parameters.AddWithValue("@country", country);
                    object countryIdObj = countryCmd.ExecuteScalar();

                    int countryId;
                    if (countryIdObj == null)
                    {
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

                    // Ensure city exists and get cityId
                    string cityQuery = "SELECT cityId FROM city WHERE city = @city AND countryId = @countryId";
                    MySqlCommand cityCmd = new MySqlCommand(cityQuery, conn, transaction);
                    cityCmd.Parameters.AddWithValue("@city", city);
                    cityCmd.Parameters.AddWithValue("@countryId", countryId);
                    object cityIdObj = cityCmd.ExecuteScalar();

                    int cityId;
                    if (cityIdObj == null)
                    {
                        string insertCityQuery = "INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@city, @countryId, NOW(), 'admin', NOW(), 'admin')";
                        MySqlCommand insertCityCmd = new MySqlCommand(insertCityQuery, conn, transaction);
                        insertCityCmd.Parameters.AddWithValue("@city", city);
                        insertCityCmd.Parameters.AddWithValue("@countryId", countryId);
                        insertCityCmd.ExecuteNonQuery();
                        cityId = (int)insertCityCmd.LastInsertedId;
                    }
                    else
                    {
                        cityId = Convert.ToInt32(cityIdObj);
                    }

                    // Update address table
                    string addressQuery = @"
                        UPDATE address a
                        JOIN customer c ON a.addressId = c.addressId
                        SET 
                            a.address = @address,
                            a.address2 = @address2,
                            a.postalCode = @postalCode,
                            a.phone = @phone,
                            a.cityId = @cityId
                        WHERE 
                            c.customerId = @customerId";

                    MySqlCommand addressCmd = new MySqlCommand(addressQuery, conn, transaction);
                    addressCmd.Parameters.AddWithValue("@address", address);
                    addressCmd.Parameters.AddWithValue("@address2", address2);
                    addressCmd.Parameters.AddWithValue("@postalCode", postalCode);
                    addressCmd.Parameters.AddWithValue("@phone", phone);
                    addressCmd.Parameters.AddWithValue("@cityId", cityId);
                    addressCmd.Parameters.AddWithValue("@customerId", customerId);
                    addressCmd.ExecuteNonQuery();

                    // Update customer table
                    string customerQuery = "UPDATE customer SET customerName = @name WHERE customerId = @customerId";
                    MySqlCommand customerCmd = new MySqlCommand(customerQuery, conn, transaction);
                    customerCmd.Parameters.AddWithValue("@name", name);
                    customerCmd.Parameters.AddWithValue("@customerId", customerId);
                    customerCmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Customer updated successfully!");
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