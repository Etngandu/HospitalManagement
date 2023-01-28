﻿using System;
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
    public class PatientsRepository : Repository<Patient>, IPatientsRepository
    {
        /// <summary>
        /// Gets a list of all the patients whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        public IEnumerable<Patient> FindByName(string lastname)
        {
            return DataContextFactory.GetDataContext().Set<Patient>().Where(x => x.Patient_last_name == lastname);
        }
    }
}
