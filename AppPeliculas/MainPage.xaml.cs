namespace AppPeliculas;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void AlPresionarBoton(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Presionado {count} vez";
		else
			CounterBtn.Text = $"Presionado {count} veces";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

