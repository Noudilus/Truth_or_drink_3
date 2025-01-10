using System.Text.Json;

namespace Truth_or_drink_3
{
    public partial class Wouldyourather : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly List<Player> _players;
        private int _currentPlayerIndex = 0;

        public Wouldyourather(DatabaseService databaseService, List<Player> players)
        {
            InitializeComponent();
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _players = players ?? new List<Player>(); // Zorg ervoor dat de lijst van spelers niet null is
            BindingContext = this;
        }

        public class WouldYouRatherQuestion
        {
            public string? Id { get; set; }
            public string? Type { get; set; }
            public string? Rating { get; set; }
            public string? Question { get; set; }
            public Dictionary<string, string>? Translations { get; set; }
            public string? Pack { get; set; }
        }

        private async void GetQuestionButtonClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://api.truthordarebot.xyz/api/wyr";
            string fullUrl = $"{apiUrl}?rating=pg";

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(fullUrl);

                Console.WriteLine($"API Response: {response}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var questionData = JsonSerializer.Deserialize<WouldYouRatherQuestion>(response, options);

                if (questionData != null && !string.IsNullOrEmpty(questionData.Question))
                {
                    // Kies de volgende speler op basis van _currentPlayerIndex
                    var currentPlayer = GetNextPlayer();

                    // Update de UI met de naam van de speler en de vraag
                    Dispatcher.Dispatch(() =>
                    {
                        PlayerNameLabel.Text = $"Speler: {currentPlayer}";
                        QuestionLabel.Text = questionData.Question;
                    });
                }
                else
                {
                    Dispatcher.Dispatch(() =>
                    {
                        QuestionLabel.Text = "Failed to load question. The response was empty or malformed.";
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Dispatcher.Dispatch(() =>
                {
                    QuestionLabel.Text = $"Error loading question: {ex.Message}";
                });
            }
        }

        private string GetNextPlayer()
        {
            if (_players == null || !_players.Any())
            {
                return "Geen spelers beschikbaar"; // Fallback voor het geval er geen spelers beschikbaar zijn
            }

            // Haal de naam van de huidige speler op
            var currentPlayer = _players[_currentPlayerIndex].Name;

            // Verhoog de index en zorg ervoor dat deze cyclisch blijft
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

            return currentPlayer;
        }
    }
}
