using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Tonvo.Services;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    [ObservableObject]
    public partial class CreateApplicantViewModel
    {
        [ObservableProperty] private DateTime dateStart = DateTime.Now.AddYears(-100);
        [ObservableProperty] private DateTime dateEnd = DateTime.Now.AddYears(-14);

        [ObservableProperty] private string professionName;
        [ObservableProperty] private string applicantSalary;
        [ObservableProperty] private string workExperience;
        [ObservableProperty] private string name;
        [ObservableProperty] private string secondName;
        [ObservableProperty] private string birthday;  
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;
        [ObservableProperty] private string applicantDescription;

        [ObservableProperty] private string professionNameErrorMessage;
        [ObservableProperty] private string applicantSalaryErrorMessage;
        [ObservableProperty] private string workExperienceErrorMessage;
        [ObservableProperty] private string nameErrorMessage;
        [ObservableProperty] private string secondNameErrorMessage;
        [ObservableProperty] private string birthdayErrorMessage;
        [ObservableProperty] private string emailErrorMessage;
        [ObservableProperty] private string passwordErrorMessage;

        [ObservableProperty] private bool showProfessionNameErrorMessage;
        [ObservableProperty] private bool showApplicantSalaryErrorMessage;
        [ObservableProperty] private bool showWorkExperienceErrorMessage;
        [ObservableProperty] private bool showNameErrorMessage;
        [ObservableProperty] private bool showSecondNameErrorMessage;
        [ObservableProperty] private bool showBirthdayErrorMessage;
        [ObservableProperty] private bool showEmailErrorMessage;
        [ObservableProperty] private bool showPasswordErrorMessage;

        [ObservableProperty] private bool enableButton;

        [ObservableProperty] private bool isValidProfessionName;
        [ObservableProperty] private bool isValidApplicantSalary;
        [ObservableProperty] private bool isValidWorkExperience;
        [ObservableProperty] private bool isValidName;
        [ObservableProperty] private bool isValidSecondName;
        [ObservableProperty] private bool isValidBirthday = true;
        [ObservableProperty] private bool isValidEmail;
        [ObservableProperty] private bool isValidPassword;

        [ObservableProperty] private ObservableCollection<Applicant> applicants;

        public CreateApplicantViewModel()
        {
            Applicants = GlobalViewModel.Applicants;
        }


        [RelayCommand]
        public async Task SignUp()
        {
            if (EnableButton)
            {
                try
                {
                    DataStorage.AddApplicant(new Applicant
                    {
                        Id = Applicants.Count != 0 ? Applicants[0].Id + 1 : 0,
                        ProfessionName = ProfessionName,
                        ApplicantSalary = ApplicantSalary,
                        WorkExperience = WorkExperience,
                        Name = Name,
                        SecondName = SecondName,
                        Birthday = Birthday.Remove(Birthday.IndexOf(' ')),
                        Email = Email,
                        Password = Password,
                        ApplicantDescription = ApplicantDescription
                    });

                    GlobalViewModel.mode = true;
                    
                    await Application.Current.MainPage.DisplayAlert("Регистрация завершена", "Ваше резюме успешно создано", "Ok");
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
        public Task ValidateProfessionName()
        {
            if (!string.IsNullOrEmpty(ProfessionName) && ProfessionName.Length >= 2)
            {
                if (ProfessionName.All(char.IsLetter) || (!ProfessionName.All(char.IsLetter) && ProfessionName.IndexOf(" ") > -1))
                {
                    IsValidProfessionName = true;
                    ShowProfessionNameErrorMessage = false;
                    EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                       IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
                }
                else
                {
                    ProfessionNameErrorMessage = "*Пожалуйста, введите название профессии, состоящее не менее чем из двух букв";
                    IsValidProfessionName = false;
                    ShowProfessionNameErrorMessage = true;
                    EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                      IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
                }
            }
            else
            {
                ProfessionNameErrorMessage = "*Пожалуйста, введите название профессии, состоящее не менее чем из двух букв";
                IsValidProfessionName = false;
                ShowProfessionNameErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
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
                   IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
            }
            else
            {
                ApplicantSalaryErrorMessage = "*Пожалуйста, введите желаемый доход, состоящий только из цифр и доход от 10 ₽";
                IsValidApplicantSalary = false;
                ShowApplicantSalaryErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ValidateWorkExperience()
        {
            if (!string.IsNullOrEmpty(WorkExperience) && (int.TryParse(WorkExperience, out _) || WorkExperience=="0") && int.Parse(WorkExperience) <= 80 &&
                int.Parse(WorkExperience) >= 0)
            {
                if (WorkExperience.Length > 1 && WorkExperience.All(f => (f == '0')))
                {
                    WorkExperienceErrorMessage = "*Пожалуйста, введите опыт работы, состоящий только из цифр от 0 (без опыта) до 80 лет";
                    IsValidWorkExperience = false;
                    ShowWorkExperienceErrorMessage = true;
                    EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                      IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
                }
                else
                {
                    IsValidWorkExperience = true;
                    ShowWorkExperienceErrorMessage = false;
                    EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                    IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
                }
            }
            else
            {
                WorkExperienceErrorMessage = "*Пожалуйста, введите опыт работы, состоящий только из цифр от 0 (без опыта) до 80 лет";
                IsValidWorkExperience = false;
                ShowWorkExperienceErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
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
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
            }
            else
            {
                NameErrorMessage = "*Пожалуйста, введите имя, состоящее не менее чем из двух букв";
                IsValidName = false;
                ShowNameErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
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
                   IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
            }
            else
            {
                SecondNameErrorMessage = "*Пожалуйста, введите фамилию, состоящее не менее чем из двух символов";
                IsValidSecondName = false;
                ShowSecondNameErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
            }

            return Task.CompletedTask;
        }

        readonly string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

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
                              IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;
                            return Task.CompletedTask;
                        }
                    }
                }
                else return Task.CompletedTask;

                IsValidEmail = true;
                ShowEmailErrorMessage = false;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;

            }
            else
            {
                EmailErrorMessage = "*Неверный адрес электронной почты";
                IsValidEmail = false;
                ShowEmailErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;

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
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;

            }
            else
            {
                PasswordErrorMessage = "*Пароль должен содержать буквы верхнего и нижнего регистра, цифры, спецсимволы и длиной не менее 8 символов";
                IsValidPassword = false;
                ShowPasswordErrorMessage = true;
                EnableButton = IsValidProfessionName && IsValidApplicantSalary && IsValidWorkExperience &&
                  IsValidName && IsValidSecondName && isValidBirthday && IsValidEmail && IsValidPassword;

            }

            return Task.CompletedTask;
        }


    }
}
