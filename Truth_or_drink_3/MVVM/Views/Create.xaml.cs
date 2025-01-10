using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Truth_or_drink_3
{
    public partial class Create : ContentPage
    {
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
        private readonly DatabaseService _databaseService;

        public Create()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("path/to/database"); // Geef het juiste pad op
            NamesListView.ItemsSource = Players;
        }

        private async void AddPlayerButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                Players.Add(new Player { Name = NameEntry.Text });
                NameEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Fout", "Naam mag niet leeg zijn!", "OK");
            }
        }

        private void DeletePlayerButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Player player)
            {
                Players.Remove(player);
            }
        }

        private async void StartButtonClicked(object sender, EventArgs e)
        {
            if (Players.Count > 0)
            {
                var playerList = new List<Player>(Players);
                await Navigation.PushAsync(new Thema(_databaseService, playerList));
            }
            else
            {
                await DisplayAlert("Fout", "Voeg minstens één speler toe om te starten.", "OK");
            }
        }
    }
}
