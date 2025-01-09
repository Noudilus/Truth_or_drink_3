using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Truth_or_drink_3
{
    public partial class Vraag : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<Player> _players = new(); // Directe lijst van spelers
        private int _currentPlayerIndex = 0; // Huidige speler index
        private readonly Random _random = new(); // Willekeurige vraaggenerator
        private int _selectedStars = 0; // Geselecteerde moeilijkheidsgraad (1-5 sterren)
        private bool _isGyroscopeStill = true; // Houdt bij of de telefoon stil is
        private bool _isGyroscopeRunning = false; // Houdt bij of de gyroscoop draait

        // Constructor die de lijst van spelers ontvangt
        public Vraag(DatabaseService databaseService, List<Player> players)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _players = players ?? new List<Player>(); // Veiligstellen dat de lijst niet null is
        }

        // Vragenlijst met moeilijkheidsgraden
        private readonly List<(string Text, int Stars)> _questions = new()
        {
            // 1 ster
            ("[Random speler], wat is het laatste dat je hebt gegoogeld?", 1),
            ("[Random speler], wat is iets waar je trots op bent?", 1),
            ("[Random speler], wat is de gekste droom die je hebt gehad?", 1),
            ("[Random speler], wie zou je bellen als je in de gevangenis zit?", 1),
            ("[Random speler], als je een superkracht zou kunnen kiezen, welke zou dat zijn?", 1),

            // 2 sterren
            ("[Random speler], wie vind je het knapst in deze kamer?", 2),
            ("[Random speler], wat is het vreemdste dat je ooit hebt gedaan om iemand te imponeren?", 2),
            ("[Random speler], wie zou je kiezen om mee naar een onbewoond eiland te gaan?", 2),
            ("[Random speler], wat is je grootste guilty pleasure op Netflix?", 2),
            ("[Random speler], wie in deze kamer zou het slechtst een geheim kunnen bewaren?", 2),

            // 3 sterren
            ("[Random speler], wat is je meest beschamende moment?", 3),
            ("[Random speler], wat is je grootste geheim?", 3),
            ("[Random speler], wat is het vreemdste compliment dat je ooit hebt gekregen?", 3),
            ("[Random speler], als je een realityshow mocht starten, wie van ons zou je erin casten?", 3),
            ("[Random speler], wat is het meest gênante dat je ooit op social media hebt gepost?", 3),

            // 4 sterren
            ("[Random speler], wat is het meest kinderachtige dat je nog steeds doet?", 4),
            ("[Random speler], wat is het raarste dat iemand ooit tegen je heeft gezegd?", 4),
            ("[Random speler], wie in deze groep zou het meest waarschijnlijk verdwalen tijdens een reis?", 4),
            ("[Random speler], wie in deze groep zou het meest waarschijnlijk een geheim dubbel leven leiden?", 4),
            ("[Random speler], wat is het grootste risico dat je ooit hebt genomen?", 4),

            // 5 sterren
            ("[Random speler], wie van ons zou je kussen voor een miljoen euro?", 5),
            ("[Random speler], wat is het ergste dat je ooit tegen je ouders hebt gelogen?", 5),
            ("[Random speler], als het niet illegaal was, wie zou je vermoorden?", 5),
            ("[Random speler], als je één geheim van iemand in deze groep moest delen, welk geheim zou dat zijn?", 5),
            ("[Random speler], wat is het meest gênante dat je ooit in een groepschat hebt gestuurd?", 5),
        };

        // Methode die wordt uitgevoerd wanneer de pagina wordt getikt
        private void OnPageTapped(object sender, EventArgs e)
        {
            if (_selectedStars == 0)
            {
                QuestionLabel.Text = "Selecteer eerst een moeilijkheidsgraad!";
                return;
            }

            // Filter vragen op basis van geselecteerde sterren
            var filteredQuestions = _questions.Where(q => q.Stars <= _selectedStars).ToList();
            if (!filteredQuestions.Any())
            {
                QuestionLabel.Text = "Geen vragen beschikbaar!";
                return;
            }

            ShowNextQuestion(filteredQuestions);
        }

        // Methode die de volgende vraag toont
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

        // Methode die wordt uitgevoerd wanneer de sterrenkeuze wordt aangepast
        private void OnStarPickerChanged(object sender, EventArgs e)
        {
            _selectedStars = StarPicker.SelectedIndex + 1;
        }

        // Methode om de gyroscoop uitdaging te starten voor een speler
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

        // Methode die wordt uitgevoerd wanneer er veranderingen zijn in de gyroscoopmeting
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

        // Methode voor de "Neem een slok" knop
        private void OnDrinkButtonClicked(object sender, EventArgs e)
        {
            // Je kunt hier de logica voor de "Neem een slok"-knop toevoegen
            QuestionLabel.Text = "Je hebt een slok genomen!";
        }
    }
}
