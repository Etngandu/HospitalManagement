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
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
        /// <summary>
        /// Gets a list of all the people whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        public IEnumerable<Staff> FindByName(string lastname)
        {
            return DataContextFactory.GetDataContext().Set<Staff>().Where(x => x.Staff_last_name == lastname);
        }

        /// <summary>
        /// Gets a list of all the people whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Adress with the matching Ad.</returns>
        public IEnumerable<Address> FindById(int id_staff)
        {
            return DataContextFactory.GetDataContext().Set<Address>().Where(x => x.Owner_Staff.Id == id_staff);
        }


    }
}
