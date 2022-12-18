namespace Tonvo_Mobile.MVVM.Views;

public partial class Login : ContentPage
{
    private LoginViewModel viewModel;

    public Login(LoginViewModel viewModel)
    {
        this.viewModel = viewModel;
        BindingContext = viewModel;
        InitializeComponent();
    }
}