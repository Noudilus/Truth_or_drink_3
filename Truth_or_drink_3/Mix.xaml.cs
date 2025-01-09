using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Truth_or_drink_3
{
    public partial class Mix : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<Player> _players;
        private int _currentPlayerIndex = 0;
        private readonly Random _random = new();
        private int _selectedStars = 0;
        private bool _isGyroscopeStill = true;
        private bool _isGyroscopeRunning = false;

        // Vragen georganiseerd op basis van sterren
        private readonly List<(string Text, int Stars)> _questions = new()
        {
            // 1 ster vragen
            ("[Random speler], durf je de persoon naast je een compliment te geven?", 1),
            ("[Random speler], durf je te zeggen wat je zou doen als je miljonair was?", 1),
            ("[Random speler], durf je een slechte grap te vertellen?", 1),
            ("[Random speler], durf je een schattig geluid na te doen van een dier?", 1),
            ("[Random speler], durf je een rare smoes te verzinnen en te delen?", 1),
            ("[Random speler], durf je te vragen wat iedereen echt van je denkt?", 1),
            ("[Random speler], doe een gek geluid na dat je als kind vaak maakte.", 1),
            ("[Random speler], neem een gekke pose aan en blijf staan tot de volgende speler aan de beurt is.", 1),
            ("[Random speler], introduceer een denkbeeldig huisdier aan de groep.", 1),
            ("[Random speler], vertel een mop terwijl je moet lachen.", 1),
            ("[Random speler], kruip door de kamer alsof je een baby bent.", 1),
            ("[Random speler], lach alsof je net een geweldige grap hebt gehoord.", 1),
            ("[Random speler], schrijf je naam in de lucht met je neus.", 1),
            ("[Random speler], fluister alles wat je zegt totdat je weer aan de beurt bent.", 1),
            ("[Random speler], wat is het laatste dat je hebt gegoogled?", 1),
            ("[Random speler], wat is iets waar je trots op bent?", 1),
            ("[Random speler], wat is de gekste droom die je hebt gehad?", 1),
            ("[Random speler], wie zou je bellen als je in de gevangenis zit?", 1),
            ("[Random speler], als je een superkracht zou kunnen kiezen, welke zou dat zijn?", 1),

            // 2 sterren vragen
            ("[Random speler], durf je iets te eten dat iemand anders kiest?", 2),
            ("[Random speler], durf je een dansje te doen op een willekeurig lied dat iemand kiest?", 2),
            ("[Random speler], durf je een geheim over je dag te vertellen?", 2),
            ("[Random speler], durf je een slechte review over iets te verzinnen?", 2),
            ("[Random speler], durf je iemand een compliment te geven die je normaal negeert?", 2),
            ("[Random speler], durf je je meest recente appgesprek te openen en voor te lezen?", 2),
            ("[Random speler], doe alsof je een professionele danser bent en geef ons een korte show.", 2),
            ("[Random speler], draag een vreemd voorwerp alsof het een hoed is voor de volgende ronde.", 2),
            ("[Random speler], zing een kinderliedje alsof het een opera is.", 2),
            ("[Random speler], introduceer jezelf alsof je in een datingshow zit.", 2),
            ("[Random speler], loop alsof je door een modderig veld loopt.", 2),
            ("[Random speler], vertel een geheim, maar het mag alleen klinken alsof het belangrijk is.", 2),
            ("[Random speler], beweeg alsof je in een onzichtbare doos zit.", 2),
            ("[Random speler], wie vind je het knapst in deze kamer?", 2),
            ("[Random speler], wat is het vreemdste dat je ooit hebt gedaan om iemand te imponeren?", 2),
            ("[Random speler], wie zou je kiezen om mee naar een onbewoond eiland te gaan?", 2),
            ("[Random speler], wat is je grootste guilty pleasure op Netflix?", 2),
            ("[Random speler], wie in deze kamer zou het slechtst een geheim kunnen bewaren?", 2),

            // 3 sterren vragen
            ("[Random speler], durf je een gênant geheim te delen dat niemand weet?", 3),
            ("[Random speler], durf je een schattige bijnaam te verzinnen voor jezelf en deze te gebruiken?", 3),
            ("[Random speler], durf je een onbekend nummer te bellen en te vragen naar hun favoriete kleur?", 3),
            ("[Random speler], durf je een geheim over je kindertijd te vertellen?", 3),
            ("[Random speler], durf je je laatst beluisterde Spotify-nummer te laten zien?", 3),
            ("[Random speler], durf je iemand te vertellen wat je eerste indruk van hen was?", 3),
            ("[Random speler], doe alsof je een nieuwslezer bent en lees een krantenkop voor in een dramatische toon.", 3),
            ("[Random speler], vertel een verhaal, maar elke zin moet beginnen met dezelfde letter.", 3),
            ("[Random speler], vertel een mop zonder te glimlachen.", 3),
            ("[Random speler], voer een denkbeeldige choreografie uit alsof je in een muziekvideo zit.", 3),
            ("[Random speler], doe alsof je een tovenaar bent en voer een nep-toverspreuk uit.", 3),
            ("[Random speler], maak een geluidsopname waarin je iemand overtuigt om een absurd product te kopen.", 3),
            ("[Random speler], loop door de kamer alsof je een geheim probeert te bewaren.", 3),
            ("[Random speler], wat is je meest beschamende moment?", 3),
            ("[Random speler], wat is je grootste geheim?", 3),
            ("[Random speler], wat is het vreemdste compliment dat je ooit hebt gekregen?", 3),
            ("[Random speler], als je een realityshow mocht starten, wie van ons zou je erin casten?", 3),
            ("[Random speler], wat is het meest gênante dat je ooit op social media hebt gepost?", 3),

            // 4 sterren vragen
            ("[Random speler], durf je een geheim te prijs te geven over je kindertijd?", 4),
            ("[Random speler], durf je de eerste foto op je telefoon te laten zien?", 4),
            ("[Random speler], durf je een gênant moment uit je verleden te herbeleven?", 4),
            ("[Random speler], durf je je telefoon aan de persoon links van je te geven voor 1 minuut?", 4),
            ("[Random speler], durf je een TikTok-video te maken en te posten?", 4),
            ("[Random speler], durf je een gênante bijnaam te geven aan iemand anders?", 4),
            ("[Random speler], speel een dramatische scène na waarin je afscheid neemt van iemand.", 4),
            ("[Random speler], vertel een sprookje, maar vervang alle namen door fruitsoorten.", 4),
            ("[Random speler], maak een vliegtuig van papier en laat zien hoe ver het vliegt.", 4),
            ("[Random speler], vertel een spannend verhaal over hoe je vandaag bent aangekomen.", 4),
            ("[Random speler], voer een nep-toverspreuk uit alsof je een tovenaar bent.", 4),
            ("[Random speler], doe alsof je een acteur bent die auditie doet voor een drama.", 4),
            ("[Random speler], maak een dans waarbij je alleen je hoofd mag bewegen.", 4),
            ("[Random speler], wat is het meest kinderachtige dat je nog steeds doet?", 4),
            ("[Random speler], wat is het raarste dat iemand ooit tegen je heeft gezegd?", 4),
            ("[Random speler], wie in deze groep zou het meest waarschijnlijk verdwalen tijdens een reis?", 4),
            ("[Random speler], wie in deze groep zou het meest waarschijnlijk een geheim dubbel leven leiden?", 4),
            ("[Random speler], wat is het grootste risico dat je ooit hebt genomen?", 4),

            // 5 sterren vragen
            ("[Random speler], durf je een bericht te sturen naar je ex?", 5),
            ("[Random speler], durf je een gênant geheim te delen dat niemand weet?", 5),
            ("[Random speler], durf je je meest gênante foto te laten zien aan iedereen?", 5),
            ("[Random speler], durf je het nummer van een willekeurige onbekende te bellen?", 5),
            ("[Random speler], durf je je meest gênante chatbericht te delen?", 5),
            ("[Random speler], wie van ons zou je kussen voor een miljoen euro?", 5),
            ("[Random speler], wat is het ergste dat je ooit tegen je ouders hebt gelogen?", 5),
            ("[Random speler], als het niet illegaal was, wie zou je vermoorden?", 5),
            ("[Random speler], als je één geheim van iemand in deze groep moest delen, welk geheim zou dat zijn?", 5),
            ("[Random speler], wat is het meest gênante dat je ooit in een groepschat hebt gestuurd?", 5),
            ("[Random speler], verzin een absurde theorie over waarom de lucht blauw is.", 5),
            ("[Random speler], vertel een verhaal waarbij elk woord met dezelfde letter begint.", 5),
            ("[Random speler], doe alsof je net een prijs hebt gewonnen en bedank iedereen.", 5),
            ("[Random speler], speel een vechtscène uit een film na.", 5),
            ("[Random speler], vertel een sprookje in 30 seconden met je eigen draai eraan.", 5),
            ("[Random speler], vertel een mop, maar doe alsof het een levensles is.", 5),
        };

        // Constructor om DatabaseService en List<Player> te accepteren
        public Mix(DatabaseService databaseService, List<Player> players)
        {
            InitializeComponent();
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _players = players ?? new List<Player>(); // Zorg ervoor dat de spelerslijst niet null is
        }

        // Haal de lijst van spelers op van de database
        private async Task LoadPlayersAsync()
        {
            _players = await _databaseService.GetPlayersAsync();
            if (_players.Count == 0)
            {
                await DisplayAlert("Fout", "Er zijn geen spelers in de database.", "OK");
            }
        }

        // Laad de spelers wanneer de pagina wordt geladen
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPlayersAsync();
        }

        // Event handler voor de Picker verandering
        private void OnStarPickerChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedDifficulty = picker.SelectedItem.ToString();
            // Stel de ster waarde in op basis van de selectie
            _selectedStars = selectedDifficulty switch
            {
                "1 Ster" => 1,
                "2 Sterren" => 2,
                "3 Sterren" => 3,
                "4 Sterren" => 4,
                "5 Sterren" => 5,
                _ => 1
            };
        }

        // Start de ronde
        private void StartButton_Clicked(object sender, EventArgs e)
        {
            if (_players.Count == 0)
            {
                DisplayAlert("Fout", "Er zijn geen spelers om een vraag aan te stellen.", "OK");
                return;
            }

            _currentPlayerIndex = _random.Next(_players.Count);
            var currentPlayer = _players[_currentPlayerIndex];

            // Filter vragen op basis van geselecteerde sterren
            var filteredQuestions = _questions.Where(q => q.Stars == _selectedStars).ToList();

            // Kies een willekeurige vraag uit de gefilterde lijst
            var randomQuestion = filteredQuestions[_random.Next(filteredQuestions.Count)];
            var questionText = randomQuestion.Text.Replace("[Random speler]", currentPlayer.Name);

            // Toon de vraag
            QuestionLabel.Text = questionText;
        }

        // Event handler voor de knop "Tik hier voor een vraag"
        private void OnPageTapped(object sender, EventArgs e)
        {
            // Hier kun je andere logica toevoegen om bijvoorbeeld de vraag te genereren
            QuestionLabel.Text = "Vraag gegenereerd!";
        }

        // Event handler voor de knop "Neem een slok"
        private void OnDrinkButtonClicked(object sender, EventArgs e)
        {
            // Voeg hier logica toe voor de "Neem een slok"-knop
            QuestionLabel.Text = "Je hebt een slok genomen!";
        }
    }
}
