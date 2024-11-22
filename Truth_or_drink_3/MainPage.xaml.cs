namespace Truth_or_drink_3
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Clicked(object sender, EventArgs e)
        {

        }

        public void LoginKnopClicked(object sender, EventArgs e)
        {
            bool IsUsernameEmpty = string.IsNullOrEmpty(UsernameEntry.Text);
            bool IsPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (IsUsernameEmpty)
            {
                UsernameEntry.Placeholder = "Vul iets in";
            }
            else if (IsPasswordEmpty)
            {
                PasswordEntry.Placeholder = "Vul iets in";
            }
            else
            {
                Navigation.PushAsync(new Hoofdscherm());
            }
        }
    }
}
