using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tonvo.Services;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo_Mobile.MVVM.ViewModels;

[ObservableObject]
public partial class LoginViewModel
{
    [ObservableProperty] private string email;
    [ObservableProperty] private string password;

    public LoginViewModel()
    {
        GlobalViewModel.Vacancies = DataStorage.ReadVacancyJson();
        GlobalViewModel.Vacancies = DataStorage.ReadVacancyJson();

        GlobalViewModel.Applicants= new();
        GlobalViewModel.Applicants.Add(new Applicant { ApplicantSalary = "ffdf", ProfessionName = "kkl", WorkExperience = "jl", Name = "jhknj", SecondName = "jhb" });
        GlobalViewModel.Applicants.Add(new Applicant { ApplicantSalary = "sffweefwdf", ProfessionName = "rger", WorkExperience = "ergf", Name = "bjoijjb", SecondName = "jbbbkkjhbbkjnkhhjkj" });
    }


    [RelayCommand]
    public async Task DoLogin()
    {

        try
        {
            // Проверка логина и пароля
            foreach (var item in GlobalViewModel.Applicants)
            {
                if(item.Email == Email && item.Password == password)
                {
                    GlobalViewModel.mode = 0;
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync("..");
                    return;
                }
            }
            foreach (var item in GlobalViewModel.Vacancies)
            {
                if (item.Email == Email && item.Password == password)
                {
                    GlobalViewModel.mode = 1;
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync("..");
                    return;
                }
            }
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Ошибка входа", "OK");

        }
        catch (Exception exception)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", exception.ToString(), "OK");

            Console.WriteLine(exception);
        }


    }

    [RelayCommand]
    public async Task DoCreateVacancy()
    {
        try
        {
            Application.Current.MainPage = new AppShell();

            await Shell.Current.GoToAsync($"createvacancy");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [RelayCommand]
    public async Task DoCreateApplicant()
    {
        try
        {
            Application.Current.MainPage = new AppShell();

            await Shell.Current.GoToAsync($"createapplicant");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
