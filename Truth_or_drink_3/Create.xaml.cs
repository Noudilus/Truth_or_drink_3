using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace Truth_or_drink_3
{
    public partial class Create : ContentPage
    {
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();

        public Create()
        {
            InitializeComponent();
            NamesListView.ItemsSource = Players; // Bind de lijst aan de ObservableCollection
        }

        private void AddPlayerButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                // Voeg de speler toe aan de ObservableCollection
                Players.Add(new Player { Name = NameEntry.Text });

                // Maak het invoerveld leeg
                NameEntry.Text = "";
            }
            else
            {
                DisplayAlert("Fout", "Naam mag niet leeg zijn!", "OK");
            }
        }

        private void DeletePlayerButtonClicked(object sender, EventArgs e)
        {
            // Haal de speler op die gekoppeld is aan de CommandParameter van de knop
            if (sender is Button button && button.CommandParameter is Player player)
            {
                Players.Remove(player); // Verwijder de speler uit de ObservableCollection
            }
        }

        private async void StartButtonClicked(object sender, EventArgs e)
        {
            if (Players.Count > 0)
            {
                // Converteer ObservableCollection naar List<Player>
                var playerList = new List<Player>(Players);

                // Navigeer naar de Thema-pagina met de lijst van spelers
                await Navigation.PushAsync(new Thema(playerList));
            }
            else
            {
                await DisplayAlert("Fout", "Voeg minstens één speler toe om te starten.", "OK");
            }
        }

    }
}

