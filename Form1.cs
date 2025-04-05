using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;

namespace c9692
{
    public partial class Form1 : Form
    {
        private string userLocation;

        public Form1()
        {
            InitializeComponent();
            DetermineUserLocation();
        }

        private void DetermineUserLocation()
        {
            var region = RegionInfo.CurrentRegion;
            userLocation = region.DisplayName;

            if (region.TwoLetterISORegionName == "US")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
            else if (region.TwoLetterISORegionName == "GB")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES"); // Example for Spanish
            }

            labelLocation.Text = $"You're country is: {userLocation}.";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (username == "test" && password == "test")
            {
                labelMessage.Text = TranslateMessage("Login successful.");
                CustomerForm customerForm = new CustomerForm();
                customerForm.Show();
            }
            else
            {
                labelMessage.Text = TranslateMessage("The username and password do not match.");
            }
        }

        private string TranslateMessage(string message)
        {
            string translatedMessage = message;
            if (message == "Login successful.")
            {
                translatedMessage += "\nInicio de sesión exitoso.";
            }
            else if (message == "The username and password do not match.")
            {
                translatedMessage += "\nEl nombre de usuario y la contraseña no coinciden.";
            }
            return translatedMessage;
        }
    }
}