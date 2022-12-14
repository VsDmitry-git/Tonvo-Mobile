using Tonvo_Mobile.MVVM.ViewModels;

namespace Tonvo_Mobile.MVVM.Views;

public partial class RootView : ContentPage
{
	public RootView()
	{
		InitializeComponent();
        BindingContext = new RootViewModel();
    }
}