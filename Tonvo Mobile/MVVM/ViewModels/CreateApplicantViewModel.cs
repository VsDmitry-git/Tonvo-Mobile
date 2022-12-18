using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    [ObservableObject]
    public partial class CreateApplicantViewModel
    {
        [ObservableProperty] private string professionName;
        [ObservableProperty] private string applicantSalary;
        [ObservableProperty] private string workExperience;
        [ObservableProperty] private string name;
        [ObservableProperty] private string secondName;
        [ObservableProperty] private string age;  
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;

        [ObservableProperty] private string professionNameErrorMessage;
        [ObservableProperty] private string applicantSalaryErrorMessage;
        [ObservableProperty] private string workExperienceErrorMessage;
        [ObservableProperty] private string nameErrorMessage;
        [ObservableProperty] private string secondNameErrorMessage;
        [ObservableProperty] private string ageErrorMessage;
        [ObservableProperty] private string emailErrorMessage;
        [ObservableProperty] private string passwordErrorMessage;

        [ObservableProperty] private bool showProfessionNameErrorMessage;
        [ObservableProperty] private bool showApplicantSalaryErrorMessage;
        [ObservableProperty] private bool showWorkExperienceErrorMessage;
        [ObservableProperty] private bool showNameErrorMessage;
        [ObservableProperty] private bool showSecondNameErrorMessage;
        [ObservableProperty] private bool showAgeErrorMessage;
        [ObservableProperty] private bool showEmailErrorMessage;
        [ObservableProperty] private bool showPasswordErrorMessage;

        [ObservableProperty] private bool enableButton;

        [ObservableProperty] private bool isValidProfessionName;
        [ObservableProperty] private bool isValidApplicantSalary;
        [ObservableProperty] private bool isValidWorkExperience;
        [ObservableProperty] private bool isValidName;
        [ObservableProperty] private bool isValidSecondName;
        [ObservableProperty] private bool isValidAge;
        [ObservableProperty] private bool isValidEmail;
        [ObservableProperty] private bool isValidPassword;

        [ObservableProperty] private ObservableCollection<Applicant> applicants;

        public CreateApplicantViewModel()
        {
            Applicants = GlobalViewModel.Applicants;
        }


        [RelayCommand]
        async Task SignUp()
        {
            if (EnableButton)
            {
                try
                {
                    //await accountService.CreateAccount(accountCreateRequest);
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
        public Task ValidateProfessionName()
        {
            if (!string.IsNullOrEmpty(ProfessionName) && ProfessionName.Length >= 2)
            {
                if (ProfessionName.All(char.IsLetter) || (!ProfessionName.All(char.IsLetter) && ProfessionName.IndexOf(" ") > -1))
                {
                    IsValidProfessionName = true;
                    ShowProfessionNameErrorMessage = false;
                    EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                       IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
                }
                else
                {
                    ProfessionNameErrorMessage = "*Пожалуйста, введите название профессии, состоящее не менее чем из двух букв";
                    IsValidProfessionName = false;
                    ShowProfessionNameErrorMessage = true;
                    EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                      IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
                }
            }
            else
            {
                ProfessionNameErrorMessage = "*Пожалуйста, введите название профессии, состоящее не менее чем из двух букв";
                IsValidProfessionName = false;
                ShowProfessionNameErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateApplicantSalary()
        {
            if (!string.IsNullOrEmpty(ApplicantSalary) && int.TryParse(ApplicantSalary, out _) && int.Parse(ApplicantSalary) >= 10)
            {
                IsValidApplicantSalary = true;
                ShowApplicantSalaryErrorMessage = false;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                   IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }
            else
            {
                ApplicantSalaryErrorMessage = "*Пожалуйста, введите желаемый доход, состоящий только из цифр и доход от 10 ₽";
                IsValidApplicantSalary = false;
                ShowApplicantSalaryErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateWorkExperience()
        {
            if (!string.IsNullOrEmpty(WorkExperience) && int.TryParse(WorkExperience, out _) && int.Parse(WorkExperience) <= 80 &&
                int.Parse(WorkExperience) >= 0 && !WorkExperience.All(f => (f == '0')))
            {
                IsValidWorkExperience = true;
                ShowWorkExperienceErrorMessage = false;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                   IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }
            else
            {
                WorkExperienceErrorMessage = "*Пожалуйста, введите опыт работы, состоящий только из цифр от 0 (без опыта) до 80 лет";
                IsValidWorkExperience = false;
                ShowWorkExperienceErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateName()
        {
            if (!string.IsNullOrEmpty(Name) && Name.Length >= 2 && Name.All(char.IsLetter))
            {
                IsValidName = true;
                ShowNameErrorMessage = false;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }
            else
            {
                NameErrorMessage = "*Пожалуйста, введите имя, состоящее не менее чем из двух букв";
                IsValidName = false;
                ShowNameErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateSecondName()
        {
            if (!string.IsNullOrEmpty(SecondName) && SecondName.Length >= 2 && SecondName.All(char.IsLetter))
            {
                IsValidSecondName = true;
                ShowSecondNameErrorMessage = false;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                   IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }
            else
            {
                SecondNameErrorMessage = "*Пожалуйста, введите фамилию, состоящее не менее чем из двух символов";
                IsValidSecondName = false;
                ShowSecondNameErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateAge()
        {
            if (!string.IsNullOrEmpty(Age) && int.TryParse(Age, out _) && int.Parse(Age) >= 14 && int.Parse(Age) <= 100)
            {
                IsValidAge = true;
                ShowAgeErrorMessage = false;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                   IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }
            else
            {
                AgeErrorMessage = "*Пожалуйста, введите свой возраст, зарегистрироваться можно с 14 лет";
                IsValidAge = false;
                ShowAgeErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        [RelayCommand]
        public Task ValidateEmail()
        {
            if (!string.IsNullOrEmpty(Email) && Regex.IsMatch(Email, emailPattern))
            {
                if (Applicants != null)
                {
                    foreach (Applicant item in Applicants)
                    {
                        if (Email.Equals(item.Email))
                        {
                            EmailErrorMessage = "*Эта почта уже занята. Попробуйте другую";
                            IsValidEmail = false;
                            ShowEmailErrorMessage = true;
                            EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                              IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;
                            return Task.CompletedTask;
                        }
                    }
                }
                else return Task.CompletedTask;

                IsValidEmail = true;
                ShowEmailErrorMessage = false;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;

            }
            else
            {
                EmailErrorMessage = "*Неверный адрес электронной почты";
                IsValidEmail = false;
                ShowEmailErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;

            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidatePassword()
        {
            if (!string.IsNullOrEmpty(Password) && Password.Length >= 8 && Password.Any(char.IsPunctuation) &&
                Password.Any(char.IsDigit) && Password.Any(char.IsLetter) && Password.Any(char.IsUpper) && Password.Any(char.IsLower))
            {
                IsValidPassword = true;
                ShowPasswordErrorMessage = false;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;

            }
            else
            {
                PasswordErrorMessage = "*Пароль должен содержать буквы верхнего и нижнего регистра, цифры, спецсимволы и длиной не менее 8 символов";
                IsValidPassword = false;
                ShowPasswordErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidAge && IsValidEmail && IsValidPassword;

            }

            return Task.CompletedTask;
        }


    }
}
