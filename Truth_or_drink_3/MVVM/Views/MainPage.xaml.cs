using SQLite;
using System;
using Microsoft.Maui.Storage;
using System.IO;
using System.Threading.Tasks;

namespace Truth_or_drink_3
{
    public partial class MainPage : ContentPage
    {
        private readonly string dbPath;

        public MainPage()
        {
            InitializeComponent();

            // Verkrijg het pad naar de database
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            dbPath = Path.Combine(folderPath, "users.db3");

            // Controleer of de map bestaat, zo niet maak deze dan aan
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Initializeer de database
            try
            {
                using var db = new SQLiteConnection(dbPath);
                db.CreateTable<User>(); // Maak de gebruikers Tabel aan als die nog niet bestaat
            }
            catch (Exception ex)
            {
                // Geef een foutmelding weer als het openen van de database mislukt
                DisplayAlert("Fout", $"Kan database niet openen: {ex.Message}", "OK");
            }

            // Check of "Onthoud mij" is ingeschakeld
            if (Preferences.ContainsKey("SavedUsername") && Preferences.ContainsKey("SavedPassword"))
            {
                UsernameEntry.Text = Preferences.Get("SavedUsername", string.Empty);
                PasswordEntry.Text = Preferences.Get("SavedPassword", string.Empty);
            }
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
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
                try
                {
                    var db = new SQLiteAsyncConnection(dbPath); // Maak een nieuwe verbinding
                    var existingUser = await db.Table<User>().FirstOrDefaultAsync(u => u.Username == UsernameEntry.Text);
                    if (existingUser == null)
                    {
                        await db.InsertAsync(new User { Username = UsernameEntry.Text, Password = PasswordEntry.Text });
                        DisplayAlert("Succes", "Registratie voltooid", "OK");
                    }
                    else
                    {
                        DisplayAlert("Fout", "Gebruikersnaam bestaat al", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Foutmelding als de registratie mislukt
                    DisplayAlert("Fout", $"Kan niet registreren: {ex.Message}", "OK");
                }
            }
        }

        public async void LoginKnopClicked(object sender, EventArgs e)
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
                try
                {
                    var db = new SQLiteAsyncConnection(dbPath); // Maak een nieuwe verbinding
                    var user = await db.Table<User>().FirstOrDefaultAsync(u => u.Username == UsernameEntry.Text && u.Password == PasswordEntry.Text);

                    if (user != null)
                    {
                        if (OnthoudMijSwitch.IsToggled)
                        {
                            Preferences.Set("SavedUsername", UsernameEntry.Text);
                            Preferences.Set("SavedPassword", PasswordEntry.Text);
                        }

                        // Voeg een wachtfunctie toe om te wachten totdat de navigatie is voltooid
                        await Navigation.PushAsync(new Create()); // Pas aan naar een async navigatie
                    }
                    else
                    {
                        DisplayAlert("Fout", "Ongeldige gebruikersnaam of wachtwoord", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Foutmelding als de login mislukt
                    DisplayAlert("Fout", $"Kan niet inloggen: {ex.Message}", "OK");
                }
            }
        }
    }

    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
