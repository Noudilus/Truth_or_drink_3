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
        "Maak een foto van je favoriete drankje."
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
