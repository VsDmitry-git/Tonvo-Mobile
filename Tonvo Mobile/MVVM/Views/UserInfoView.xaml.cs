using Tonvo_Mobile.MVVM.ViewModels;

namespace Tonvo_Mobile.MVVM.Views;

public partial class UserInfoView : ContentPage
{
	public UserInfoView()
	{
		InitializeComponent();
		BindingContext = new UserInfoViewModel();
	}
}