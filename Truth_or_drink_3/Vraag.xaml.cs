using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Truth_or_drink_3
{
    public partial class Vraag : ContentPage
    {
        // Lijst met vragen
        private readonly List<string> _questions = new()
        {   
            //
            "[Random speler], wat is het laatste dat je hebt gegoogled?",
            "[Random speler], wat is je grootste geheim?",
            "[Random speler], wie vind je het knapst in deze kamer?",
            "[Random speler], wat is je meest beschamende moment?",
            "[Random speler], wat is iets waar je trots op bent?",
            "[Random speler], wat is de gekste droom die je hebt gehad?",
            "[Random speler], wat is de engste droom die je ooit hebt gehad",
            "[Random speler], als het niet illegaal was, wie zou je vermoorden",
            "[Random speler], waar heb je de meeste spijt van",
            "[Random speler], wat is het meest kinderachtige dat je nogsteeds doet",
            "[Random speler], als je voor één dag een ander geslacht was, wat zou je doen?",
            "[Random speler], open een zakje chips/snoepjes zonder je handen te gebruiken",
            "[Random speler], wat is iets dat je met je vrienden doet, dat je niet met je partner zou willen doen?",
            "[Random speler], wie zou je bellen als je in de gevangenis zit?",
            "[Random speler], wat is het vreemdste dat je ooit hebt gedaan om iemand te imponeren",
            "[Random speler], als je één geheim van iemand in deze groep moest delen, welk geheim zou dat dan zijn",
            "[Random speler], wie van ons zou je het minst vertrouwen om je levensgeheimen aan te vertellen",
            "[Random speler], wat is de meest gênante situatie waarin je ooit in de buurt van iemand hier was",
            "[Random speler], als je iemand uit deze groep moest kussen voor een miljoen euro, wie zou je kiezen",
            "[Random speler], wat is het ergste dat je ooit tegen je ouders hebt gelogen",
            "[Random speler], wie in deze kamer zou het snelst een geheim verklappen",
            "[Random speler], wat is het grootste misverstand dat je ooit hebt gehad met iemand hier",
            "[Random speler], heb je ooit iemand in deze groep stiekem afgeschreven, en waarom",
            "[Random speler], wat is het meest ongemakkelijke dat je ooit hebt gehoord tijdens een gesprek met iemand uit deze groep",
            "[Random speler], wie zou je kiezen om mee naar een onbewoond eiland te gaan",
            "[Random speler], wat is de vreemdste gewoonte die je hebt, die niemand hier weet",
            "[Random speler], als je ons moest beschrijven met één woord, wat zou dat dan zijn",
            "[Random speler], wie zou je niet in vertrouwen nemen met je telefoon als je moest gaan slapen",
            "[Random speler], wat is het grootste risico dat je ooit hebt genomen dat goed uitpakte",
            "[Random speler], wat is het gekste dat je ooit hebt gedaan op een feestje zonder dat iemand het wist",
            "[Random speler], wie zou je niet vertrouwen met je Netflix-account",
            "[Random speler], wat is het ergste dat je ooit tegen iemand hebt gezegd in een ruzie",
            "[Random speler], wat is het meest gênante dat je ooit op social media hebt gepost?",
            "[Random speler], als je een superkracht zou kunnen kiezen, welke zou dat zijn en waarom?",
            "[Random speler], wie van deze groep zou je het eerst verlaten in een zombie-apocalypse?",
            "[Random speler], wat is het meest romantische gebaar dat je ooit hebt gedaan?",
            "[Random speler], wat is het raarste dat je ooit in de koelkast hebt gevonden?",
            "[Random speler], als je een beroemdheid zou kunnen daten, wie zou dat zijn?",
            "[Random speler], wat is het meest nutteloze dat je ooit hebt gekocht?",
            "[Random speler], wie in deze kamer zou het slechtst een geheim kunnen bewaren?",
            "[Random speler], wat is je grootste guilty pleasure op Netflix?",
            "[Random speler], wat is het meest gênante dat ooit in je browsergeschiedenis heeft gestaan?",
            "[Random speler], wie zou je kiezen om je wingman/wingwoman te zijn tijdens een avondje uit?",
            "[Random speler], wat is je meest rare gewoonte waar je je soms voor schaamt?",
            "[Random speler], als je een crimineel moest worden, wat zou je misdaad zijn?",
            "[Random speler], wat is het vreemdste compliment dat je ooit hebt gekregen?",
            "[Random speler], wie in deze kamer zou het snelst zijn baan opzeggen voor een miljoen euro?",
            "[Random speler], wat is de grootste leugen die je ooit hebt verteld aan een vriend(in)?",
            "[Random speler], wat is het meest gênante dat je ooit in een openbare ruimte hebt gedaan?",
            "[Random speler], als je een realityshow mocht starten, wie van ons zou je erin casten?",
            "[Random speler], wat is het vreemdste eten dat je ooit hebt geprobeerd?",
            "[Random speler], als je één ding kon veranderen aan jezelf, wat zou dat zijn?",
            "[Random speler], wie in deze groep zou je kiezen om een geheime missie mee uit te voeren?",
            "[Random speler], wat is het stomste advies dat je ooit hebt gegeven, maar dat werkte?",
            "[Random speler], als je een tattoo zou moeten nemen, wat zou het dan zijn en waarom?",
            "[Random speler], wie in deze groep zou je het minst snel laten babysitten voor je (toekomstige) kinderen?",
            "[Random speler], wat is het raarste dat iemand ooit tegen je heeft gezegd?",
            "[Random speler], als je één lied moest zingen op karaoke, welk lied zou je kiezen?",
            "[Random speler], wie in deze kamer zou het meest waarschijnlijk te laat komen op zijn/haar eigen bruiloft?",
            "[Random speler], wat is het meest gênante dat je ooit in je zak hebt gevonden?",
            "[Random speler], wie in deze groep zou het waarschijnlijkst verdwalen tijdens een reis?",
            "[Random speler], wat is het meest impulsieve dat je ooit hebt gedaan?",
            "[Random speler], als je morgen wakker zou worden in het lichaam van iemand hier, wie zou je kiezen?",
            "[Random speler], wat is het meest onverwachte dat iemand ooit over jou heeft onthuld?",
            "[Random speler], wie van ons zou volgens jou het beste een geheim agent kunnen zijn?",
            "[Random speler], wat is het slechtste cadeau dat je ooit hebt gegeven?",
            "[Random speler], als je een film over je leven zou maken, wie in deze groep zou je casten als de slechterik?",
            "[Random speler], als je één slechte gewoonte moest opgeven, welke zou dat zijn?",
            "[Random speler], wat is het meest gênante dat je ooit in een groepschat hebt gestuurd?",
            "[Random speler], wie van deze groep zou je bellen als je autopech had?",
            "[Random speler], wat is het ergste dat je ooit hebt gedaan om onder een verplichting uit te komen?",
            "[Random speler], wat is het meest illegale dat je ooit hebt gedaan?",
            "[Random speler], als je een wereldrecord kon breken, welke zou dat zijn?",
            "[Random speler], wie van deze groep zou je nooit met je huisdier vertrouwen?",
            "[Random speler], wat is het vreemdste wat je ooit in iemands badkamer hebt gevonden?",
            "[Random speler], wat is je meest gênante kindertijdverhaal?",
            "[Random speler], als je voor één dag een dier kon zijn, welk dier zou je kiezen?",
            "[Random speler], wat is het vreemdste bijgeloof waar je ooit in hebt geloofd?",
            "[Random speler], wie van ons zou het beste een geheim dubbel leven kunnen leiden?",
            "[Random speler], wat is het slechtste advies dat je ooit hebt gekregen?",
            "[Random speler], wie zou je het minst vertrouwen om je mee te helpen verhuizen?",
            "[Random speler], wat is de gekste plek waar je ooit in slaap bent gevallen?",
            "[Random speler], wat is het meest onverwachte cadeau dat je ooit hebt gekregen?",
            "[Random speler], als je één dag onzichtbaar kon zijn, wat zou je doen?",
            "[Random speler], wat is je grootste modeflater ooit?",
            "[Random speler], wie in deze groep zou je nooit om advies vragen over liefde?",
            "[Random speler], wat is het slechtste excuus dat je ooit hebt gebruikt?",
            "[Random speler], wie zou het meeste geld kunnen uitgeven tijdens een dagje shoppen?",
            "[Random speler], wat is het stomste wat je ooit hebt gedaan toen je boos was?",
            "[Random speler], wat is het grootste misverstand dat iemand ooit over jou heeft gehad?",
            "[Random speler], wie in deze groep zou het meest waarschijnlijk een wereldreis maken zonder iets te plannen?",
            "[Random speler], wat is de raarste bijnaam die je ooit hebt gehad?",
            "[Random speler], wat is het meest impulsieve dat je ooit online hebt gekocht?",
            "[Random speler], wie in deze groep zou het meest waarschijnlijk per ongeluk iets waardevols breken?",
            "[Random speler], wat is het stomste dat je ooit hebt gezegd tijdens een belangrijke afspraak?",
            "[Random speler], als je een fictieve wereld kon bezoeken, welke zou dat zijn?",
            "[Random speler], wie in deze groep zou je meest irritante huisgenoot zijn?",
            "[Random speler], wat is het raarste wat je ooit hebt gegeten en lekker vond?",
            "[Random speler], als je een superheldnaam moest kiezen, wat zou die zijn?",
            "[Random speler], wat is het meest gênante dat je ouders ooit tegen iemand hebben gezegd?",
            "[Random speler], wie in deze groep zou het beste een schurkenrol in een film kunnen spelen?",
            "[Random speler], wat is de vreemdste droom die je ooit hebt gehad en je nog steeds herinnert?",
            "[Random speler], wie in deze groep zou het slechtst zijn in een geheime missie?",
            "[Random speler], wat is je grootste blunder op het werk of op school?",
            "[Random speler], wat is het vreemdste geluid dat je ooit per ongeluk hebt gemaakt in het openbaar?",
            "[Random speler], als je één vraag kon stellen en zeker het antwoord zou krijgen, welke vraag zou dat zijn?",
            "[Random speler], wie van deze groep zou als eerste falen in een kookwedstrijd?",
            "[Random speler], wat is de grootste angst die je als kind had?",
            "[Random speler], wie in deze groep zou het langst zonder sociale media kunnen leven?",
            "[Random speler], wat is de meest gênante bijnaam die iemand ooit voor je heeft gebruikt?",
            "[Random speler], wat is de meest gênante bijnaam die iemaWAnd ooit voor je heeft gebruikt?",
            "[Random speler], wie in deze groep zou je het eerst verliezen in een quiz?",
            "[Random speler], als je een lied moest kiezen dat je leven beschrijft, welk lied zou dat zijn?",
            "[Random speler], wat is het gekste verhaal dat je ooit hebt verzonnen om ergens onderuit te komen?",
            "[Random speler], wie in deze groep zou het meest waarschijnlijk een geheime aanbidder hebben?",
            "[Random speler], wat is het stomste dat je ooit hebt gedaan toen je dacht dat niemand keek?",
            "[Random speler], wie in deze groep zou de grappigste ouder zijn?",
            "[Random speler], wat is het meest gênante dat je ooit hebt gezegd tegen iemand die je leuk vond?"
        };

        // Lijst met spelers
        private readonly List<string> _players = new()
        {
            "Noud", "Eva", "Ricardo", "Dewi", "Rimke", "Damon", "Youp"
        };

        // Willekeurige generator
        private readonly Random _random = new();

        public Vraag()
        {
            InitializeComponent();
        }

        // Methode die wordt uitgevoerd bij een tik op het scherm
        private void OnPageTapped(object sender, EventArgs e)
        {
            // Kies een willekeurige vraag
            string randomQuestion = _questions[_random.Next(_questions.Count)];

            // Kies een willekeurige speler
            string randomPlayer = _players[_random.Next(_players.Count)];

            // Vervang [Random speler] met de geselecteerde speler
            string displayedQuestion = randomQuestion.Replace("[Random speler]", randomPlayer);

            // Update de tekst van de vraag
            QuestionLabel.Text = displayedQuestion;
        }
    }
}
