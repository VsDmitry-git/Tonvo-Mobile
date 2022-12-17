using System.ComponentModel.DataAnnotations;

namespace Tonvo_Mobile.MVVM.Modelss
{
    public class Company : IModel
    {
        #region Properties
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string AddressCompany { get; set; }
        public string VacancyDescription { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        #endregion Properties

        public Company()
        {
        }
    }
}
