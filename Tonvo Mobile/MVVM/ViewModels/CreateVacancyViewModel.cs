using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Tonvo.Services;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    [ObservableObject]
    public partial class CreateVacancyViewModel
    {
        [ObservableProperty] private string vacancyName;
        [ObservableProperty] private string vacancySalary;
        [ObservableProperty] private string companyName;
        [ObservableProperty] private string requiredExperience;
        [ObservableProperty] private string addressCompany;
        [ObservableProperty] private string vacancyDescription;
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;

        [ObservableProperty] private string vacancyNameErrorMessage;
        [ObservableProperty] private string vacancySalaryErrorMessage;
        [ObservableProperty] private string companyNameErrorMessage;
        [ObservableProperty] private string requiredExperienceErrorMessage;
        [ObservableProperty] private string addressCompanyErrorMessage;
        [ObservableProperty] private string vacancyDescriptionErrorMessage;
        [ObservableProperty] private string emailErrorMessage;
        [ObservableProperty] private string passwordErrorMessage;

        [ObservableProperty] private bool showVacancyNameErrorMessage;
        [ObservableProperty] private bool showVacancySalaryErrorMessage;
        [ObservableProperty] private bool showCompanyNameErrorMessage;
        [ObservableProperty] private bool showRequiredExperienceErrorMessage;
        [ObservableProperty] private bool showAddressCompanyErrorMessage;
        [ObservableProperty] private bool showVacancyDescriptionErrorMessage;
        [ObservableProperty] private bool showEmailErrorMessage;
        [ObservableProperty] private bool showPasswordErrorMessage;

        [ObservableProperty] private bool enableButton;

        [ObservableProperty] private bool isValidVacancyName;
        [ObservableProperty] private bool isValidVacancySalary;
        [ObservableProperty] private bool isValidCompanyName;
        [ObservableProperty] private bool isValidRequiredExperience;
        [ObservableProperty] private bool isValidAddressCompany;
        [ObservableProperty] private bool isValidVacancyDescription;
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
                    DataStorage.AddVacancy(new Vacancy
                    {
                        Id=Vacancies.Count != 0 ? Vacancies[0].Id + 1 : 0,
                        VacancyName=VacancyName,
                        VacancySalary=VacancySalary,
                        CompanyName=CompanyName,
                        RequiredExperience=RequiredExperience,
                        AddressCompany=AddressCompany,
                        VacancyDescription=VacancyDescription,
                        Email=Email,
                        Password=Password
                    });

                    GlobalViewModel.mode = false;

                    await Application.Current.MainPage.DisplayAlert("Регистрация завершена", "Ваша вакансия успешно создана", "Ok");
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync("..");
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Регистрация провалена",
                        "Не удалось создать резюме", "Ok");
                }
            }

        }


        [RelayCommand]
        public Task ValidateVacancyName()
        {
            if (!string.IsNullOrEmpty(VacancyName) && VacancyName.Length >= 2)
            {
                if (VacancyName.All(char.IsLetter) || (!VacancyName.All(char.IsLetter) && VacancyName.IndexOf(" ") > -1))
                {
                    IsValidVacancyName = true;
                    ShowVacancyNameErrorMessage = false;
                    EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName &&
                       IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
                }
                else
                {
                    VacancyNameErrorMessage = "*Пожалуйста, введите название профессии, состоящее не менее чем из двух букв";
                    IsValidVacancyName = false;
                    ShowVacancyNameErrorMessage = true;
                    EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName &&
                       IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
                }
            }
            else
            {
                VacancyNameErrorMessage = "*Пожалуйста, введите название профессии, состоящее не менее чем из двух букв";
                IsValidVacancyName = false;
                ShowVacancyNameErrorMessage = true;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName &&
                       IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateVacancySalary()
        {
            if (!string.IsNullOrEmpty(VacancySalary) && int.TryParse(VacancySalary, out _) && int.Parse(VacancySalary) >= 10)
            {
                IsValidVacancySalary = true;
                ShowVacancySalaryErrorMessage = false;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName &&
                       IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
            }
            else
            {
                VacancySalaryErrorMessage = "*Пожалуйста, введите желаемый доход, состоящий только из цифр и доход от 10 ₽";
                IsValidVacancySalary = false;
                ShowVacancySalaryErrorMessage = true;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName &&
                        IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateRequiredExperience()
        {
            if (!string.IsNullOrEmpty(RequiredExperience) && (int.TryParse(RequiredExperience, out _) || RequiredExperience == "0") && int.Parse(RequiredExperience) <= 80 &&
                int.Parse(RequiredExperience) >= 0)
            {
                if (RequiredExperience.Length > 1 && RequiredExperience.All(f => (f == '0')))
                {
                    RequiredExperienceErrorMessage = "*Пожалуйста, введите опыт работы, состоящий только из цифр от 0 (без опыта) до 80 лет";
                    IsValidRequiredExperience = false;
                    ShowRequiredExperienceErrorMessage = true;
                    EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
                }
                else
                {
                    IsValidRequiredExperience = true;
                    ShowRequiredExperienceErrorMessage = false;
                    EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
                }
            }
            else
            {
                RequiredExperienceErrorMessage = "*Пожалуйста, введите опыт работы, состоящий только из цифр от 0 (без опыта) до 80 лет";
                IsValidRequiredExperience = false;
                ShowRequiredExperienceErrorMessage = true;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateCompanyName()
        {
            if (!string.IsNullOrEmpty(CompanyName) && CompanyName.Length >= 2 && CompanyName.All(char.IsLetter))
            {
                IsValidCompanyName = true;
                ShowCompanyNameErrorMessage = false;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
            }
            else
            {
                CompanyNameErrorMessage = "*Пожалуйста, введите имя, состоящее не менее чем из двух букв";
                IsValidCompanyName = false;
                ShowCompanyNameErrorMessage = true;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateAddressCompany()
        {
            if (!string.IsNullOrEmpty(AddressCompany) && AddressCompany.Length >= 2 && AddressCompany.All(char.IsLetter))
            {
                IsValidAddressCompany = true;
                ShowAddressCompanyErrorMessage = false;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
            }
            else
            {
                AddressCompanyErrorMessage = "*Пожалуйста, введите фамилию, состоящее не менее чем из двух символов";
                IsValidAddressCompany = false;
                ShowAddressCompanyErrorMessage = true;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        readonly string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        [RelayCommand]
        public Task ValidateEmail()
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
                            EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;
                            return Task.CompletedTask;
                        }
                    }
                }
                else return Task.CompletedTask;

                IsValidEmail = true;
                ShowEmailErrorMessage = false;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;

            }
            else
            {
                EmailErrorMessage = "*Неверный адрес электронной почты";
                IsValidEmail = false;
                ShowEmailErrorMessage = true;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;

            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidatePassword()
        {
            if (!string.IsNullOrEmpty(Password) && Password.Length >= 8 && (Password.Any(char.IsPunctuation) || Password.Any(char.IsSymbol)) &&
                Password.Any(char.IsDigit) && Password.Any(char.IsLetter) && Password.Any(char.IsUpper) && Password.Any(char.IsLower))
            {
                IsValidPassword = true;
                ShowPasswordErrorMessage = false;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;

            }
            else
            {
                PasswordErrorMessage = "*Пароль должен содержать буквы верхнего и нижнего регистра, цифры, спецсимволы и длиной не менее 8 символов";
                IsValidPassword = false;
                ShowPasswordErrorMessage = true;
                EnableButton = IsValidVacancyName && IsValidVacancySalary && IsValidCompanyName && IsValidRequiredExperience && IsValidAddressCompany && IsValidEmail && IsValidPassword;

            }

            return Task.CompletedTask;
        }

    }
}
