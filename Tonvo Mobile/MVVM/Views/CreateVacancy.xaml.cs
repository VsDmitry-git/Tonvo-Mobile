namespace Tonvo_Mobile.MVVM.Views;

public partial class CreateVacancy : ContentPage
{
    private CreateVacancyViewModel vm;
	public CreateVacancy(CreateVacancyViewModel vm)
    {
        this.vm = vm;
        BindingContext = vm;
		InitializeComponent();
	}
}