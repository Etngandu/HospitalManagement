using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Collections
{
    /// <summary>
    /// Represents a collection of Diagnoses instances in the system.
    /// </summary>
    public class Diagnoses : CollectionBase<Diagnose>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beds"/> class.
        /// </summary>
        public Diagnoses() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patients"/> class.
        /// </summary>
        /// <param name="initialList">Accepts an IList of Person as the initial list.</param>
        public Diagnoses(IList<Diagnose> initialList) : base(initialList) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patients"/> class.
        /// </summary>
        /// <param name="initialList">Accepts a CollectionBase of Person as the initial list.</param>
        public Diagnoses(CollectionBase<Diagnose> initialList) : base(initialList) { }

        /// <summary>
        /// Adds a new instance of Diagnoses to the collection.
        /// </summary>
        /// <param name="emailAddressText">The e-mail address text.</param>
        /// <param name="contactType">The type of the e-mail address.</param>
        public void Add(string diagdetail, Patient patient, Staff staff)
        {
            Add(new Diagnose { Diagnose_details=diagdetail, Owner_Patient=patient, Owner_Staff=staff});
        }

        /// <summary>
        /// Validates the current collection by validating each individual item in the collection.
        /// </summary>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var diag in this)
            {
                errors.AddRange(diag.Validate());
            }
            return errors;
        }
    }
}
