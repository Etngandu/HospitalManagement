using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Entities.Collections;

namespace HospitalManagement.Entities
{
    /// <summary>
    /// Represents Patient_Rooms in the system.
    /// </summary>
   public class Patient_Bill: DomainEntity<int>, IDateTracking
    {
        #region Properties


        /// <summary>
        /// Gets or sets the owner (a Staff member) of the address.
        /// </summary>
        [Owner("key_patient")]
        public Patient Owner { get; set; } 

        /// <summary>
        /// Gets or sets the Datum of room occupation.
        /// </summary>
        public DateTime Date_bill_paid { get; set; }

        /// <summary>
        /// Gets or sets the Datum of departure from the room.
        /// </summary>
        public decimal Total_amount_due { get; set; }

       /// <summary>
        /// Gets or sets Other_patient_details.
        /// </summary>
        
        public string Other_bill_details { get; set; }

        /// <summary>
        /// Gets or sets many to many relationship between Patient and addres
        /// </summary>

        public Patient_Bill_Items Patient_Bill_Items { get; set; }

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
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Date_bill_paid < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }

            if (Total_amount_due == 0)
            {
                yield return new ValidationResult("total_amount_due mandatory", new[] { "total_amount_due" });
            }

            if (DateCreated < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
            if (DateModified > DateTime.Now)
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
            //if (Address_Owner_Id == 0)
            //{
            //    yield return new ValidationResult("", new[] { "Address_Owner_Id" });
        }


        }
        #endregion
    }

