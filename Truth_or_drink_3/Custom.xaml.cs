namespace Truth_or_drink_3;

public partial class Custom : ContentPage
{
    private List<string> _questions; // Lijst om vragen op te slaan

    public Custom(List<Player> players)
    {
        InitializeComponent();
        _questions = new List<string>(); // Initialiseer de lijst met vragen
        BindingContext = this; // Stel de bindingcontext in
    }

    // Binding property for CollectionView
    public List<string> Questions
    {
        get => _questions;
        set
        {
            _questions = value;
            OnPropertyChanged(nameof(Questions)); // Verfris de UI wanneer de lijst verandert
        }
    }

    // Event handler voor de "Toevoegen"-knop
    private void AddQuestionButtonClicked(object sender, EventArgs e)
    {
        var newQuestion = NewQuestionEntry.Text?.Trim();

        if (!string.IsNullOrEmpty(newQuestion))
        {
            _questions.Add(newQuestion); // Voeg de vraag toe aan de lijst
            Questions = new List<string>(_questions); // Update de binding
            NewQuestionEntry.Text = string.Empty; // Wis het invoerveld
        }
        else
        {
            DisplayAlert("Fout", "De vraag mag niet leeg zijn!", "OK");
        }
    }

    // Event handler voor de verwijderknop
    private void DeleteQuestionButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string question)
        {
            _questions.Remove(question); // Verwijder de vraag uit de lijst
            Questions = new List<string>(_questions); // Update de binding
        }
    }

    // Event handler voor de "Random Vraag"-knop
    private void RandomQuestionButtonClicked(object sender, EventArgs e)
    {
        if (_questions.Count > 0)
        {
            var random = new Random();
            var randomQuestion = _questions[random.Next(_questions.Count)]; // Kies een willekeurige vraag
            RandomQuestionLabel.Text = randomQuestion; // Toon de vraag
        }
        else
        {
            RandomQuestionLabel.Text = "Geen vragen beschikbaar. Voeg eerst vragen toe!";
        }
    }
}
