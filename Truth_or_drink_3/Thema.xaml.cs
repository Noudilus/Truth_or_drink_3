namespace Truth_or_drink_3
{
    public partial class Thema : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly List<Player> _players;

        public Thema(DatabaseService databaseService, List<Player> players)
        {
            InitializeComponent();
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _players = players ?? new List<Player>(); // Ensure players list is not null
        }

        private async void WaarheidKnopClicked(object sender, EventArgs e)
        {
            if (_players.Count == 0)
            {
                await DisplayAlert("Fout", "Geen spelers beschikbaar.", "OK");
                return;
            }
            await Navigation.PushAsync(new Vraag(_databaseService, _players));
        }

        private async void DoenKnopClicked(object sender, EventArgs e)
        {
            if (_players.Count == 0)
            {
                await DisplayAlert("Fout", "Geen spelers beschikbaar.", "OK");
                return;
            }
            await Navigation.PushAsync(new Doen(_databaseService, _players));
        }

        private async void DurfKnopClicked(object sender, EventArgs e)
        {
            if (_players.Count == 0)
            {
                await DisplayAlert("Fout", "Geen spelers beschikbaar.", "OK");
                return;
            }
            await Navigation.PushAsync(new DurfPage(_databaseService, _players));
        }

        private async void MixKnopClicked(object sender, EventArgs e)
        {
            if (_players.Count == 0)
            {
                await DisplayAlert("Fout", "Geen spelers beschikbaar.", "OK");
                return;
            }
            await Navigation.PushAsync(new Mix(_databaseService, _players));
        }

        private async void CustomKnopClicked(object sender, EventArgs e)
        {
            if (_players.Count == 0)
            {
                await DisplayAlert("Fout", "Geen spelers beschikbaar.", "OK");
                return;
            }
            await Navigation.PushAsync(new Custom(_databaseService, _players));
        }

        private async void WouldYouRatherKnopClicked(object sender, EventArgs e)
        {
            if (_players.Count == 0)
            {
                await DisplayAlert("Fout", "Geen spelers beschikbaar.", "OK");
                return;
            }
            await Navigation.PushAsync(new Wouldyourather(_databaseService, _players));
        }
    }
}
