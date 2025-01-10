using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace Truth_or_drink_3;

public partial class Foto : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly List<Player> _players;
    private readonly List<string> _opdrachten = new()
    {
        "Maak een foto van iets roods.",
        "Maak een foto van het gekste voorwerp in de kamer.",
        "Maak een selfie met een grappig gezicht.",
        "Maak een foto van iets met een schaduw.",
        "Maak een foto van je favoriete drankje.",
        "Maak een foto van iets wat je hebt gekocht tijdens je laatste vakantie.",
        "Maak een foto van een object dat je altijd bij je hebt.",
        "Maak een foto van iets dat je nooit had verwacht te zien.",
        "Maak een foto van een voorwerp dat begint met de letter 'A'.",
        "Maak een foto van iets dat je vroeger als kind geweldig vond.",
        "Maak een foto van iets dat je vandaag hebt geleerd.",
        "Maak een foto van iets dat je zou meenemen naar een onbewoond eiland.",
        "Maak een foto van een voorwerp dat je zou gebruiken als je superkracht had.",
        "Maak een foto van iets dat je je ouders nooit zou durven laten zien.",
        "Maak een foto van je favoriete boek of film in je kamer.",
        "Maak een foto van iets dat je zou gebruiken in een kunstwerk.",
        "Maak een foto van een voorwerp dat je niet zonder kunt functioneren.",
        "Maak een foto van iets dat je gelukkig maakt.",
        "Maak een foto van iets wat je altijd hebt willen leren.",
        "Maak een foto van iets dat je nooit eerder hebt geprobeerd.",
        "Maak een foto van een object dat je altijd in je tas hebt zitten.",
        "Maak een foto van iets dat je zou gebruiken voor een sciencefictionfilm.",
        "Maak een foto van iets dat je verbazingwekkend mooi vindt.",
        "Maak een foto van iets dat je dacht dat het niet bestond.",
        "Maak een foto van iets dat je nooit zou kopen, maar het ziet er interessant uit."

    };

    public Foto(DatabaseService databaseService, List<Player> players)
    {
        InitializeComponent();
        _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        _players = players ?? new List<Player>();

        GenereerNieuweOpdracht();
    }

    private void GenereerNieuweOpdracht()
    {
        // Kies een willekeurige opdracht
        Random random = new();
        int index = random.Next(_opdrachten.Count);
        OpdrachtLabel.Text = _opdrachten[index];
    }

    private async void FotoKnopClicked(object sender, EventArgs e)
    {
        try
        {
            // Controleer of de camera beschikbaar is
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Start de camera en maak een foto
                FileResult foto = await MediaPicker.Default.CapturePhotoAsync();

                if (foto != null)
                {
                    // Converteer de foto naar een stream en toon deze in de Image-view
                    var stream = await foto.OpenReadAsync();
                    FotoWeergave.Source = ImageSource.FromStream(() => stream);
                }
            }
            else
            {
                await DisplayAlert("Fout", "Camera is niet beschikbaar op dit apparaat.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fout", $"Er is een probleem opgetreden: {ex.Message}", "OK");
        }
    }

    private void NieuweOpdrachtClicked(object sender, EventArgs e)
    {
        GenereerNieuweOpdracht();
    }
}
