using SQLite;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Truth_or_drink_3
{
    public partial class DurfPage : ContentPage
    {
        private List<Player> _players = new(); // Dynamische spelerslijst
        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();
        private int _selectedStars = 0;
        private bool _isGyroscopeStill = true;
        private bool _isGyroscopeRunning = false;

        private readonly DatabaseService _databaseService;

        private readonly List<(string Text, int Stars)> _questions = new()
        {
            // 1 ster vragen
            ("[Random speler], durf je de persoon naast je een compliment te geven?", 1),
            ("[Random speler], durf je te zeggen wat je zou doen als je miljonair was?", 1),
            ("[Random speler], durf je een slechte grap te vertellen?", 1),
            ("[Random speler], durf je een schattig geluid na te doen van een dier?", 1),
            ("[Random speler], durf je een rare smoes te verzinnen en te delen?", 1),
            ("[Random speler], durf je te vragen wat iedereen echt van je denkt?", 1),

            // 2 sterren vragen
            ("[Random speler], durf je iets te eten dat iemand anders kiest?", 2),
            ("[Random speler], durf je een dansje te doen op een willekeurig lied dat iemand kiest?", 2),
            ("[Random speler], durf je een geheim over je dag te vertellen?", 2),
            ("[Random speler], durf je een slechte review over iets te verzinnen?", 2),
            ("[Random speler], durf je iemand een compliment te geven die je normaal negeert?", 2),
            ("[Random speler], durf je je meest recente appgesprek te openen en voor te lezen?", 2),

            // 3 sterren vragen
            ("[Random speler], durf je een gênant geheim te delen dat niemand weet?", 3),
            ("[Random speler], durf je een schattige bijnaam te verzinnen voor jezelf en deze te gebruiken?", 3),
            ("[Random speler], durf je een onbekend nummer te bellen en te vragen naar hun favoriete kleur?", 3),
            ("[Random speler], durf je een geheim over je kindertijd te vertellen?", 3),
            ("[Random speler], durf je je laatst beluisterde Spotify-nummer te laten zien?", 3),
            ("[Random speler], durf je iemand te vertellen wat je eerste indruk van hen was?", 3),

            // 4 sterren vragen
            ("[Random speler], durf je een geheim te prijs te geven over je kindertijd?", 4),
            ("[Random speler], durf je de eerste foto op je telefoon te laten zien?", 4),
            ("[Random speler], durf je een gênant moment uit je verleden te herbeleven?", 4),
            ("[Random speler], durf je je telefoon aan de persoon links van je te geven voor 1 minuut?", 4),
            ("[Random speler], durf je een TikTok-video te maken en te posten?", 4),
            ("[Random speler], durf je een gênante bijnaam te geven aan iemand anders?", 4),

            // 5 sterren vragen
            ("[Random speler], durf je een bericht te sturen naar je ex?", 5),
            ("[Random speler], durf je een gênant geheim te delen dat niemand weet?", 5),
            ("[Random speler], durf je je meest gênante foto te laten zien aan iedereen?", 5),
            ("[Random speler], durf je het nummer van een willekeurige onbekende te bellen?", 5),
            ("[Random speler], durf je je meest gênante chatbericht te delen?", 5),
            ("[Random speler], durf je een selfie te maken en direct te posten zonder filter?", 5)
        };

        public DurfPage(DatabaseService databaseService, List<Player> players)
        {
            InitializeComponent();
            _databaseService = databaseService;

            // Initialize the players list properly (empty list if null)
            _players = players ?? new List<Player>();
            LoadPlayersFromDatabase();
        }

        private async void LoadPlayersFromDatabase()
        {
            try
            {
                var players = await _databaseService.GetPlayersAsync();
                _players.AddRange(players);

                if (_players.Count == 0)
                {
                    await DisplayAlert("Waarschuwing", "Er zijn geen spelers beschikbaar. Voeg spelers toe om door te gaan.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fout", $"Kon spelers niet laden: {ex.Message}", "OK");
            }
        }

        private void OnPageTapped(object sender, EventArgs e)
        {
            if (_selectedStars == 0)
            {
                QuestionLabel.Text = "Selecteer eerst een moeilijkheidsgraad!";
                return;
            }

            var filteredQuestions = _questions.Where(q => q.Stars <= _selectedStars).ToList();
            if (!filteredQuestions.Any())
            {
                QuestionLabel.Text = "Geen vragen beschikbaar!";
                return;
            }

            ShowNextQuestion(filteredQuestions);
        }

        private async void OnDrinkButtonClicked(object sender, EventArgs e)
        {
            if (_players.Count == 0)
            {
                await DisplayAlert("Fout", "Geen spelers beschikbaar.", "OK");
                return;
            }

            // Verhoog de teller voor de huidige speler
            var currentPlayer = _players[_currentPlayerIndex];
            currentPlayer.DrinkCounter++;

            // Werk de drinkteller bij in de database
            await _databaseService.UpdatePlayerDrinkCounterAsync(currentPlayer.Id, currentPlayer.DrinkCounter);

            // Controleer of de teller 10 bereikt
            if (currentPlayer.DrinkCounter >= 10)
            {
                currentPlayer.DrinkCounter = 0; // Reset de teller
                await ShowGyroscopeChallenge(currentPlayer.Name); // Start de gyroscoop-uitdaging voor deze speler
            }

            // Ga naar de volgende vraag
            if (_selectedStars > 0)
            {
                var filteredQuestions = _questions.Where(q => q.Stars <= _selectedStars).ToList();
                if (filteredQuestions.Any())
                {
                    ShowNextQuestion(filteredQuestions);
                }
            }
        }

        private void ShowNextQuestion(List<(string Text, int Stars)> filteredQuestions)
        {
            if (_players.Count == 0)
            {
                QuestionLabel.Text = "Er zijn geen spelers!";
                return;
            }

            var randomQuestion = filteredQuestions[_random.Next(filteredQuestions.Count)].Text;
            var currentPlayer = _players[_currentPlayerIndex].Name;

            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

            var displayedQuestion = randomQuestion.Replace("[Random speler]", currentPlayer);
            QuestionLabel.Text = displayedQuestion;
        }

        private async Task ShowGyroscopeChallenge(string currentPlayer)
        {
            if (!Gyroscope.IsSupported)
            {
                await DisplayAlert("Geen Gyroscoop", "De gyroscoop wordt niet ondersteund op dit apparaat.", "OK");
                return;
            }

            if (_isGyroscopeRunning)
            {
                await DisplayAlert("Fout", "De gyroscoop draait al. Wacht tot de huidige test is voltooid.", "OK");
                return;
            }

            _isGyroscopeStill = true;
            _isGyroscopeRunning = true;
            int countdown = 5;

            Gyroscope.ReadingChanged += OnGyroscopeReadingChanged;

            try
            {
                Gyroscope.Start(SensorSpeed.UI);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fout", $"Gyroscoop kon niet worden gestart: {ex.Message}", "OK");
                return;
            }

            await DisplayAlert("Challenge!", $"{currentPlayer}, houd de telefoon 5 seconden zo stil mogelijk!", "OK");

            while (countdown > 0)
            {
                await Task.Delay(1000);
                countdown--;
            }

            Gyroscope.Stop();
            Gyroscope.ReadingChanged -= OnGyroscopeReadingChanged;
            _isGyroscopeRunning = false;

            if (_isGyroscopeStill)
            {
                await DisplayAlert("Gefeliciteerd!", "Je hebt de telefoon stil genoeg gehouden. Het spel gaat door!", "OK");
            }
            else
            {
                await DisplayAlert("Te veel bewogen!", $"{currentPlayer}, je moet water gaan drinken!", "OK");
            }
        }

        private void OnGyroscopeReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var reading = e.Reading;

            double tolerance = 0.5; // Verhoog de tolerantie naar 0.5
            if (Math.Abs(reading.AngularVelocity.X) > tolerance ||
                Math.Abs(reading.AngularVelocity.Y) > tolerance ||
                Math.Abs(reading.AngularVelocity.Z) > tolerance)
            {
                _isGyroscopeStill = false;
            }
        }

        private void OnStarPickerChanged(object sender, EventArgs e)
        {
            _selectedStars = StarPicker.SelectedIndex + 1;
        }
    }
}
