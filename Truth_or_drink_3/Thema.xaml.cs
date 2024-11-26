namespace Truth_or_drink_3;

public partial class Thema : ContentPage
{
	public Thema()
	{
		InitializeComponent();
	}
    private void NormaalKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Create());
    }
}