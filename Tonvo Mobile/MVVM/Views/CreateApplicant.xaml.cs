namespace Tonvo_Mobile.MVVM.Views;

public partial class CreateApplicant : ContentPage
{
    private CreateApplicantViewModel vm;
	public CreateApplicant(CreateApplicantViewModel vm)
    {
        this.vm = vm;
        BindingContext = vm;
		InitializeComponent();
	}
}