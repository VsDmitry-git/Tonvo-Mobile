using System.Collections.ObjectModel;
using Tonvo_Mobile.MVVM.Modelss;
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

        [ObservableProperty]
        private ObservableCollection<Vacancy> vacancies;

        public void Init()
        {
            try
            {
                ReadVacancy();
                GlobalViewModel.mode = 0;
            }
            catch (Exception)
            {
                // TODO: logging
                Debug.WriteLine("Unable to retrieve list");
            }
        }

        async partial void OnSelectedVacancyChanged(Vacancy value)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"SelectedVacancy", SelectedVacancy}
            };
            await Shell.Current.GoToAsync("UserInfoView", navigationParameter);
            Debug.WriteLine(value.VacancyName.ToString());
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

        public RootViewModel()
        {
        }
        void ReadVacancy()
        {
            Vacancies = GlobalViewModel.Vacancies;
        }

        async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            Vacancies.Clear();
            ReadVacancy();
            IsRefreshing = false;
        }
    }
}
