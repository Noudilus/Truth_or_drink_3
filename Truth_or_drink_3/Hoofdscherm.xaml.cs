namespace Truth_or_drink_3
{
    public partial class Hoofdscherm : ContentPage
    {
        public Hoofdscherm()
        {
            InitializeComponent();
        }

        private void CreateKnopClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Thema());
        }
    }
}
