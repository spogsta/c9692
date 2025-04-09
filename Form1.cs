using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;

namespace c9692
{
    public partial class Form1 : Form
    {
        private string userLocation;
        private string connectionString = "Server=localhost;Database=client_schedule;User Id=sqlUser;Password=Passw0rd!;Port=3306;";

        public Form1()
        {
            InitializeComponent();
            DetermineUserLocation();
        }

        private void DetermineUserLocation()
        {
            var region = RegionInfo.CurrentRegion;
            userLocation = region.DisplayName;

            switch (region.TwoLetterISORegionName)
            {
                case "US":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
                case "GB":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
                    break;
                case "IN": // India
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("hi-IN");
                    break;
                case "FR": // France
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                    break;
                case "CN": // China
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                    break;
                case "ID": // Indonesia
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("id-ID");
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES"); // Default to Spanish
                    break;
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
                LogLogin(username);
                CheckForUpcomingAppointments(1); // Assuming userId is 1 for now
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
            switch (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName)
            {
                case "hi": // Hindi (India)
                    if (message == "Login successful.")
                        return "लॉगिन सफल।";
                    else if (message == "The username and password do not match.")
                        return "उपयोगकर्ता नाम और पासवर्ड मेल नहीं खाते।";
                    break;

                case "fr": // French (France)
                    if (message == "Login successful.")
                        return "Connexion réussie.";
                    else if (message == "The username and password do not match.")
                        return "Le nom d'utilisateur et le mot de passe ne correspondent pas.";
                    break;

                case "zh": // Chinese (China)
                    if (message == "Login successful.")
                        return "登录成功。";
                    else if (message == "The username and password do not match.")
                        return "用户名和密码不匹配。";
                    break;

                case "id": // Indonesian (Indonesia)
                    if (message == "Login successful.")
                        return "Login berhasil.";
                    else if (message == "The username and password do not match.")
                        return "Nama pengguna dan kata sandi tidak cocok.";
                    break;

                case "es": // Spanish
                    if (message == "Login successful.")
                        return "Inicio de sesión exitoso.";
                    else if (message == "The username and password do not match.")
                        return "El nombre de usuario y la contraseña no coinciden.";
                    break;

                case "en": // English
                default:
                    return message;
            }
            return message; // Fallback to the original message
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CheckForUpcomingAppointments(int userId)
        {
            DateTime currentTimeUtc = DateTime.UtcNow;
            DateTime fifteenMinutesLaterUtc = currentTimeUtc.AddMinutes(15);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        title, start
                    FROM 
                        appointment
                    WHERE 
                        userId = @userId
                        AND start BETWEEN @currentTimeUtc AND @fifteenMinutesLaterUtc";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@currentTimeUtc", currentTimeUtc);
                cmd.Parameters.AddWithValue("@fifteenMinutesLaterUtc", fifteenMinutesLaterUtc);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string title = reader["title"].ToString();
                        DateTime start = Convert.ToDateTime(reader["start"]);
                        TimeZoneInfo userTimeZone = TimeZoneInfo.Local;
                        DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(start, userTimeZone);

                        MessageBox.Show($"You have an upcoming appointment '{title}' at {localStart}.", "Upcoming Appointment Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void LogLogin(string username)
        {
            string logFilePath = "Login_History.txt";
            string logEntry = $"{DateTime.Now}: {username}";

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(logEntry);
            }
        }
    }
}