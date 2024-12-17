using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Truth_or_drink_3
{
    public partial class DurfPage : ContentPage
    {
        private readonly List<Player> _players;
        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();
        private int _selectedStars = 0;

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

        public DurfPage(List<Player> players)
        {
            InitializeComponent();
            _players = players;
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

            var randomQuestion = filteredQuestions[_random.Next(filteredQuestions.Count)].Text;
            var currentPlayer = _players[_currentPlayerIndex].Name;

            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

            var displayedQuestion = randomQuestion.Replace("[Random speler]", currentPlayer);
            QuestionLabel.Text = displayedQuestion;
        }

        private void OnStarPickerChanged(object sender, EventArgs e)
        {
            _selectedStars = StarPicker.SelectedIndex + 1;
        }
    }
}
