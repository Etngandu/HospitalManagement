using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities
{
   public class Bed : DomainEntity<int>
    {
        /// <summary>
        /// Gets or sets the owner (a Ward) of the Bed.
        /// </summary>
        [Owner("key_ward")]
        public Ward Owner { get; set; }

        /// <summary>
        /// Gets or sets the Bed_Number.
        /// </summary>
        public string Bed_Number { get; set; }

        /// <summary>
        /// Gets or sets the Bed_Location.
        /// </summary>
        public string Bed_Location { get; set; }

        /// <summary>
        /// Gets or sets the ID of the owner (a Ward) of the Bed.
        /// </summary>
        public int OwnerId { get; set; }


        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            if (String.IsNullOrEmpty(Bed_Number))
            {
                yield return new ValidationResult("Ward_Name can't be null", new[] { "Ward_Name" });
            }
            if (string.IsNullOrEmpty(Bed_Location))
            {
                yield return new ValidationResult("Ward_Location can't be null", new[] { "Ward_Location" });
            }        


        }
        #endregion

    }
}
