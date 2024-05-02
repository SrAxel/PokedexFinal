using AppPeliculas.Modelos;
using Newtonsoft.Json;
using System.Text;
using System;
using AppPeliculas.Repositories;

namespace AppPeliculas.Views;

public partial class AgregarPelicula : ContentPage
{
    RepositoryPokemon repositoryPokemons = new RepositoryPokemon();
    public AgregarPelicula()
    {
        InitializeComponent();
    }

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        Pokemon nuevoPokemon = new Pokemon()
        {
            nombre = txtNombre.Text,
            genero = txtGenero.Text,
            tipo = txtTipo.Text,
            portada_url = txtPortadaUrl.Text,
            debilidad = txtDebilidad.Text,
        };

        var agregada = await repositoryPokemons.AddAsync(nuevoPokemon);

        if (agregada)
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