using System.Globalization;
using Tonvo.Services;
using Tonvo_Mobile.MVVM.Views;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    public partial class AppShellVieModel : ObservableObject
    {
        [RelayCommand]
        async Task SignOut()
        {
            DataStorage.SaveApplicantList(GlobalViewModel.Applicants);
            DataStorage.SaveVacancyList(GlobalViewModel.Vacancies);

            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
