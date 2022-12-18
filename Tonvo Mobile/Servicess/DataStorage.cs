using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo.Services
{
    internal static class DataStorage
    {
        // TODO: Try add async

        // FileName
        private const string _vacancyDSNameFile = "vacancydatastorage.json";
        private const string _companyDSNameFile = "companydatastorage.json";
        private const string _applicantDSNameFile = "applicantdatastorage.json";

        // FullPath
        private static string _pathToSaveApplicant;
        private static string _pathToSaveCompany;
        private static string _pathToSaveVacancy;

        private static string _currentPath;

        // Lists
        private static ObservableCollection<Applicant> Applicants { get; set; } = new();
        private static ObservableCollection<Vacancy> Vacancies { get; set; } = new();
        private static ObservableCollection<Company> Companies { get; set; } = new();

        // Init
        public static void Init()
        {
            _pathToSaveApplicant = Path.Combine(FileSystem.Current.AppDataDirectory, _applicantDSNameFile);
            _pathToSaveVacancy = Path.Combine(FileSystem.Current.AppDataDirectory, _vacancyDSNameFile);
            _pathToSaveCompany = Path.Combine(FileSystem.Current.AppDataDirectory, _companyDSNameFile);

            Applicants = ReadApplicantsJson();
            Companies = ReadCompanyJson();
            Vacancies = ReadVacancyJson();
        }

        // Add Commands
        public static void AddApplicant(Applicant acc)
        {
            _currentPath = _pathToSaveApplicant;
            Applicants.Insert(0, acc);
            SaveApplicantList(Applicants);
        }
        public static void AddVacancy(Vacancy acc)
        {
            _currentPath = _pathToSaveVacancy;
            Vacancies.Insert(0, acc);
            SaveVacancyList(Vacancies);
        }
        public static void AddCompany(Company acc)
        {
            _currentPath = _pathToSaveCompany;
            Companies.Insert(0, acc);
            SaveCompanyList(Companies);
        }

        // Remove Command
        public static void Remove()
        {
            // TODO: Command Remove
        }

        // Read JSON
        private static ObservableCollection<Applicant> ReadApplicantsJson()
        {
            _currentPath = _pathToSaveApplicant;
            if (!File.Exists(_currentPath))
            {
                ObservableCollection<Applicant> applicants = new ();
                File.WriteAllText(_currentPath, JsonConvert.SerializeObject(applicants));
                return applicants;
            }
            else
            {
                Applicants = JsonConvert.DeserializeObject<ObservableCollection<Applicant>>(File.ReadAllText(_currentPath));
                return Applicants;
            }
        }
        private static ObservableCollection<Vacancy> ReadVacancyJson()
        {
            _currentPath= _pathToSaveVacancy;
            if (!File.Exists(_currentPath))
            {
                ObservableCollection<Vacancy> vacancies = new();
                File.WriteAllText(_currentPath, JsonConvert.SerializeObject(vacancies));
                return vacancies;
            }
            else
            {
                Vacancies = JsonConvert.DeserializeObject<ObservableCollection<Vacancy>>(File.ReadAllText(_currentPath));
                return Vacancies;
            }
        }
        private static ObservableCollection<Company> ReadCompanyJson()
        {
            _currentPath = _pathToSaveCompany;
            if (!File.Exists(_currentPath))
            {
                ObservableCollection<Company> companies = new();
                File.WriteAllText(_currentPath, JsonConvert.SerializeObject(companies));
                return companies;
            }
            else
            {
                Companies = JsonConvert.DeserializeObject<ObservableCollection<Company>>(File.ReadAllText(_currentPath));
                return Companies;
            }
        }

        // Save
        public static void SaveApplicantList(ObservableCollection<Applicant> accs) => File.WriteAllText(_currentPath, JsonConvert.SerializeObject(accs));
        public static void SaveVacancyList(ObservableCollection<Vacancy> accs) => File.WriteAllText(_currentPath, JsonConvert.SerializeObject(accs));
        public static void SaveCompanyList(ObservableCollection<Company> accs) => File.WriteAllText(_currentPath, JsonConvert.SerializeObject(accs));

        // Get List
        public static ObservableCollection<Applicant> GetApplicants() => Applicants;
        public static ObservableCollection<Vacancy> GetVacancies() => Vacancies;
        public static ObservableCollection<Company> GetCompanies() => Companies;

        // Change User
        public static void ApplicantAccountChange(Applicant replace)
        {
            _currentPath = _pathToSaveApplicant;
            foreach (var item in Applicants)
            {
                if (item.Id.Equals(replace.Id))
                {
                    int i = Applicants.IndexOf(item);
                    Applicants[i] = replace;
                    SaveApplicantList(Applicants);
                    return;
                }
            }
        }
        public static void VacancyAccountChange(Vacancy replace)
        {
            _currentPath = _pathToSaveVacancy;
            foreach (var item in Vacancies)
            {
                if (item.Id.Equals(replace.Id))
                {
                    int i = Vacancies.IndexOf(item);
                    Vacancies[i] = replace;
                    SaveVacancyList(Vacancies);
                    return;
                }
            }
        }

        public static void CompanyAccountChange(Company replace)
        {
            _currentPath = _pathToSaveCompany;
            foreach (var item in Companies)
            {
                if (item.Id.Equals(replace.Id))
                {
                    int i = Companies.IndexOf(item);
                    Companies[i] = replace;
                    SaveCompanyList(Companies);
                    return;
                }
            }
        }
    }
}
