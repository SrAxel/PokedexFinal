using System.Diagnostics;

namespace AppPeliculas.Views;

public partial class ControlesMaui : ContentPage
{
	public IList<string> Localidades=new List<string>()
	{
		"San Justo",
		"Marcelino Escalada",
		"Videla",
		"Soledad"
	};


	public ControlesMaui()
	{
		InitializeComponent();
		AlturaSlider.ValueChanged += CambioValorSlider;
		LocalidadesPicker.ItemsSource = Localidades.ToList();
	}

    private void CambioValorSlider(object sender, ValueChangedEventArgs e)
    {
		//txtNombre.Text =Math.Round((AlturaSlider.Value),0).ToString();
        Debug.Print(AlturaSlider.Value.ToString());
    }
}