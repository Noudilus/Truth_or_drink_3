using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Truth_or_drink_3
{
    public partial class Wouldyourather : ContentPage
    {
        private readonly List<Player> _players;

        // Constructor, accepteer de List<Player> die wordt doorgegeven bij navigatie
        public Wouldyourather(List<Player> players)
        {
            InitializeComponent();
            _players = players ?? new List<Player>(); // Zorg ervoor dat _players correct is geïnitialiseerd
        }

        // Structuur om de API-respons op te slaan
        public class WouldYouRatherQuestion
        {
            public string? Id { get; set; }
            public string? Type { get; set; }
            public string? Rating { get; set; }
            public string? Question { get; set; }
            public Dictionary<string, string>? Translations { get; set; }
            public string? Pack { get; set; } // Dit veld kan nullable zijn omdat het optioneel is
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
                    PropertyNameCaseInsensitive = true // Zorgt ervoor dat JSON-veldnaam niet hoofdlettergevoelig is
                };

                var questionData = JsonSerializer.Deserialize<WouldYouRatherQuestion>(response, options);

                if (questionData != null && !string.IsNullOrEmpty(questionData.Question))
                {
                    Dispatcher.Dispatch(() =>
                    {
                        QuestionLabel.Text = questionData.Question;
                    });
                }
                else
                {
                    Console.WriteLine("Deserialized questionData is null or missing key data.");
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
    }
}
