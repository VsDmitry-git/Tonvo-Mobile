using System.Collections.ObjectModel;

namespace Tonvo.MVVM.Models
{
    public class Vacancy
    {
        #region Properties
        public int Id { get; set; }
        public int IdCompany { get; set; }
        public string VacancyName { get; set; }
        public string VacancySalary { get; set; }
        public string CompanyName { get; set; }
        public string RequiredExperience { get; set; }
        public string AddressCompany { get; set; }
        public string VacancyDescription { get; set; }
        public string Email { get; set; }
        // TODO: Убрать после добавления аккаунта компании
        public string Password { get; set; }
        public ObservableCollection<int> Responds { get; set; } = new ObservableCollection<int>();
        #endregion Properties

        public Vacancy()
        {
        }
    }
}
