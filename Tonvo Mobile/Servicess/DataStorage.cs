using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo.Services
{
    internal static class DataStorage
    {
        private const string _vacancyDSNameFile = "vacancydatastorage.json";
        private const string _companyDSNameFile = "companydatastorage.json";
        private const string _applicantDSNameFile = "applicantdatastorage.json";

        private static string _pathToSaveApplicant;
        private static string _pathToSaveCompany;
        private static string _pathToSaveVacancy;
        private static string _currentPath;

        private static ObservableCollection<Applicant> Applicants { get; set; } = new ObservableCollection<Applicant>();
        private static ObservableCollection<Vacancy> Vacancies { get; set; } = new ObservableCollection<Vacancy>();
        private static ObservableCollection<Company> Companies { get; set; } = new ObservableCollection<Company>();

        public static string CurrentPath(IModel acc)
        {
            try
            {
                return acc.GetType().ToString() switch
                {
                    "Tonvo.MVVM.Models.Applicant" => _currentPath = _pathToSaveApplicant,
                    "Tonvo.MVVM.Models.Company" => _currentPath = _pathToSaveCompany,
                    "Tonvo.MVVM.Models.Vacancy" => _currentPath = _pathToSaveVacancy,
                    _ => throw new Exception("Некоректный тип входных данных"),
                };
            }
            catch { return null; }
        }

        // Создание файла dataStorage.json
        public static async Task Init()
        {
            _pathToSaveApplicant = Path.Combine(FileSystem.Current.AppDataDirectory, _applicantDSNameFile);
            if (File.Exists(_pathToSaveApplicant))
            {
                _currentPath = _pathToSaveApplicant;
                Applicants = JsonConvert.DeserializeObject<ObservableCollection<Applicant>>(File.ReadAllText(_currentPath));
            }

            _pathToSaveCompany = Path.Combine(FileSystem.Current.AppDataDirectory, _companyDSNameFile);
            if (File.Exists(_pathToSaveCompany))
            {
                _currentPath = _pathToSaveCompany;
                Companies = JsonConvert.DeserializeObject<ObservableCollection<Company>>(File.ReadAllText(_currentPath));
            }

            _pathToSaveVacancy = Path.Combine(FileSystem.Current.AppDataDirectory, _vacancyDSNameFile);

            Vacancies = ReadVacancyJson();
        }

        // команда добавления нового объекта
        public async static void AddApplicant(Applicant acc)
        {
            try
            {
                _currentPath = _pathToSaveApplicant;
                Applicants = ReadApplicantsJson();
                Applicants.Insert(0, acc);
                //SaveDataList(Applicants);
            }
            catch (Exception) { }
        }
        public async static Task AddVacancy(Vacancy acc)
        {
            _currentPath = _pathToSaveVacancy;
            Vacancies = ReadVacancyJson();
            Vacancies.Insert(0, acc);
            SaveDataList(Vacancies);
        }
        public async static void AddCompany(Company acc)
        {
            try
            {
                _currentPath = _pathToSaveCompany;
                Companies = ReadCompanyJson();
                Companies.Insert(0, acc);
                //SaveDataList(Companies);
            }
            catch (Exception) { }
        }

        // Команда удаления
        public static void Remove()
        {
            
        }

        // Чтение файла
        public async static void ReadJson() { await Init(); }

        public static ObservableCollection<Applicant> ReadApplicantsJson()
        {
            _pathToSaveApplicant = Path.Combine(FileSystem.Current.AppDataDirectory, _applicantDSNameFile);
            if (File.Exists(_pathToSaveApplicant))
                return Applicants = JsonConvert.DeserializeObject<ObservableCollection<Applicant>>(File.ReadAllText(_pathToSaveApplicant));
            else
                File.WriteAllText(_pathToSaveApplicant, "[]");
            return Applicants = JsonConvert.DeserializeObject<ObservableCollection<Applicant>>(File.ReadAllText(_pathToSaveApplicant));
        }
        public static ObservableCollection<Vacancy> ReadVacancyJson()
        {
            _pathToSaveVacancy = Path.Combine(FileSystem.Current.AppDataDirectory, _vacancyDSNameFile);
            _currentPath= _pathToSaveVacancy;
            //File.Delete(_pathToSaveVacancy);
            if (!File.Exists(_currentPath))
            {
                string targetFile = _currentPath;
                ObservableCollection<Vacancy> vacancies = new ObservableCollection<Vacancy>();
                File.WriteAllText(targetFile, JsonConvert.SerializeObject(vacancies));
                return vacancies;
            }
            else
            {
                string targetFile = _currentPath;
                var Vacancies = JsonConvert.DeserializeObject<ObservableCollection<Vacancy>>(File.ReadAllText(targetFile));
                Vacancies.Add(new Vacancy { CompanyName = "hbj", VacancyName = "kjn", VacancySalary = "hjb" , AddressCompany="hjb", Email="jhj", Password="hb", RequiredExperience="jhbj"});
                SaveDataList(Vacancies);
                return Vacancies;
            }
        }

        // Сохранение данных
        public static void SaveDataList(ObservableCollection<Vacancy> accs)
        {
            File.WriteAllText(_currentPath, JsonConvert.SerializeObject(accs));
            //using FileStream outputSteram = File.OpenWrite(_currentPath);
            //using var streamWriter = new StreamWriter(outputSteram);
            
            //await streamWriter.WriteAsync(JsonConvert.SerializeObject(accs, Formatting.Indented));

            //await File.WriteAllTextAsync(_currentPath, JsonConvert.SerializeObject(accs, Formatting.Indented));
        }

        public static ObservableCollection<Vacancy> GetVacancy()
        {
            return Vacancies;
        }
        public static ObservableCollection<Company> ReadCompanyJson()
        {
            return Companies;
        }

        // Конвертация списка
        public static ObservableCollection<IModel> ConvertListFromFile<T>(ObservableCollection<T> jsonlist)
        {
            ObservableCollection<IModel> convertedList = new();
            foreach (var item in jsonlist)
            {
                IModel obj = (IModel)item;
                convertedList.Add(obj);
            }
            return convertedList;
        }

        // Изменение объекта
        public async static void ApplicantAccountChange(Applicant replace)
        {
            int i;
            CurrentPath(replace);
            if (_currentPath == _pathToSaveApplicant)
            {
                ObservableCollection<Applicant> readed = JsonConvert.DeserializeObject<ObservableCollection<Applicant>>(File.ReadAllText(_currentPath));
                foreach (var item in readed)
                {
                    if (item.Id.Equals(replace.Id))
                    {
                        i = readed.IndexOf(item);
                        readed[i] = replace;
                        //SaveDataList(readed);
                        return;
                    }
                }
            }
        }
        public async static void VacancyAccountChange(Vacancy replace)
        {
            int i;
            CurrentPath(replace);
            if (_currentPath == _pathToSaveVacancy)
            {
                ObservableCollection<Vacancy> readed = JsonConvert.DeserializeObject<ObservableCollection<Vacancy>>(File.ReadAllText(_currentPath));
                foreach (var item in readed)
                {
                    if (item.Id.Equals(replace.Id))
                    {
                        i = readed.IndexOf(item);
                        readed[i] = replace;
                        SaveDataList(readed);
                        return;
                    }
                }
            }
        }
    }
}
