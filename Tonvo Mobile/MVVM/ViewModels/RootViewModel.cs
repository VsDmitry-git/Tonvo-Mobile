using System.Collections.ObjectModel;
using Tonvo.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    public partial class RootViewModel : ObservableObject
    {
        [ObservableProperty]
        private Vacancy selectedVacancy;


        async partial void OnSelectedVacancyChanged(Vacancy value)
        {
            // TODO: Изменить путь к view
            //await Shell.Current.GoToAsync($"UserInfo", true);
            Debug.WriteLine(value.VacancyName.ToString());
        }

        const int RefreshDuration = 3;
        bool _isRefreshing;


        public ObservableCollection<Vacancy> Vacancies { get; private set; } = new ObservableCollection<Vacancy>();

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

        public RootViewModel()
        {
            AddVacancy();
        }
        void AddVacancy()
        {
            Vacancies.Add(new Vacancy
            {
                VacancyName = "Vacancy",
                VacancySalary = "Salary",
                CompanyName = "Company"
            });
            Vacancies.Add(new Vacancy
            {
                VacancyName = "Vacancy2",
                VacancySalary = "Salary2",
                CompanyName = "Company2"
            });
        }

        async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            // TODO: Добавить метод заполнения списка
            AddVacancy();
            IsRefreshing = false;
        }


    }
}
