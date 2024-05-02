using CommunityToolkit.Maui.Views;

namespace AppPeliculas.Views;

public partial class TrailerPelicula : ContentPage
{
	string url_trailer;
	public TrailerPelicula(string url)
	{
		InitializeComponent();
		this.url_trailer = url;
		//ReproductorWebView.Source = url;
	}

	protected async override void OnAppearing()
	{
  //      var youtube = new YoutubeClient();
		//var video= await youtube.Videos.GetAsync("L8P2nvNbYGk");
		//ReproductorMediaElement.Source = video.;
  //      ReproductorMediaElement.Play();
    }
}