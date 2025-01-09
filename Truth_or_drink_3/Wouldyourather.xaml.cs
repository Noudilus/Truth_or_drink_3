using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Truth_or_drink_3
{
    public partial class Wouldyourather : ContentPage
    {
        private readonly List<Player> _players;
        private int _currentPlayerIndex = 0; // Houd bij wie de huidige speler is

        public Wouldyourather(List<Player> players)
        {
            InitializeComponent();
            _players = players ?? new List<Player>(); // Zorg ervoor dat _players correct is geïnitialiseerd
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
                    PropertyNameCaseInsensitive = true // Zorgt ervoor dat JSON-veldnaam niet hoofdlettergevoelig is
                };

                var questionData = JsonSerializer.Deserialize<WouldYouRatherQuestion>(response, options);

                if (questionData != null && !string.IsNullOrEmpty(questionData.Question))
                {
                    // Kies de volgende speler op basis van _currentPlayerIndex
                    var currentPlayer = GetNextPlayer();

                    // Update de UI met de naam van de speler en de vraags
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
