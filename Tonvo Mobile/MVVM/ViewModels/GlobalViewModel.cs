using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tonvo_Mobile.MVVM.Modelss;

namespace Tonvo_Mobile.MVVM.ViewModels
{
    internal static class GlobalViewModel
    {
        public static bool mode;
        public static ObservableCollection<Applicant> Applicants { get; set; }
        public static ObservableCollection<Vacancy> Vacancies { get; set; }

        public static Applicant CurrentApplicant { get; set; }
        public static Applicant CurrentVacancy { get; set; }
    }
}
