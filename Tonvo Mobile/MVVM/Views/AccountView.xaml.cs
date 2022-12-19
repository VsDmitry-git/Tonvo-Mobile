namespace Tonvo_Mobile.MVVM.Views;

public partial class AccountView : ContentPage
{
	AccountViewModel vm;
	public AccountView(AccountViewModel vm)
	{
		this.vm = vm;
		BindingContext= vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        vm.Init();
    }
}