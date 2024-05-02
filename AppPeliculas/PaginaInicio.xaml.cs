namespace AppPeliculas;

public partial class PaginaInicio : ContentPage
{
    public PaginaInicio()
    {
        InitializeComponent();
        DocumentalesBtn.Clicked += MostrarDocumentales;

        SeriesBtn.Clicked += (s, a) =>
        {
            ContenidoLbl.Text = "Series con funciones flecha";
        };
    }
    public async void MostrarPeliculas(object sender, EventArgs e)
    {
        //await DisplayAlert("Peliculas", "Mostrando la lista de peliculas", "OK");
        ContenidoLbl.Text = "Peliculas";
    }
    public void MostrarDocumentales(object sender, EventArgs e)
    {
        ContenidoLbl.Text = "Documentales";
    }
    private async void CerrarLaVentana(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}