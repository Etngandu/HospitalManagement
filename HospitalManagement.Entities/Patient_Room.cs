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
    /// Represents Patient_Rooms in the system.
    /// </summary>
    public class Patient_Room : DomainEntity<int>
    {
        #region Properties


        /// <summary>
        /// Gets or sets the owner (a Staff member) of the address.
        /// </summary>
        [Owner("")]
        public Patient Owner_Patient { get; set; }


        /// <summary>
        /// Gets or sets the ID of the owner (a Patient) of the diagnose.
        /// </summary>
        public int Owner_PatientId { get; set; }

        /// <summary>
        /// Gets or sets the Number of the room
        /// </summary>
        /// 
        public string Room_Name { get; set; }


        /// <summary>
        /// Gets or sets the Datum of room occupation.
        /// </summary>
        public DateTime Date_stay_from { get; set; }

        /// <summary>
        /// Gets or sets the Datum of departure from the room.
        /// </summary>
        public DateTime Date_depart_to { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Date_stay_from < DateTime.Now.AddDays(Constants.MaxUseTime * -1))
            {
                yield return new ValidationResult("Invalid range for Date_stay_from; must be between today and 5 days ago.", new[] { "Date_stay_from" });
            }

            if (Date_stay_from > DateTime.Now)
            {
                yield return new ValidationResult("Invalid range for Date_stay_from; must be between today and 5 days ago.", new[] { "Date_stay_from" });
            }
            if (Date_depart_to < Date_stay_from)
            {
                yield return new ValidationResult("Invalid range for Date_depart_to; must be after Date_stay_from.", new[] { "Date_depart_to" });
            }

        }


    }
    #endregion


}

