using HospitalManagement.Entities.Collections;
using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities
{
  public class Diagnose: DomainEntity<int>, IDateTracking
    {


        #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the Person class.
        ///// </summary>
        public Diagnose()
        {
            Patient_Drug_Treatments = new Patient_Drug_Treatments();            
        }

        #endregion
        /// <summary>
        /// Gets or sets the owner Patient.
        /// </summary>
        [Owner("key_patient")]
        public Patient Owner_Patient { get; set; }

        /// <summary>
        /// Gets or sets the owner Staff.
        /// </summary>
        [Owner("")]
        public Staff Owner_Staff { get; set; }


        /// <summary>
        /// Gets or sets the ID of the owner (a Patient) of the diagnose.
        /// </summary>
        public int Owner_PatientId { get; set; }


        /// <summary>
        /// Gets or sets the ID of the owner (a Staff member) of the...
        /// </summary>
        public int? Owner_StaffId { get; set; }



        /// <summary>
        /// Gets or sets the Datum of room occupation.
        /// </summary>
        public string Diagnose_details { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Diagnose and Patient_Drug_Treatment
        /// </summary>

        public Patient_Drug_Treatments Patient_Drug_Treatments { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (Type == PersonType.None)
            //{
            //    yield return new ValidationResult("Type can't be None.", new[] { "Type" });
            //}

            if (DateCreated < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
            if (DateModified > DateTime.Now)
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }

            

            //foreach (var result in HomeAddress.Validate())
            //{
            //    yield return result;
            //}

            //foreach (var result in WorkAddress.Validate())
            //{
            //    yield return result;
            //}
        }
        #endregion

    }
}
