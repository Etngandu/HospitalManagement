using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities
{
    /// <summary>
    /// Represents Patient_Records in the system.
    /// </summary>

    public class Patient_Record : DomainEntity<int>, IDateTracking
    {

        #region Properties
        /// <summary>
        /// Gets or sets the owner (a Staff member) of the address.
        /// </summary>
        [Owner("")]
        public Patient Owner_patient { get; set; }

        /// <summary>
        /// Gets or sets the owner address.
        /// </summary>
        [Owner("")]
        public Staff Owner_staff { get; set; }
        

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime Admisssion_datetime { get; set; }


        /// <summary>
        /// Gets or sets the staff member first name.
        /// </summary>
      
        public string Medical_Condition { get; set; }

        /// <summary>
        /// Gets or sets the Staff member middle name.
        /// </summary>
       
        public string Other_Details { get; set; }


        /// <summary>
        /// Gets or sets the date and time the object was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateModified { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// Validates this object. 
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Admisssion_datetime < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
            //if (Staff_birth_date > DateTime.Now)
            //{
            //    yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            //}

        }
        #endregion

    }
}
