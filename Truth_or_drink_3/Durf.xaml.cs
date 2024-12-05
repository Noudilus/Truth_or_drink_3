using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Truth_or_drink_3
{
    public partial class Durf : ContentPage
    {
        // Lijst met vragen
        private readonly List<string> _questions = new()
        {
            "[Random speler], durf je te bellen naar de laatste persoon in je contactenlijst?",
            "[Random speler], durf je een bericht te sturen naar je ex?",
            "[Random speler], durf je een selfie te maken en direct te posten zonder filter?",
            "[Random speler], durf je 10 minuten lang alleen in rijm te praten?",
            "[Random speler], durf je een gênant geheim te delen dat niemand weet?",
            "[Random speler], durf je de persoon naast je een compliment te geven?",
            "[Random speler], durf je een dansmove te doen die je nooit eerder hebt geprobeerd?",
            "[Random speler], durf je een hap te nemen van iets dat je normaal nooit eet?",
            "[Random speler], durf je de eerste foto op je telefoon te laten zien?",
            "[Random speler], durf je het nummer van een willekeurige onbekende te bellen?",
            "[Random speler], durf je een lelijke gezichtsexpressie te maken en vast te houden?",
            "[Random speler], durf je een TikTok-dansje na te doen, zelfs als je het niet kent?",
            "[Random speler], durf je een lied te zingen zonder de tekst te kennen?",
            "[Random speler], durf je je telefoon aan de persoon links van je te geven voor 1 minuut?",
            "[Random speler], durf je iemand een gênante vraag te stellen?",
            "[Random speler], durf je een geheim over jezelf te vertellen?",
            "[Random speler], durf je een gek geluid te maken dat niemand verwacht?",
            "[Random speler], durf je een compliment te geven aan iemand die je normaal negeert?",
            "[Random speler], durf je een denkbeeldig gesprek te voeren met een object in de kamer?",
            "[Random speler], durf je een minuut lang stil te staan in een gekke pose?",
            "[Random speler], durf je te dansen zonder muziek?",
            "[Random speler], durf je een verhaal te verzinnen over hoe je in een andere stad bent beland?",
            "[Random speler], durf je te bellen naar een fastfoodrestaurant en een raar verzoek te doen?",
            "[Random speler], durf je iets uit je tas of zakken te laten zien wat je nu bij je hebt?",
            "[Random speler], durf je een willekeurige foto van je galerij te laten zien?",
            "[Random speler], durf je een geheim prijs te geven over je kindertijd?",
            "[Random speler], durf je een leugen te bekennen die je ooit hebt verteld?",
            "[Random speler], durf je iemand een gênante bijnaam te geven?",
            "[Random speler], durf je een dansje te doen op een willekeurig lied dat iemand kiest?",
            "[Random speler], durf je een slechte grap te vertellen?",
            "[Random speler], durf je een schattig geluid na te doen van een dier?",
            "[Random speler], durf je een liedje na te zingen dat iemand in de kamer kiest?",
            "[Random speler], durf je de persoon naast je iets te laten schrijven op je hand?",
            "[Random speler], durf je je meest recente appgesprek te openen en voor te lezen?",
            "[Random speler], durf je een object in de kamer te verkopen alsof je een verkoper bent?",
            "[Random speler], durf je iets te eten dat iemand anders kiest?",
            "[Random speler], durf je je volgende drankje te drinken zonder je handen te gebruiken?",
            "[Random speler], durf je een mop te verzinnen en nu meteen te vertellen?",
            "[Random speler], durf je je meest gênante foto te laten zien aan iedereen?",
            "[Random speler], durf je iemand anders een nieuwe outfit te laten kiezen uit jouw kleding?",
            "[Random speler], durf je een imitatie te doen van een beroemd persoon?",
            "[Random speler], durf je een emoji-verhaal te maken in de groepschat?",
            "[Random speler], durf je een geheime crush te bekennen (of verzin een)?",
            "[Random speler], durf je te zingen terwijl je een rare dans doet?",
            "[Random speler], durf je een rare accessoire te dragen voor de rest van het spel?",
            "[Random speler], durf je een gek geluid te maken elke keer dat je naam wordt genoemd?",
            "[Random speler], durf je een onbekend nummer te bellen en te vragen naar hun favoriete kleur?",
            "[Random speler], durf je een TikTok-video te maken en te posten?",
            "[Random speler], durf je een selfie te maken met iemand anders zijn telefoon?",
            "[Random speler], durf je iemand te vertellen wat je eerste indruk van hen was?",
            "[Random speler], durf je een schattige bijnaam te verzinnen voor jezelf en deze te gebruiken?",
            "[Random speler], durf je een slechte eigenschap van jezelf toe te geven?",
            "[Random speler], durf je een improvisatiedans te doen op een gek geluid?",
            "[Random speler], durf je je meest gênante chatbericht te delen?",
            "[Random speler], durf je iets stoms te zeggen in een groepschat?",
            "[Random speler], durf je je telefoonwachtwoord te delen (alleen grapje natuurlijk!)?",
            "[Random speler], durf je iemand een vraag te stellen die je normaal nooit zou vragen?",
            "[Random speler], durf je je vreemdste eigenschap te beschrijven?",
            "[Random speler], durf je een kussengevecht te starten met iemand in de kamer?",
            "[Random speler], durf je een dans op hakken na te doen (zelfs als je ze niet hebt)?",
            "[Random speler], durf je te vertellen wie je favoriete persoon in de kamer is?",
            "[Random speler], durf je een gênant moment uit je verleden te herbeleven?",
            "[Random speler], durf je het gekste wat je vandaag hebt gedaan te beschrijven?",
            "[Random speler], durf je iemand een geheim te ontfutselen?",
            "[Random speler], durf je te springen en een gek geluid te maken zonder reden?",
            "[Random speler], durf je een scène uit een horrorfilm na te spelen?",
            "[Random speler], durf je een vreemde gewoonte te onthullen die niemand kent?",
            "[Random speler], durf je iemand te vragen naar een gênant moment?",
            "[Random speler], durf je een grap te vertellen die niemand begrijpt?",
            "[Random speler], durf je je laatst beluisterde Spotify-nummer te laten zien?",
            "[Random speler], durf je te zeggen wat je echt denkt over de persoon tegenover je?",
            "[Random speler], durf je een emoji te sturen naar je laatste contact zonder uitleg?",
            "[Random speler], durf je een rare smoes te verzinnen en te delen?",
            "[Random speler], durf je een geheim over je dag te vertellen?",
            "[Random speler], durf je een gek geluid te maken en te doen alsof het normaal is?",
            "[Random speler], durf je je telefoon achter te laten voor 10 minuten?",
            "[Random speler], durf je iemand te laten raden wat je laatst gegeten hebt?",
            "[Random speler], durf je een slechte review over iets te verzinnen?",
            "[Random speler], durf je te zeggen wat je zou doen als je miljonair was?",
            "[Random speler], durf je te vragen wat iedereen echt van je denkt?",
            "[Random speler], durf je een rare dans uit te voeren met een willekeurig object?",
            "[Random speler], durf je een 'beste dag ooit'-verhaal te verzinnen?",
            "[Random speler], durf je iets raars te zeggen tegen iemand die je niet kent?",
            "[Random speler], durf je te doen alsof je een superheld bent voor 1 minuut?",
            "[Random speler], durf je een foto te kiezen en in een groepschat te delen?"

        };

        // Lijst met spelers
        private readonly List<string> _players = new()
        {
            "Noud", "Eva", "Ricardo", "Dewi", "Rimke", "Damon", "Youp"
        };

        // Willekeurige generator
        private readonly Random _random = new();

        public Durf()
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
