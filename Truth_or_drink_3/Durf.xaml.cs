using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Truth_or_drink_3
{
    public partial class Durf : ContentPage
    {
        // Lijst met spelers
        private readonly List<string> _players = new()
        {
            "Noud", "Eva", "Ricardo", "Dewi", "Rimke", "Damon", "Indy"
        };

        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();
        private SQLiteAsyncConnection _database;

        public Durf()
        {
            InitializeComponent();
            InitializeDatabaseAndQuestions();
        }

        // Initialiseer de database en voeg vragen toe als ze nog niet bestaan
        private async void InitializeDatabaseAndQuestions()
        {
            // Database-initialisatie
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "questions.db");
            _database = new SQLiteAsyncConnection(dbPath);

            // Maak de tabel als die nog niet bestaat
            await _database.CreateTableAsync<Question>();

            // Voeg vragen toe als ze nog niet in de database staan
            var existingQuestions = await _database.Table<Question>().ToListAsync();
            if (!existingQuestions.Any())
            {
                var questions = new List<string>
                {
                    //1 ster
                    "[Random speler], durf je de persoon naast je een compliment te geven?",
                    "[Random speler], durf je te zeggen wat je zou doen als je miljonair was?",
                    "[Random speler], durf je een slechte grap te vertellen?",
                    "[Random speler], durf je een schattig geluid na te doen van een dier?",
                    "[Random speler], durf je een rare smoes te verzinnen en te delen?",
                    "[Random speler], durf je te vragen wat iedereen echt van je denkt?",
                    //2 sterren
                    "[Random speler], durf je iets te eten dat iemand anders kiest?",
                    "[Random speler], durf je een dansje te doen op een willekeurig lied dat iemand kiest?",
                    "[Random speler], durf je een geheim over je dag te vertellen?",
                    "[Random speler], durf je een slechte review over iets te verzinnen?",
                    "[Random speler], durf je iemand een compliment te geven die je normaal negeert?",
                    "[Random speler], durf je je meest recente appgesprek te openen en voor te lezen?",
                    //3 sterren
                    "[Random speler], durf je een g�nant geheim te delen dat niemand weet?",
                    "[Random speler], durf je een schattige bijnaam te verzinnen voor jezelf en deze te gebruiken?",
                    "[Random speler], durf je een onbekend nummer te bellen en te vragen naar hun favoriete kleur?",
                    "[Random speler], durf je een geheim over je kindertijd te vertellen?",
                    "[Random speler], durf je je laatst beluisterde Spotify-nummer te laten zien?",
                    "[Random speler], durf je iemand te vertellen wat je eerste indruk van hen was?",
                    //4 sterren
                    "[Random speler], durf je een geheim te prijs te geven over je kindertijd?",
                    "[Random speler], durf je de eerste foto op je telefoon te laten zien?",
                    "[Random speler], durf je een g�nant moment uit je verleden te herbeleven?",
                    "[Random speler], durf je je telefoon aan de persoon links van je te geven voor 1 minuut?",
                    "[Random speler], durf je een TikTok-video te maken en te posten?",
                    "[Random speler], durf je een g�nante bijnaam te geven aan iemand anders?",
                    //5 sterren
                    "[Random speler], durf je een bericht te sturen naar je ex?",
                    "[Random speler], durf je een g�nant geheim te delen dat niemand weet?",
                    "[Random speler], durf je je meest g�nante foto te laten zien aan iedereen?",
                    "[Random speler], durf je het nummer van een willekeurige onbekende te bellen?",
                    "[Random speler], durf je je meest g�nante chatbericht te delen?",
                    "[Random speler], durf je een selfie te maken en direct te posten zonder filter?",
                };

                foreach (var question in questions)
                {
                    await _database.InsertAsync(new Question { Text = question });
                }
            }
        }

        // Methode die wordt uitgevoerd bij een tik op het scherm
        private async void OnPageTapped(object sender, EventArgs e)
        {
            // Haal alle vragen uit de database
            var questions = await _database.Table<Question>().ToListAsync();
            if (!questions.Any())
            {
                QuestionLabel.Text = "Geen vragen beschikbaar!";
                return;
            }

            // Kies een willekeurige vraag
            string randomQuestion = questions[_random.Next(questions.Count)].Text;

            // Haal de huidige speler op
            string currentPlayer = _players[_currentPlayerIndex];

            // Update de index naar de volgende speler (circulair)
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

            // Vervang [Random speler] met de geselecteerde speler
            string displayedQuestion = randomQuestion.Replace("[Random speler]", currentPlayer);

            // Update de tekst van de vraag
            QuestionLabel.Text = displayedQuestion;
        }
    }

    // Database-model voor een vraag
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
