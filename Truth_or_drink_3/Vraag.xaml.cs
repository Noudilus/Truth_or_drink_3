using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Truth_or_drink_3
{
    public partial class Vraag : ContentPage
    {
        // Lijst met vragen met sterrenwaardering
        private readonly List<(string Question, int Stars)> _questions = new()
        {
            // 1 ster
            ("[Random speler], wat is het laatste dat je hebt gegoogled?", 1),
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

        // Lijst met spelers
        private readonly List<string> _players = new()
        {
            "Noud", "Eva", "Ricardo", "Dewi", "Rimke", "Damon", "Indy"
        };

        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();

        public Vraag()
        {
            InitializeComponent();
        }

        // Methode die wordt uitgevoerd bij een tik op het scherm
        private void OnPageTapped(object sender, EventArgs e)
        {
            // Controleer of een moeilijkheidsgraad is geselecteerd
            if (StarPicker.SelectedIndex < 0)
            {
                // Toon een melding dat de gebruiker een moeilijkheidsgraad moet selecteren
                DisplayAlert("Let op", "Selecteer een moeilijkheidsgraad voordat je een vraag kunt krijgen.", "OK");
                return;
            }

            // Haal de geselecteerde moeilijkheidsgraad op
            int selectedStars = StarPicker.SelectedIndex + 1;

            // Filter vragen op basis van sterren
            var filteredQuestions = _questions.FindAll(q => q.Stars == selectedStars);

            if (filteredQuestions.Count == 0)
            {
                // Geen vragen beschikbaar voor de geselecteerde sterrenwaardering
                QuestionLabel.Text = "Geen vragen beschikbaar voor deze moeilijkheidsgraad.";
                return;
            }

            // Kies een willekeurige vraag uit de gefilterde vragen
            var (randomQuestion, _) = filteredQuestions[_random.Next(filteredQuestions.Count)];

            // Haal de huidige speler op
            string currentPlayer = _players[_currentPlayerIndex];

            // Vervang [Random speler] met de geselecteerde speler
            string displayedQuestion = randomQuestion.Replace("[Random speler]", currentPlayer);

            // Update de vraagtekst
            QuestionLabel.Text = displayedQuestion;

            // Update index voor de volgende speler
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        }

        // Eventhandler voor verandering in de sterren-Picker
        private void OnStarPickerChanged(object sender, EventArgs e)
        {
            if (sender is Picker picker)
            {
                int selectedIndex = picker.SelectedIndex;
                if (selectedIndex >= 0 && selectedIndex < 5) // Geldige index
                {
                    // Filter vragen op basis van sterren
                    int selectedStars = selectedIndex + 1;
                    var filteredQuestions = _questions.FindAll(q => q.Stars == selectedStars);

                    if (filteredQuestions.Count > 0)
                    {
                        var (randomQuestion, _) = filteredQuestions[_random.Next(filteredQuestions.Count)];
                        string currentPlayer = _players[_currentPlayerIndex];
                        string displayedQuestion = randomQuestion.Replace("[Random speler]", currentPlayer);

                        // Update vraagtekst
                        QuestionLabel.Text = displayedQuestion;

                        // Update index voor volgende speler
                        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
                    }
                    else
                    {
                        QuestionLabel.Text = "Geen vragen beschikbaar voor deze sterrenwaardering.";
                    }
                }
            }
        }
    }
}
