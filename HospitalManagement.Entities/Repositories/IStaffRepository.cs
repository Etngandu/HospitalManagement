using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Repositories
{
    /// <summary>
    /// Defines the various methods available to work with Staff member in the system.
    /// </summary>
    public interface IStaffRepository : IRepository<Staff, int>
    {
        /// <summary>
        /// Gets a list of all the staff member whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Staffmembers with the matching staffmember.</returns>
        IEnumerable<Staff> FindByName(string Staff_last_name);
       // IEnumerable<Address> FindById(int staffid);
    }
}
