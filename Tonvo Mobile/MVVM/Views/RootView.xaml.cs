using Tonvo_Mobile.MVVM.ViewModels;

namespace Tonvo_Mobile.MVVM.Views;

public partial class RootView : ContentPage
{
    private readonly RootViewModel vm;
    public RootView(RootViewModel vm)
	{
        this.vm = vm;
        BindingContext = vm;
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        vm.Init();
    }
}