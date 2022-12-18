using System.Collections.ObjectModel;

namespace Tonvo_Mobile.MVVM.Modelss
{
    public class Applicant : IModel
    {
        #region Properties
        public int Id { get; set; }
        public string ProfessionName { get; set; }
        public string ApplicantSalary { get; set; }
        public string WorkExperience { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string FullName { get; set; }
        public string ApplicantDescription { get; set; }
        public string Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ObservableCollection<int> Responds { get; set; } = new ObservableCollection<int>();
        #endregion Properties

        public Applicant()
        {
        }
    }
}
