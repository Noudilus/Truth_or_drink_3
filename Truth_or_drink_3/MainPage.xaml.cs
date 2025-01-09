using SQLite;
using System;
using Microsoft.Maui.Storage;

namespace Truth_or_drink_3
{
    public partial class MainPage : ContentPage
    {
        private readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db3");

        public MainPage()
        {
            InitializeComponent();

            // Initialize database
            using var db = new SQLiteConnection(dbPath);
            db.CreateTable<User>();

            // Check if "Onthoud mij" is enabled
            if (Preferences.ContainsKey("SavedUsername") && Preferences.ContainsKey("SavedPassword"))
            {
                UsernameEntry.Text = Preferences.Get("SavedUsername", string.Empty);
                PasswordEntry.Text = Preferences.Get("SavedPassword", string.Empty);
            }
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(UsernameEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (isUsernameEmpty)
            {
                UsernameEntry.Placeholder = "Vul een gebruikersnaam in";
            }
            else if (isPasswordEmpty)
            {
                PasswordEntry.Placeholder = "Vul een wachtwoord in";
            }
            else
            {
                using var db = new SQLiteConnection(dbPath);
                var existingUser = db.Table<User>().FirstOrDefault(u => u.Username == UsernameEntry.Text);
                if (existingUser == null)
                {
                    db.Insert(new User { Username = UsernameEntry.Text, Password = PasswordEntry.Text });
                    DisplayAlert("Succes", "Registratie voltooid", "OK");
                }
                else
                {
                    DisplayAlert("Fout", "Gebruikersnaam bestaat al", "OK");
                }
            }
        }

        public void LoginKnopClicked(object sender, EventArgs e)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(UsernameEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (isUsernameEmpty)
            {
                UsernameEntry.Placeholder = "Vul een gebruikersnaam in";
            }
            else if (isPasswordEmpty)
            {
                PasswordEntry.Placeholder = "Vul een wachtwoord in";
            }
            else
            {
                using var db = new SQLiteConnection(dbPath);
                var user = db.Table<User>().FirstOrDefault(u => u.Username == UsernameEntry.Text && u.Password == PasswordEntry.Text);
                if (user != null)
                {
                    if (OnthoudMijSwitch.IsToggled)
                    {
                        Preferences.Set("SavedUsername", UsernameEntry.Text);
                        Preferences.Set("SavedPassword", PasswordEntry.Text);
                    }

                    Navigation.PushAsync(new Create());
                }
                else
                {
                    DisplayAlert("Fout", "Ongeldige gebruikersnaam of wachtwoord", "OK");
                }
            }
        }
    }

    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; } // Verwijder de required modifier
        public string Password { get; set; } // Verwijder de required modifier
    }
}
