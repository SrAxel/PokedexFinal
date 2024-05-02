using Firebase.Auth;
using Firebase.Auth.Repository;

namespace AppPeliculas.Views;

public partial class IniciarSesion : ContentPage
{
    private readonly FirebaseAuthClient _clientAuth;
    private FileUserRepository _userRepository;
    private UserInfo _userInfo;
    private FirebaseCredential _firebaseCredential;
    public IniciarSesion(FirebaseAuthClient firebaseAuthClient)
    {
        InitializeComponent();
        _clientAuth = firebaseAuthClient;
        _userRepository = new FileUserRepository("AppPeliculas");

        ChequearSiHayUsuarioAlmacenado();
    }
    private async void ChequearSiHayUsuarioAlmacenado()
    {
        if (_userRepository.UserExists())
        {
            (_userInfo, _firebaseCredential) = _userRepository.ReadUser();

            await Navigation.PushAsync(new AppShell(_clientAuth));
        }
    }

    private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
    {

        try
        {

            var userCredential = await _clientAuth.SignInWithEmailAndPasswordAsync(txtEmail.Text, txtPassword.Text);

            if (chkRecordarContraseña.IsChecked)
            {
                _userRepository.SaveUser(userCredential.User);
            }
            else
            {
                _userRepository.DeleteUser();
            }

            await Navigation.PushAsync(new AppShell(_clientAuth));

        }
        catch (FirebaseAuthException error)
        {
            await Application.Current.MainPage.DisplayAlert("Inicio de sesión", "Ocurrió un problema:" + error.Reason, "Ok");

        }
    }

    private async void btnRegistrarse_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearCuenta(_clientAuth));
    }
}