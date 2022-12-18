using Tonvo.Services;

namespace Tonvo_Mobile;

public partial class App : Application
{
	public App(LoginViewModel loginViewModel)
	{
		InitializeComponent();

        // MainPage = new AppShell();

        MainPage = new Login(loginViewModel);
    }
}
