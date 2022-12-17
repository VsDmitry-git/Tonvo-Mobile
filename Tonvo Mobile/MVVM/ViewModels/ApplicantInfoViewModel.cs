using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    [ObservableObject]
    [QueryProperty(nameof(SelectedApplicant), "SelectedApplicant")]
    public partial class ApplicantInfoViewModel
    {
        [ObservableProperty]
        private string professionName;
        [ObservableProperty]
        private string applicantSalary;
        [ObservableProperty]
        private string workExperience;
        [ObservableProperty]
        private string fullName;
        [ObservableProperty]
        private string birthday;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private ObservableCollection<int> responds;
        [ObservableProperty]
        private string applicantDescription;

        public string TestText { get; set; } = "Test";

        public ICommand Respond => new Command(async () => await RespondAsync());

        private Applicant selectedApplicant;
        public Applicant SelectedApplicant
        {
            get => selectedApplicant;
            set
            {
                SetProperty(ref selectedApplicant, value);
                professionName = value.ProfessionName;
                applicantSalary = value.ApplicantSalary;
                workExperience = value.WorkExperience;
                fullName = $"{value.SecondName} {value.Name}";
                birthday = value.Birthday;
                email = value.Email;
                responds= value.Responds;
                applicantDescription= value.ApplicantDescription;
            }
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
