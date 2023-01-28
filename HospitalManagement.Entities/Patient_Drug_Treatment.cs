using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities
{
  public class Patient_Drug_Treatment : DomainEntity<int>, IDateTracking
    {
        /// <summary>
        /// Gets or sets the owner Patient.
        /// </summary>
        [Owner("")]
        public Patient Owner_Patient { get; set; }

        /// <summary>
        /// Gets or sets the owner Staff.
        /// </summary>
        [Owner("")]
        public Diagnose Owner_Diagnose { get; set; }

        /// <summary>
        /// Gets or sets the owner Staff.
        /// </summary>
        [Owner("")]
        public Drug Owner_Drug { get; set; }


        /// <summary>
        /// Gets or sets the ID of the owner (a Staff member) of the...
        /// </summary>
        public int? Owner_PatientId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the owner (diagnose) of the...
        /// </summary>
        public int Owner_DiagnoseId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the owner (drug) of the...
        /// </summary>
        public int Owner_DrugId { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets Dosage_administred
        /// </summary>

        public string Dosage_administred { get; set; }

        /// <summary>
        /// Gets or sets Comments
        /// </summary>

        public string Comments { get; set; }


        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {            

            if (string.IsNullOrEmpty(Dosage_administred))
            {
                yield return new ValidationResult("Drug_name can't be null", new[] { "Drug_name" });
            }           


        }
        #endregion
    }
}
