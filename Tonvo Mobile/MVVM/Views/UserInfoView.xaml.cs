namespace Tonvo_Mobile.MVVM.Views;

public partial class UserInfoView : ContentPage
{
	private UserInfoViewModel vm;
	public UserInfoView(UserInfoViewModel vm)
	{
		this.vm = vm;
		BindingContext= vm;
		InitializeComponent();
	}
}