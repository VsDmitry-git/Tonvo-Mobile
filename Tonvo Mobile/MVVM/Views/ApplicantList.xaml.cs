namespace Tonvo_Mobile.MVVM.Views;

public partial class ApplicantList : ContentPage
{
    private ApplicantViewModel vm;
	public ApplicantList(ApplicantViewModel vm)
	{
        this.vm = vm;
        BindingContext = vm;
        InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await vm.Init();
    }
}