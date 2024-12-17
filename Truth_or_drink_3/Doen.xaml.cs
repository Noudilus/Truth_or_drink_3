using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Truth_or_drink_3
{
    public partial class Doen : ContentPage
    {
        private readonly List<Player> _players;
        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();
        private int _selectedStars = 0;

        private readonly List<(string Text, int Stars)> _questions = new()
        {
            // 1 ster vragen
            ("[Random speler], doe een gek geluid na dat je als kind vaak maakte.", 1),
            ("[Random speler], neem een gekke pose aan en blijf staan tot de volgende speler aan de beurt is.", 1),
            ("[Random speler], introduceer een denkbeeldig huisdier aan de groep.", 1),
            ("[Random speler], vertel een mop terwijl je moet lachen.", 1),
            ("[Random speler], kruip door de kamer alsof je een baby bent.", 1),
            ("[Random speler], lach alsof je net een geweldige grap hebt gehoord.", 1),
            ("[Random speler], schrijf je naam in de lucht met je neus.", 1),
            ("[Random speler], fluister alles wat je zegt totdat je weer aan de beurt bent.", 1),

            // 2 sterren vragen
            ("[Random speler], doe alsof je een professionele danser bent en geef ons een korte show.", 2),
            ("[Random speler], draag een vreemd voorwerp alsof het een hoed is voor de volgende ronde.", 2),
            ("[Random speler], zing een kinderliedje alsof het een opera is.", 2),
            ("[Random speler], introduceer jezelf alsof je in een datingshow zit.", 2),
            ("[Random speler], loop alsof je door een modderig veld loopt.", 2),
            ("[Random speler], vertel een geheim, maar het mag alleen klinken alsof het belangrijk is.", 2),
            ("[Random speler], beweeg alsof je in een onzichtbare doos zit.", 2),

            // 3 sterren vragen
            ("[Random speler], doe alsof je een nieuwslezer bent en lees een krantenkop voor in een dramatische toon.", 3),
            ("[Random speler], vertel een verhaal, maar elke zin moet beginnen met dezelfde letter.", 3),
            ("[Random speler], vertel een mop zonder te glimlachen.", 3),
            ("[Random speler], voer een denkbeeldige choreografie uit alsof je in een muziekvideo zit.", 3),
            ("[Random speler], doe alsof je een tovenaar bent en voer een nep-toverspreuk uit.", 3),
            ("[Random speler], maak een geluidsopname waarin je iemand overtuigt om een absurd product te kopen.", 3),
            ("[Random speler], loop door de kamer alsof je een geheim probeert te bewaren.", 3),

            // 4 sterren vragen
            ("[Random speler], speel een dramatische scène na waarin je afscheid neemt van iemand.", 4),
            ("[Random speler], vertel een sprookje, maar vervang alle namen door fruitsoorten.", 4),
            ("[Random speler], maak een vliegtuig van papier en laat zien hoe ver het vliegt.", 4),
            ("[Random speler], vertel een spannend verhaal over hoe je vandaag bent aangekomen.", 4),
            ("[Random speler], voer een nep-toverspreuk uit alsof je een tovenaar bent.", 4),
            ("[Random speler], doe alsof je een acteur bent die auditie doet voor een drama.", 4),
            ("[Random speler], maak een dans waarbij je alleen je hoofd mag bewegen.", 4),

            // 5 sterren vragen
            ("[Random speler], verzin een absurde theorie over waarom de lucht blauw is.", 5),
            ("[Random speler], vertel een verhaal waarbij elk woord met dezelfde letter begint.", 5),
            ("[Random speler], doe alsof je net een prijs hebt gewonnen en bedank iedereen.", 5),
            ("[Random speler], speel een vechtscène uit een film na.", 5),
            ("[Random speler], vertel een sprookje in 30 seconden met je eigen draai eraan.", 5),
            ("[Random speler], vertel een mop, maar doe alsof het een levensles is.", 5),
            ("[Random speler], praat alsof je een alien bent die net op aarde is geland.", 5)
        };

        public Doen(List<Player> players)
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
            if (filteredQuestions.Count == 0)
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
