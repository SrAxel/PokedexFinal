using AppPeliculas.Modelos;
using AppPeliculas.Repositories;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AppPeliculas.Views;

public partial class InicioApp : ContentPage
{
    public ObservableCollection<Pokemon> Pokemon { get; set; }
    private Pokemon pokemonSeleccionado;



    public Pokemon PokemonSeleccionado
    {
        get { return pokemonSeleccionado; }
        set
        {
            pokemonSeleccionado = value;
            OnPropertyChanged(nameof(PokemonSeleccionado));
        }
    }



    RepositoryPokemon repositoryPokemons = new RepositoryPokemon();
    public InicioApp()
    {
        InitializeComponent();
        Pokemon = new ObservableCollection<Pokemon>();
        PeliculasCollectionView.SelectionChanged += SeleccionarPelicula;
    }

    private void SeleccionarPelicula(object sender, SelectionChangedEventArgs e)
    {
        if (PeliculasCollectionView.SelectedItem != null)
        {
            PokemonSeleccionado = (Pokemon)PeliculasCollectionView.SelectedItem;
        }
    }

    public async Task GetAllPeliculas(object sender, EventArgs e)
    {
        Pokemon = await repositoryPokemons.GetAllAsync();
        PeliculasCollectionView.ItemsSource = Pokemon;
    }

    public void SeleccionarPokemonEnCollectionView(object sender, EventArgs e)
    {
        //iteramos las peliculas hasta encontrar la coincide con la Pelicula Seleccionada, al encontrarla la utilizaremos para indicar que es el SelectedItem del CollectionView e interrumpiremos la iteración.
        if (PokemonSeleccionado != null)
        {
            foreach (var pokemon in Pokemon)
            {
                if (pokemon._id == PokemonSeleccionado._id)
                {
                    PeliculasCollectionView.SelectedItem = pokemon;
                    PeliculasCollectionView.ScrollTo(pokemon, null, ScrollToPosition.Center, true);
                    //PokemonCollectionView.;
                    break;
                }
            }
        }

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        NetworkAccess conexionInternet = Connectivity.Current.NetworkAccess;
        if (conexionInternet == NetworkAccess.Internet)
        {
            await GetAllPeliculas(this, EventArgs.Empty);
            SeleccionarPokemonEnCollectionView(this, EventArgs.Empty);
        }

    }
    protected override bool OnBackButtonPressed()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> se ha pulsado el botón de atrás");
        return false;
    }
    protected override void OnDisappearing()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> se ha cerrado la ventana de la lista de Pokemons");
    }
    private async void AbrirPaginaInicio(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PaginaInicio());
    }

    private async void AgregarPokemonBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarPelicula());
    }

    private async void EliminarPokemonBtn_Clicked(object sender, EventArgs e)
    {
        if (PokemonSeleccionado != null)
        {
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Está seguro que desea borrar el Pokemon:{PokemonSeleccionado.nombre}", "Si", "No");
            if (respuesta)
            {
                var eliminado = await repositoryPokemons.RemoveAsync(PokemonSeleccionado._id);
                if (eliminado)
                {
                    await Application.Current.MainPage.DisplayAlert("Eliminar", $"Se ha eliminado el Pokemon {PokemonSeleccionado.nombre} correctamente", "Ok");
                    GetAllPeliculas(this, EventArgs.Empty);
                }
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Eliminar", "Error: debe seleccionar el Pokemon que quiere borrar", "ok");
        }
    }

    private async void EditarPokemonBtn_Clicked(object sender, EventArgs e)
    {
        if (PokemonSeleccionado != null)
        {
            await Navigation.PushAsync(new EditarPelicula(PokemonSeleccionado));
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Editar", "Error: debe seleccionar la película que quiere editar", "ok");
        }
    }

    private void PeliculasBtn_Clicked(object sender, EventArgs e)
    {
        //PeliculasCollectionView.
        //SeleccionarPeliculaEnCollectionView(this,EventArgs.Empty);
    }

}