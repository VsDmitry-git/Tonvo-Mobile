namespace Tonvo_Mobile.MVVM.Views;

public partial class ApplicantInfoView : ContentPage
{
	ApplicantInfoViewModel vm;

	public ApplicantInfoView(ApplicantInfoViewModel vm)
	{
		this.vm = vm;
		BindingContext= vm;
		InitializeComponent();
	}
}