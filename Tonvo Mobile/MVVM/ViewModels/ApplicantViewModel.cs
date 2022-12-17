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
    public partial class ApplicantViewModel : ObservableObject
    {
        [ObservableProperty]
        private Applicant selectedApplicant;
        [ObservableProperty]
        private ObservableCollection<Applicant> applicants;

        public ApplicantViewModel()
        {
        }

        private int mode;

        public async Task Init()
        {
            try
            {
                await ReadApplicant();
                GlobalViewModel.mode = 1;
            }
            catch (Exception)
            {
                // TODO: logging
                Debug.WriteLine("Unable to retrieve list");
            }
        }
        async partial void OnSelectedApplicantChanged(Applicant value)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"SelectedApplicant", SelectedApplicant}
            };
            await Shell.Current.GoToAsync("ApplicantInfoView", navigationParameter);
        }

        const int RefreshDuration = 1;
        bool _isRefreshing;

        public ICommand RefreshCommand => new Command(async () => await RefreshDataAsync());

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        async Task ReadApplicant()
        {
            Applicants = new ObservableCollection<Applicant>();
            Applicants.Add(new Applicant { ApplicantSalary = "ffdf", ProfessionName = "kkl", WorkExperience = "jl", Name = "jhknj", SecondName="jhb" });
            Applicants.Add(new Applicant { ApplicantSalary = "sffweefwdf", ProfessionName = "rger", WorkExperience = "ergf", Name="bjoijjb", SecondName="jbbbkkjhbbkjnkhhjkj" });
        }

        async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            Applicants.Clear();
            await ReadApplicant();
            IsRefreshing = false;
        }
    }
}
