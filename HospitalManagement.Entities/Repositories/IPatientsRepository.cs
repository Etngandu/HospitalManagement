using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Repositories
{
    /// <summary>
    /// Defines the various methods available to work with patients in the system.
    /// </summary>
    public interface IPatientsRepository : IRepository<Patient, int>
    {
        /// <summary>
        /// Gets a list of all the patients whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Patients with the matching patient.</returns>
        IEnumerable<Patient> FindByName(string Patient_last_name);
    }
}
