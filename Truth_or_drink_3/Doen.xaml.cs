using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Truth_or_drink_3
{
    public partial class Doen : ContentPage
    {
        // Lijst met vragen
        private readonly List<string> _questions = new()
        {
            "[Random speler], doe een gek geluid na dat je als kind vaak maakte.",
            "[Random speler], probeer te lopen alsof je op de maan bent voor de komende 3 rondes.",
            "[Random speler], spreek de komende ronde alsof je een robot bent.",
            "[Random speler], imiteer je favoriete dier totdat iemand raadt wat het is.",
            "[Random speler], zing een fragment van je favoriete liedje zonder de tekst te gebruiken.",
            "[Random speler], dans alsof je een beroemdheid bent op de rode loper.",
            "[Random speler], doe alsof je een nieuwslezer bent en lees een krantenkop voor in een dramatische toon.",
            "[Random speler], doe alsof je een vlogger bent en introduceer de groep alsof ze je fans zijn.",
            "[Random speler], geef iemand in de groep een (goede) bijnaam op basis van hun uiterlijk of gedrag.",
            "[Random speler], stel een onmogelijke rekensom aan iemand en doe alsof je echt het antwoord verwacht.",
            "[Random speler], doe een perfecte imitatie van iemand anders in deze kamer.",
            "[Random speler], voer een nep telefoongesprek alsof je boos bent op de persoon aan de andere kant.",
            "[Random speler], spreek alsof je een geheim moet vertellen, maar zeg alleen onzin.",
            "[Random speler], doe alsof je een professionele danser bent en geef ons een korte show.",
            "[Random speler], neem een gekke selfie met iedereen in de kamer.",
            "[Random speler], draag een voorwerp uit de kamer alsof het een kroontje is en je een koning(in) bent.",
            "[Random speler], drink een glas water alsof je in een woestijn hebt overleefd.",
            "[Random speler], dans alsof je in een muziekvideoclip speelt.",
            "[Random speler], probeer zo goed mogelijk een bekende persoon na te doen zonder de naam te noemen.",
            "[Random speler], verzin een dramatische liefdesscène met iemand anders in de groep.",
            "[Random speler], imiteer de geluiden van drie verschillende dieren zonder te zeggen welke ze zijn.",
            "[Random speler], doe alsof je een superheld bent en introduceer jezelf en je krachten.",
            "[Random speler], doe alsof je een geheim agent bent die instructies geeft voor een missie.",
            "[Random speler], maak een hoed van een alledaags voorwerp en draag het voor de komende 3 rondes.",
            "[Random speler], fluister alles wat je zegt voor de komende ronde.",
            "[Random speler], maak een rare dansbeweging en laat iemand anders hem nadoen.",
            "[Random speler], vertel een verhaal, maar elke zin moet beginnen met dezelfde letter.",
            "[Random speler], voer een gesprek met een plant in de kamer alsof het een goede vriend is.",
            "[Random speler], maak een geluidsopname waarin je iemand overtuigt om een absurd product te kopen.",
            "[Random speler], doe alsof je een goochelaar bent en laat een 'truc' zien.",
            "[Random speler], neem een vreemde pose aan en blijf staan tot de volgende speler aan de beurt is.",
            "[Random speler], loop alsof je op een catwalk bent voor de komende twee rondes.",
            "[Random speler], doe alsof je een beroemde chef-kok bent en beschrijf een gerecht op een rare manier.",
            "[Random speler], maak een dans met alleen je handen.",
            "[Random speler], vertel een mop, maar doe alsof het een levensles is.",
            "[Random speler], maak een geluid alsof je een monster bent.",
            "[Random speler], introduceer jezelf alsof je een historisch figuur bent.",
            "[Random speler], spreek alsof je een wetenschapper bent die een doorbraak heeft ontdekt.",
            "[Random speler], zing een kinderliedje alsof het een opera is.",
            "[Random speler], doe alsof je probeert een mug in de kamer te vangen.",
            "[Random speler], gebruik alleen gebarentaal om de volgende vraag te beantwoorden.",
            "[Random speler], vertel een sprookje in 30 seconden met je eigen draai eraan.",
            "[Random speler], imiteer een huisdier dat je ooit hebt gehad of graag zou willen hebben.",
            "[Random speler], draag een vreemd voorwerp alsof het een hoed is voor de volgende ronde.",
            "[Random speler], loop alsof je voeten aan elkaar geplakt zijn.",
            "[Random speler], maak geluiden alsof je in een horrorfilm zit.",
            "[Random speler], stel je voor dat je een nieuwe sport uitvindt en laat ons zien hoe het werkt.",
            "[Random speler], praat alsof je een alien bent die net op aarde is geland.",
            "[Random speler], doe alsof je een beroemd atleet bent en geef een overwinningstoespraak.",
            "[Random speler], bedenk een nieuwe manier om hallo te zeggen en laat het zien.",
            "[Random speler], draai in een cirkel terwijl je je volgende beurt speelt.",
            "[Random speler], geef een weerbericht met een totaal verzonnen situatie.",
            "[Random speler], voer een dramatische interpretatie op van het openen van een deur.",
            "[Random speler], ga naar de andere kant van de kamer alsof je een geheim moet afleveren.",
            "[Random speler], praat alsof je een piraat bent voor de volgende twee rondes.",
            "[Random speler], speel een gitaar zonder dat er een echte gitaar is.",
            "[Random speler], maak een reclame voor een belachelijk product en verkoop het aan de groep.",
            "[Random speler], doe alsof je een beroemd schilder bent en schets iets met je vinger in de lucht.",
            "[Random speler], gebruik een willekeurig voorwerp alsof het je mobiele telefoon is en voer een gesprek.",
            "[Random speler], dans zonder je voeten te gebruiken.",
            "[Random speler], doe alsof je de presentator bent van een kinderprogramma.",
            "[Random speler], maak een nieuwe handshake met iemand anders in de groep.",
            "[Random speler], stel jezelf voor alsof je in een datingshow zit.",
            "[Random speler], voer een dramatische scène op waarin je afscheid neemt van iemand.",
            "[Random speler], fluister de tekst van een bekend lied alsof het een groot geheim is.",
            "[Random speler], creëer een kort toneelstuk van 10 seconden met een willekeurig voorwerp.",
            "[Random speler], doe alsof je net een prijs hebt gewonnen en bedank iedereen.",
            "[Random speler], loop alsof je door een modderig veld loopt.",
            "[Random speler], doe alsof je net hebt ontdekt dat je superkrachten hebt.",
            "[Random speler], imiteer een bekende tekenfilmfiguur.",
            "[Random speler], doe alsof je een ninja bent en laat je moves zien.",
            "[Random speler], vertel een verhaal met alleen geluidseffecten.",
            "[Random speler], maak een vliegtuig van papier en laat zien hoe ver het vliegt.",
            "[Random speler], dans alsof je op ijs staat.",
            "[Random speler], doe alsof je een spion bent die betrapt is.",
            "[Random speler], spreek alsof je een strenge leraar bent.",
            "[Random speler], speel een telefoongesprek na waarin je ruzie maakt met je provider.",
            "[Random speler], maak een gezicht alsof je een citroen eet.",
            "[Random speler], fluister alles wat je zegt totdat je weer aan de beurt bent.",
            "[Random speler], vertel een sprookje, maar vervang alle namen door fruitsoorten.",
            "[Random speler], doe alsof je een standbeeld bent voor de volgende ronde.",
            "[Random speler], imiteer een vogel en probeer te vliegen.",
            "[Random speler], geef iemand in de groep een hilarische bijnaam.",
            "[Random speler], introduceer een denkbeeldig huisdier aan de groep.",
            "[Random speler], vertel een mop terwijl je moet lachen.",
            "[Random speler], speel een scène na uit je favoriete film.",
            "[Random speler], verzin een absurde theorie over waarom de lucht blauw is.",
            "[Random speler], spreek alsof je een goeroe bent die wijsheid deelt.",
            "[Random speler], ga door de kamer alsof je je schoen kwijt bent.",
            "[Random speler], doe alsof je een spelshow host en stel een vraag.",
            "[Random speler], imiteer de persoon rechts van je.",
            "[Random speler], doe een armworstelwedstrijd met iemand anders in de groep.",
            "[Random speler], blaas een ballon op (of doe alsof) totdat hij knapt.",
            "[Random speler], doe alsof je een ouder bent die iemand straft.",
            "[Random speler], kruip door de kamer alsof je een baby bent.",
            "[Random speler], doe alsof je een danswedstrijd wint.",
            "[Random speler], doe alsof je een beroemd schilder bent en schilder in de lucht.",
            "[Random speler], loop alsof je op hoge hakken staat.",
            "[Random speler], geef een liefdesverklaring aan een willekeurig object in de kamer.",
            "[Random speler], doe alsof je een karaoke-wedstrijd wint.",
            "[Random speler], vertel een spannend verhaal over hoe je vandaag bent aangekomen.",
            "[Random speler], doe alsof je een vechtscène uit een film nadoet.",
            "[Random speler], leg uit hoe een onbekend voorwerp werkt alsof je een expert bent.",
            "[Random speler], zing een bekend lied, maar met verkeerde woorden.",
            "[Random speler], loop alsof je een model bent op een catwalk.",
            "[Random speler], vertel een geheim, maar het mag alleen klinken alsof het belangrijk is.",
            "[Random speler], speel een drumsolo op een denkbeeldige drumset.",
            "[Random speler], lach alsof je net een geweldige grap hebt gehoord.",
            "[Random speler], imiteer een sportcommentator terwijl je een willekeurige actie beschrijft.",
            "[Random speler], maak een dans waarbij je alleen je hoofd mag bewegen.",
            "[Random speler], stel je voor dat je de president bent en geef een speech.",
            "[Random speler], zing alsof je een musicalster bent.",
            "[Random speler], doe alsof je een hond bent die op zoek is naar iets.",
            "[Random speler], beweeg door de kamer alsof je een spook bent.",
            "[Random speler], vertel een mop zonder te glimlachen.",
            "[Random speler], speel een scène na waarin je een geheime boodschap moet doorgeven.",
            "[Random speler], doe alsof je een beroemd chef-kok bent die een gerecht serveert.",
            "[Random speler], vertel een grappig verhaal dat niet waar is.",
            "[Random speler], imiteer een robot die kapot gaat.",
            "[Random speler], praat alsof je een verkoper bent die iets absurds verkoopt.",
            "[Random speler], doe alsof je vastzit in slow motion.",
            "[Random speler], voer een denkbeeldige choreografie uit alsof je in een muziekvideo zit.",
            "[Random speler], doe alsof je een beroemde zanger bent tijdens een interview.",
            "[Random speler], loop door de kamer alsof je een geheim probeert te bewaren.",
            "[Random speler], maak een geluid alsof je een auto bent.",
            "[Random speler], praat alsof je een piraat bent voor de rest van de ronde.",
            "[Random speler], schrijf je naam in de lucht met je neus.",
            "[Random speler], introduceer de groep alsof je een stand-upcomedian bent.",
            "[Random speler], doe alsof je een acteur bent die auditie doet voor een drama.",
            "[Random speler], probeer een denkbeeldige vlieg te vangen.",
            "[Random speler], imiteer het geluid van regen.",
            "[Random speler], doe alsof je je favoriete sport beoefent.",
            "[Random speler], vertel een verhaal waarbij elk woord met dezelfde letter begint.",
            "[Random speler], voer een nep-toverspreuk uit alsof je een tovenaar bent.",
            "[Random speler], spreek alsof je een cartoonfiguur bent.",
            "[Random speler], maak een dramatisch gebaar alsof je net een belangrijke ontdekking hebt gedaan.",
            "[Random speler], probeer een grap te vertellen in een andere taal (of doe alsof).",
            "[Random speler], beweeg alsof je in een onzichtbare doos zit.",
            "[Random speler], doe alsof je een boze klant bent die klaagt bij de klantenservice."

        };

        // Lijst met spelers
        private readonly List<string> _players = new()
        {
            "Noud", "Eva", "Ricardo", "Dewi", "Rimke", "Damon", "Indy"
        };

        // Index van de huidige speler
        private int _currentPlayerIndex = 0;

        // Willekeurige generator
        private readonly Random _random = new();

        public Doen()
        {
            InitializeComponent();
        }

        // Methode die wordt uitgevoerd bij een tik op het scherm
        private void OnPageTapped(object sender, EventArgs e)
        {
            // Kies een willekeurige vraag
            string randomQuestion = _questions[_random.Next(_questions.Count)];

            // Haal de volgende speler op
            string currentPlayer = _players[_currentPlayerIndex];

            // Update de index naar de volgende speler (circulair)
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

            // Vervang [Random speler] met de geselecteerde speler
            string displayedQuestion = randomQuestion.Replace("[Random speler]", currentPlayer);

            // Update de tekst van de vraag
            QuestionLabel.Text = displayedQuestion;
        }
    }
}
