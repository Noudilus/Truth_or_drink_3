using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Truth_or_drink_3
{
    public partial class Doen : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<GamePlayer> _players;
        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();
        private int _selectedStars = 0;
        private bool _isGyroscopeStill = true;
        private bool _isGyroscopeRunning = false;

        // Constructor that accepts database service and player list
        public Doen(DatabaseService databaseService, List<Player> players)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _players = players.Select(player => new GamePlayer(player)).ToList(); // Convert Player to GamePlayer
        }

        // List of questions with difficulty levels
        private readonly List<(string Text, int Stars)> _questions = new()
        {
            // 1 star questions
            ("[Random speler], doe een gek geluid na dat je als kind vaak maakte.", 1),
            ("[Random speler], neem een gekke pose aan en blijf staan tot de volgende speler aan de beurt is.", 1),
            ("[Random speler], introduceer een denkbeeldig huisdier aan de groep.", 1),
            ("[Random speler], vertel een mop terwijl je moet lachen.", 1),
            ("[Random speler], kruip door de kamer alsof je een baby bent.", 1),
            ("[Random speler], lach alsof je net een geweldige grap hebt gehoord.", 1),
            ("[Random speler], schrijf je naam in de lucht met je neus.", 1),
            ("[Random speler], fluister alles wat je zegt totdat je weer aan de beurt bent.", 1),

            // 2 stars questions
            ("[Random speler], doe alsof je een professionele danser bent en geef ons een korte show.", 2),
            ("[Random speler], draag een vreemd voorwerp alsof het een hoed is voor de volgende ronde.", 2),
            ("[Random speler], zing een kinderliedje alsof het een opera is.", 2),
            ("[Random speler], introduceer jezelf alsof je in een datingshow zit.", 2),
            ("[Random speler], loop alsof je door een modderig veld loopt.", 2),
            ("[Random speler], vertel een geheim, maar het mag alleen klinken alsof het belangrijk is.", 2),
            ("[Random speler], beweeg alsof je in een onzichtbare doos zit.", 2),

            // 3 stars questions
            ("[Random speler], doe alsof je een nieuwslezer bent en lees een krantenkop voor in een dramatische toon.", 3),
            ("[Random speler], vertel een verhaal, maar elke zin moet beginnen met dezelfde letter.", 3),
            ("[Random speler], vertel een mop zonder te glimlachen.", 3),
            ("[Random speler], voer een denkbeeldige choreografie uit alsof je in een muziekvideo zit.", 3),
            ("[Random speler], doe alsof je een tovenaar bent en voer een nep-toverspreuk uit.", 3),
            ("[Random speler], maak een geluidsopname waarin je iemand overtuigt om een absurd product te kopen.", 3),
            ("[Random speler], loop door de kamer alsof je een geheim probeert te bewaren.", 3),

            // 4 stars questions
            ("[Random speler], speel een dramatische scène na waarin je afscheid neemt van iemand.", 4),
            ("[Random speler], vertel een sprookje, maar vervang alle namen door fruitsoorten.", 4),
            ("[Random speler], maak een vliegtuig van papier en laat zien hoe ver het vliegt.", 4),
            ("[Random speler], vertel een spannend verhaal over hoe je vandaag bent aangekomen.", 4),
            ("[Random speler], voer een nep-toverspreuk uit alsof je een tovenaar bent.", 4),
            ("[Random speler], doe alsof je een acteur bent die auditie doet voor een drama.", 4),
            ("[Random speler], maak een dans waarbij je alleen je hoofd mag bewegen.", 4),

            // 5 stars questions
            ("[Random speler], verzin een absurde theorie over waarom de lucht blauw is.", 5),
            ("[Random speler], vertel een verhaal waarbij elk woord met dezelfde letter begint.", 5),
            ("[Random speler], doe alsof je net een prijs hebt gewonnen en bedank iedereen.", 5),
            ("[Random speler], speel een vechtscène uit een film na.", 5),
            ("[Random speler], vertel een sprookje in 30 seconden met je eigen draai eraan.", 5),
            ("[Random speler], vertel een mop, maar doe alsof het een levensles is.", 5),
            ("[Random speler], praat alsof je een alien bent die net op aarde is geland.", 5)
        };

        // Constructor that initializes the page without players
        public Doen(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;

            // Load players from the database
            LoadPlayersAsync();
        }

        // Load players from the database asynchronously
        private async Task LoadPlayersAsync()
        {
            var playersFromDb = await _databaseService.GetPlayersAsync();
            _players = playersFromDb.Select(player => new GamePlayer(player)).ToList();

            if (_players.Count == 0)
            {
                await DisplayAlert("Waarschuwing", "Er zijn geen spelers toegevoegd aan het spel.", "OK");
            }
        }

        // Method called when the page is tapped to show a question
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

        // Method to handle the 'Drink' button click and update the player's drink counter
        private async void OnDrinkButtonClicked(object sender, EventArgs e)
        {
            if (_players == null || _players.Count == 0)
            {
                await DisplayAlert("Fout", "Geen spelers beschikbaar.", "OK");
                return;
            }

            var currentPlayer = _players[_currentPlayerIndex];
            currentPlayer.DrinkCounter++;

            // Update drink counter in the database
            await _databaseService.UpdatePlayerDrinkCounterAsync(currentPlayer.Id, currentPlayer.DrinkCounter);

            // Check if player has to do a challenge after reaching a certain drink count
            if (currentPlayer.DrinkCounter >= 10)
            {
                currentPlayer.DrinkCounter = 0;
                await _databaseService.UpdatePlayerDrinkCounterAsync(currentPlayer.Id, 0);
                await ShowGyroscopeChallenge(currentPlayer.Name);
            }

            if (_selectedStars > 0)
            {
                var filteredQuestions = _questions.Where(q => q.Stars <= _selectedStars).ToList();
                if (filteredQuestions.Any())
                {
                    ShowNextQuestion(filteredQuestions);
                }
            }
        }

        // Method to show the next question from the list
        private void ShowNextQuestion(List<(string Text, int Stars)> filteredQuestions)
        {
            var randomQuestion = filteredQuestions[_random.Next(filteredQuestions.Count)].Text;
            var currentPlayer = _players[_currentPlayerIndex].Name;

            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

            var displayedQuestion = randomQuestion.Replace("[Random speler]", currentPlayer);
            QuestionLabel.Text = displayedQuestion;
        }

        // Method to show a gyroscope challenge for a player
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

        // Method that is triggered when the gyroscope reading changes
        private void OnGyroscopeReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var reading = e.Reading;

            double tolerance = 0.5;
            if (Math.Abs(reading.AngularVelocity.X) > tolerance ||
                Math.Abs(reading.AngularVelocity.Y) > tolerance ||
                Math.Abs(reading.AngularVelocity.Z) > tolerance)
            {
                _isGyroscopeStill = false;
            }
        }

        // Method to handle star picker change
        private void OnStarPickerChanged(object sender, EventArgs e)
        {
            _selectedStars = StarPicker.SelectedIndex + 1;
        }
    }

    // GamePlayer class to hold player data and drink count
    public class GamePlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DrinkCounter { get; set; } = 0;

        public GamePlayer(Player player)
        {
            Id = player.Id;
            Name = player.Name;
            DrinkCounter = player.DrinkCounter;
        }
    }
}