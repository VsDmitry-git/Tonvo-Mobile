using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    [ObservableObject]
    public partial class CreateVacancyViewModel
    {

        [ObservableProperty] private string name;
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;
        [ObservableProperty] private string nameErrorMessage;
        [ObservableProperty] private string emailErrorMessage;
        [ObservableProperty] private string passwordErrorMessage;
        [ObservableProperty] private bool showNameErrorMessage;
        [ObservableProperty] private bool showEmailErrorMessage;
        [ObservableProperty] private bool showPasswordErrorMessage;
        [ObservableProperty] private bool enableButton;
        [ObservableProperty] private bool isValidName;
        [ObservableProperty] private bool isValidEmail;
        [ObservableProperty] private bool isValidPassword;

        [ObservableProperty] private ObservableCollection<Vacancy> vacancies;

        public CreateVacancyViewModel()
        {
            Vacancies = GlobalViewModel.Vacancies;
        }


        [RelayCommand]
        async Task SignUp()
        {
            if (EnableButton)
            {
                try
                {
                    await Application.Current.MainPage.DisplayAlert("Регистрация завершена",
                        "Ваше резюме успешно создано", "Ok");
                    await Shell.Current.GoToAsync("..");
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("Регистрация провалена",
                        "Не удалось создать резюме", "Ok");
                }
            }

        }


        [RelayCommand]
        public Task ValidateName()
        {
            if (!string.IsNullOrEmpty(Name) && Name.Length >= 2)
            {
                IsValidName = true;
                ShowNameErrorMessage = false;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;
            }
            else
            {
                NameErrorMessage = "*Пожалуйста, введите имя, состоящее не менее чем из двух символов";
                IsValidName = false;
                ShowNameErrorMessage = true;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }


        string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        [RelayCommand]
        public async Task ValidateEmail()
        {
            if (!string.IsNullOrEmpty(Email) && Regex.IsMatch(Email, emailPattern))
            {
                if (Vacancies != null)
                {
                    foreach (Vacancy item in Vacancies)
                    {
                        if (Email.Equals(item.Email))
                        {
                            EmailErrorMessage = "*Эта почта уже занята. Попробуйте другую";
                            IsValidEmail = false;
                            ShowEmailErrorMessage = true;
                            EnableButton = IsValidName && IsValidEmail && IsValidPassword;
                            return;
                        }
                    }
                }
                else return;

                IsValidEmail = true;
                ShowEmailErrorMessage = false;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;
            }
            else
            {
                EmailErrorMessage = "*Invalid email";
                IsValidEmail = false;
                ShowEmailErrorMessage = true;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;

            }

        }

        [RelayCommand]
        public Task ValidatePassword()
        {
            if (!string.IsNullOrEmpty(Password) && Password.Length >= 8 && Password.Any(char.IsPunctuation) &&
                Password.Any(char.IsDigit) && Password.Any(char.IsLetter) && Password.Any(char.IsUpper) && Password.Any(char.IsLower))
            {
                IsValidPassword = true;
                ShowPasswordErrorMessage = false;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;

            }
            else
            {
                PasswordErrorMessage = "*Пароль должен содержать буквы верхнего и нижнего регистра, цифры, спецсимволы и длиной не менее 8 символов";
                IsValidPassword = false;
                ShowPasswordErrorMessage = true;
                EnableButton = IsValidName && IsValidEmail && IsValidPassword;

            }

            return Task.CompletedTask;
        }


    }
}
