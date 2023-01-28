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
    public class WardRepository : Repository<Ward>, IWardRepository
    {
        /// <summary>
        /// Gets a list of all the people whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        public IEnumerable<Ward> FindByName(string wardname)
        {
            return DataContextFactory.GetDataContext().Set<Ward>().Where(x => x.Ward_Name == wardname);
        }

        /// <summary>
        /// Gets a list of all the people whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Adress with the matching Ad.</returns>
        public IEnumerable<Bed> FindById(int id_bed)
        {
            return DataContextFactory.GetDataContext().Set<Bed>().Where(x => x.Owner.Id == id_bed);
        }


    }
}
