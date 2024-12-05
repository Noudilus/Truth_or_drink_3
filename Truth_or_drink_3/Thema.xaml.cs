namespace Truth_or_drink_3;

public partial class Thema : ContentPage
{
	public Thema()
	{
		InitializeComponent();
	}
    private void WaarheidKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vraag());
    }
    private void DoenKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Doen());
    }
    private void DurfKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Durf());
    }
    private void MixKnopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Mix());
    }
}