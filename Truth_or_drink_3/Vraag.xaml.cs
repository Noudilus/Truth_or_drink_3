using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Truth_or_drink_3
{
    public partial class Vraag : ContentPage
    {
        private readonly List<Player> _players; // Dynamische spelerslijst
        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();
        private int _selectedStars = 0;

        private readonly List<(string Text, int Stars)> _questions = new()
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

        public Vraag(List<Player> players)
        {
            InitializeComponent();
            _players = players; // Dynamische lijst van spelers instellen
        }

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

            // Willekeurige vraag kiezen
            var randomQuestion = filteredQuestions[_random.Next(filteredQuestions.Count)].Text;
            var currentPlayer = _players[_currentPlayerIndex].Name;

            // Volgende speler selecteren
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

            // Vervang [Random speler] door huidige speler en toon vraag
            var displayedQuestion = randomQuestion.Replace("[Random speler]", currentPlayer);
            QuestionLabel.Text = displayedQuestion;
        }

        private void OnStarPickerChanged(object sender, EventArgs e)
        {
            _selectedStars = StarPicker.SelectedIndex + 1;
        }
    }
}
