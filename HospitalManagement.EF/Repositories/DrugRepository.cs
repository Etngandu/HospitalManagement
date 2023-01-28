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
    public class DrugRepository : Repository<Drug>, IDrugRepository
    {
        /// <summary>
        /// Gets a list of all the people whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        public IEnumerable<Drug> FindByName(string drugname)
        {
            return DataContextFactory.GetDataContext().Set<Drug>().Where(x => x.Drug_name == drugname);
        }

        /// <summary>
        /// Gets a list of all the people whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Adress with the matching Ad.</returns>
        public IEnumerable<Patient_Drug_Treatment> FindById(int id_drug)
        {
            return DataContextFactory.GetDataContext().Set<Patient_Drug_Treatment>().Where(x => x.Owner_DrugId == id_drug);
        }


    }
}
