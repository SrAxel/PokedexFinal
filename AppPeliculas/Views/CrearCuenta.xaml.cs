using Firebase.Auth;
using System.Net.Http.Headers;

namespace AppPeliculas.Views;

public partial class CrearCuenta : ContentPage
{
    private readonly FirebaseAuthClient _client;
    private const string FirebaseApiKey = "AIzaSyAI3gWfb15qePFTO5sYXFFC44LuhFp-2eg";
    private const string RequestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + FirebaseApiKey;
    public CrearCuenta(FirebaseAuthClient firebaseAuthClient)
	{
		InitializeComponent();
        _client = firebaseAuthClient;
	}

    private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
    {
        if (txtPassword.Text == txtVerificarPassword.Text)
        {
            try
            {
                var user = await _client.CreateUserWithEmailAndPasswordAsync(txtEmail.Text, txtPassword.Text);
                await SendVerificationEmailAsync(user.User.GetIdTokenAsync().Result);
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Cuenta creada!", "Ok");
            }
            catch (FirebaseAuthException error)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Ocurrió un problema:" + error.Reason, "Ok");

            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Registrarse", "Las contraseñas ingresadas no coinciden", "Ok");
        }
    }

    public static async Task SendVerificationEmailAsync(string idToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent("{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + idToken + "\"}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(RequestUri, content);
            response.EnsureSuccessStatusCode();
        }
    }
}