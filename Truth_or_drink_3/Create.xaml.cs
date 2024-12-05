using Microsoft.Maui.Controls;

namespace Truth_or_drink_3
{
    public partial class Create : ContentPage
    {
        public Create()
        {
            InitializeComponent();
        }

        // Event-handler voor de START-knop
        private async void OnStartButtonClicked(object sender, EventArgs e)
        {
            // Navigeer naar de Vraag-pagina
            await Navigation.PushAsync(new Thema());
        }
    }
}
