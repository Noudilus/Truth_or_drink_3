namespace Truth_or_drink_3;

public partial class Thema : ContentPage
{
    private List<Player> _players;

    public Thema(List<Player> players)
    {
        InitializeComponent();
        _players = players ?? new List<Player>(); // Zorg ervoor dat _players correct wordt ge�nitialiseerd
    }

    private void WaarheidKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vraag(_players));
    }

    private void DoenKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Doen(_players));
    }

    private void DurfKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new DurfPage(_players));
    }

    private void MixKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Mix(_players));
    }

    private void CustomKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Custom(_players));
    }

    private void WouldYouRatherKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Wouldyourather(_players));
    }
}
