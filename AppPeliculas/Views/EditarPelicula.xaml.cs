using AppPeliculas.Modelos;
using AppPeliculas.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace AppPeliculas.Views;

public partial class EditarPelicula : ContentPage
{
    RepositoryPokemon repositoryPokemons = new RepositoryPokemon();
    public Pokemon PokemonSeleccionado { get; }

    public EditarPelicula()
    {
        InitializeComponent();
    }

    public EditarPelicula(Pokemon pokemonSeleccionado)
    {
        InitializeComponent();
        PokemonSeleccionado = pokemonSeleccionado;
        CargarDatosEnPantalla();
    }

    private void CargarDatosEnPantalla()
    {
        txtNombre.Text = PokemonSeleccionado.nombre;
        txtGenero.Text = PokemonSeleccionado.genero;
        txtTipo.Text = PokemonSeleccionado.tipo;
        txtPortadaUrl.Text = PokemonSeleccionado.portada_url;
        txtDebilidad.Text = PokemonSeleccionado.debilidad;

    }

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        Pokemon pokemonEditado = new Pokemon()
        {
            _id = PokemonSeleccionado._id,
            nombre = txtNombre.Text,
            genero = txtGenero.Text,
            tipo = txtTipo.Text,
            portada_url = txtPortadaUrl.Text,
            debilidad = txtDebilidad.Text,
        };

        var guardada = await repositoryPokemons.UpdateAsync(pokemonEditado);

        if (guardada)
        {
            await DisplayAlert("Notificación", "Pokemon guardado", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}