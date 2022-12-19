using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tonvo.Services;
using Tonvo_Mobile.MVVM.Modelss;
using System.Text.RegularExpressions;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    [ObservableObject]
    public partial class AccountViewModel
    {
        [ObservableProperty]
        private Applicant currentApplicant;
        [ObservableProperty]
        private Applicant currentVacancy;

        [ObservableProperty]
        private ObservableCollection<Applicant> applicants;

        [ObservableProperty]
        private bool modeA;
        [ObservableProperty]
        private bool modeV;

        public void Init()
        {
            try
            {
                ModeA = GlobalViewModel.mode;
                ModeV = !GlobalViewModel.mode;

                if (GlobalViewModel.mode)
                {
                    Applicants = GlobalViewModel.Applicants;
                    currentApplicant = GlobalViewModel.CurrentApplicant;
                }
                else
                {
                    currentVacancy = GlobalViewModel.CurrentVacancy;
                }
            }
            catch (Exception)
            {
                // TODO: logging
                Debug.WriteLine("Unable to retrieve list");
            }
        }

        public AccountViewModel()
        {

        }

        #region Applicant
        [ObservableProperty]
        private DateTime dateStart = DateTime.Now.AddYears(-100);
        [ObservableProperty]
        private DateTime dateEnd = DateTime.Now.AddYears(-14);

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string professionName;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string applicantSalary;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string workExperience;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string name;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string secondName;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string birthday;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string email;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string password;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangeACommand))]
        private string applicantDescription;

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

        [RelayCommand]
        public async Task SaveChangeA()
        {
            if (EnableButton)
            {
                try
                {
                    DataStorage.ApplicantAccountChange(new Applicant
                    {
                        Id = CurrentApplicant.Id,
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

                    await Application.Current.MainPage.DisplayAlert("Сохранение завершено", "Ваше резюме успешно сохранено", "Ok");
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Сохранение провалено",
                        "Не удалось сохранить резюме", "Ok");
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
            if (!string.IsNullOrEmpty(WorkExperience) && (int.TryParse(WorkExperience, out _) || WorkExperience == "0") && int.Parse(WorkExperience) <= 80 &&
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

        #endregion Applicant

    }

}   
