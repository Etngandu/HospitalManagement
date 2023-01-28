using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Repositories;

namespace HospitalManagement.EF.Repositories
{
    

    /// <summary>
    /// A concrete repository to work with patients in the system.
    /// </summary>
    public class DiagnoseRepository : Repository<Diagnose>, IDiagnoseRepository
    {
        /// <summary>
        /// Gets a list of all the people whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        public IEnumerable<Diagnose> FindByName(string dgdetail)
        {
            return DataContextFactory.GetDataContext().Set<Diagnose>().Where(x => x.Diagnose_details == dgdetail);
        }

        /// <summary>
        /// Gets a list of all the people whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Adress with the matching Ad.</returns>
        public IEnumerable<Patient_Drug_Treatment> FindById(int id_diag)
        {
            return DataContextFactory.GetDataContext().Set<Patient_Drug_Treatment>().Where(x => x.Owner_DiagnoseId == id_diag);
        }


    }
}
