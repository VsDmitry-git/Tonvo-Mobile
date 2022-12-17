using System.Windows.Input;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    [ObservableObject]
    [QueryProperty(nameof(SelectedVacancy), "SelectedVacancy")]
    public partial class UserInfoViewModel
    {
        [ObservableProperty]
        private string vacancyName;
        [ObservableProperty]
        private string companyName;
        [ObservableProperty]
        private string vacancySalary;
        [ObservableProperty]
        private string requiredExperience;
        [ObservableProperty]
        private string addressCompany;
        [ObservableProperty]
        private string vacancyDescription;
        [ObservableProperty]
        private string email;

        public ICommand Respond => new Command(async () => await RespondAsync());

        private Vacancy selectedVacancy;
        public Vacancy SelectedVacancy
        {
            get => selectedVacancy;
            set
            {
                SetProperty(ref selectedVacancy, value);
                vacancyName = value.VacancyName;
                companyName= value.CompanyName;
                vacancySalary = value.VacancySalary;
                requiredExperience = value.RequiredExperience;
                addressCompany = value.AddressCompany;
                vacancyDescription= value.VacancyDescription;
                email= value.Email;
            }
        }

        public UserInfoViewModel()
        {
            
        }

        async Task RespondAsync()
        {
            //foreach (var item in SelectedVacancy.Responds)
            //{
            //    if (item == UserApplicant.Id) return;
            //}
            //SelectedVacancy.Responds.Add(GlobalViewModel.UserApplicant.Id);
            //DataStorage.VacancyAccountChange(GlobalViewModel.SelectedVacancy);
        }
    }
}
