using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Truth_or_drink_3
{
    public partial class Doen : ContentPage
    {
        // Lijst met spelers
        private readonly List<string> _players = new()
        {
            "Noud", "Eva", "Ricardo", "Dewi", "Rimke", "Damon", "Indy"
        };

        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();
        private SQLiteAsyncConnection _database;

        public Doen()
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
                    "[Random speler], doe een gek geluid na dat je als kind vaak maakte.",
                    "[Random speler], neem een gekke pose aan en blijf staan tot de volgende speler aan de beurt is.",
                    "[Random speler], introduceer een denkbeeldig huisdier aan de groep.",
                    "[Random speler], vertel een mop terwijl je moet lachen.",
                    "[Random speler], kruip door de kamer alsof je een baby bent.",
                    "[Random speler], lach alsof je net een geweldige grap hebt gehoord.",
                    "[Random speler], schrijf je naam in de lucht met je neus.",
                    "[Random speler], fluister alles wat je zegt totdat je weer aan de beurt bent.",
                    //2 sterren
                    "[Random speler], doe alsof je een professionele danser bent en geef ons een korte show.",
                    "[Random speler], draag een vreemd voorwerp alsof het een hoed is voor de volgende ronde.",
                    "[Random speler], zing een kinderliedje alsof het een opera is.",
                    "[Random speler], introduceer jezelf alsof je in een datingshow zit.",
                    "[Random speler], loop alsof je door een modderig veld loopt.",
                    "[Random speler], vertel een geheim, maar het mag alleen klinken alsof het belangrijk is.",
                    "[Random speler], beweeg alsof je in een onzichtbare doos zit.",
                    //3 sterren
                    "[Random speler], doe alsof je een nieuwslezer bent en lees een krantenkop voor in een dramatische toon.",
                    "[Random speler], vertel een verhaal, maar elke zin moet beginnen met dezelfde letter.",
                    "[Random speler], vertel een mop zonder te glimlachen.",
                    "[Random speler], voer een denkbeeldige choreografie uit alsof je in een muziekvideo zit.",
                    "[Random speler], doe alsof je een tovenaar bent en voer een nep-toverspreuk uit.",
                    "[Random speler], maak een geluidsopname waarin je iemand overtuigt om een absurd product te kopen.",
                    "[Random speler], loop door de kamer alsof je een geheim probeert te bewaren.",
                    //4 sterren
                    "[Random speler], speel een dramatische sc�ne na waarin je afscheid neemt van iemand.",
                    "[Random speler], vertel een sprookje, maar vervang alle namen door fruitsoorten.",
                    "[Random speler], maak een vliegtuig van papier en laat zien hoe ver het vliegt.",
                    "[Random speler], vertel een spannend verhaal over hoe je vandaag bent aangekomen.",
                    "[Random speler], voer een nep-toverspreuk uit alsof je een tovenaar bent.",
                    "[Random speler], doe alsof je een acteur bent die auditie doet voor een drama.",
                    "[Random speler], maak een dans waarbij je alleen je hoofd mag bewegen.",
                    //5 sterren
                    "[Random speler], verzin een absurde theorie over waarom de lucht blauw is.",
                    "[Random speler], vertel een verhaal waarbij elk woord met dezelfde letter begint.",
                    "[Random speler], doe alsof je net een prijs hebt gewonnen en bedank iedereen.",
                    "[Random speler], speel een vechtsc�ne uit een film na.",
                    "[Random speler], vertel een sprookje in 30 seconden met je eigen draai eraan.",
                    "[Random speler], vertel een mop, maar doe alsof het een levensles is.",
                    "[Random speler], praat alsof je een alien bent die net op aarde is geland.",
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
