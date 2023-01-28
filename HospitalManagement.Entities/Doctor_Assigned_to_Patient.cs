using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities
{
    public class Doctor_Assigned_to_Patient: DomainEntity<int>
    {
        /// <summary>
        /// Gets or sets the owner Patient.
        /// </summary>
        [Owner("key")]
        public Patient Owner_Patient { get; set; }
        public int? Owner_PatientId { get; set; }

        /// <summary>
        /// Gets or sets the owner Staff.
        /// </summary>
        [Owner("")]
        public Staff Owner_Staff { get; set; }
        public int Owner_StaffId { get; set; }

        /// <summary>
        /// Gets or sets the Datum of room occupation.
        /// </summary>
        public DateTime Date_Ass_from { get; set; }

        /// <summary>
        /// Gets or sets the Datum of departure from the room.
        /// </summary>
        public DateTime? Date_Ass_to { get; set; }


        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Date_Ass_from < DateTime.Now.AddDays(Constants.MaxUseTime * -1))
            {
                yield return new ValidationResult("Invalid range for Date_from;  must be between today and 5 days ago.", new[] { "Date_Ass_from" });
            }

            if (Date_Ass_from > DateTime.Now)
            {
                yield return new ValidationResult("Invalid range for Date_from;  must be between today and 5 days ago.", new[] { "Date_Ass_from" });
            }
            if (Date_Ass_to < Date_Ass_from)
            {
                yield return new ValidationResult("Invalid range for Date_to; must be after Date from.", new[] { "Date_Ass_to" });
            }

        }


    }
    #endregion
}

