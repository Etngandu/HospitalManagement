using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Collections
{
    /// <summary>
    /// Represents a collection of Doctor_Assigned_to_Patients instances in the system.
    /// </summary>
      public  class Doctor_Assigned_to_Patients: CollectionBase<Doctor_Assigned_to_Patient>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beds"/> class.
        /// </summary>
        public Doctor_Assigned_to_Patients() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patients"/> class.
        /// </summary>
        /// <param name="initialList">Accepts an IList of Person as the initial list.</param>
        public Doctor_Assigned_to_Patients(IList<Doctor_Assigned_to_Patient> initialList) : base(initialList) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patients"/> class.
        /// </summary>
        /// <param name="initialList">Accepts a CollectionBase of Person as the initial list.</param>
        public Doctor_Assigned_to_Patients(CollectionBase<Doctor_Assigned_to_Patient> initialList) : base(initialList) { }

        /// <summary>
        /// Adds a new instance of EmailAddress to the collection.
        /// </summary>
        /// <param name="emailAddressText">The e-mail address text.</param>
        /// <param name="contactType">The type of the e-mail address.</param>
        public void Add(DateTime datefrom, DateTime dateto)
        {
            Add(new Doctor_Assigned_to_Patient { Date_Ass_from = datefrom, Date_Ass_to = dateto });
        }

        /// <summary>
        /// Validates the current collection by validating each individual item in the collection.
        /// </summary>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var doctor_Assigned_To in this)
            {
                errors.AddRange(doctor_Assigned_To.Validate());
            }
            return errors;
        }
    }
}
