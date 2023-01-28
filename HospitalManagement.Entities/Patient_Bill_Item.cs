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
    /// Represents Patient_Bill_Items in the system.
    /// </summary>
    public class Patient_Bill_Item : DomainEntity<int>
    {

        #region Properties
        /// <summary>
        /// Gets or sets the owner (a Staff member) of the address.
        /// </summary>
        [Owner("")]
        public Patient_Bill Owner { get; set; }


        /// <summary>
        /// Gets or sets the Datum of room occupation.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the Datum of departure from the room.
        /// </summary>
        public decimal Totalcost { get; set; }




        #endregion

        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {



            if (Quantity == 0)
            {
                yield return new ValidationResult("quantity must be more than 0", new[] { "quantity" });
            }
            if (Totalcost == 0)
            {
                yield return new ValidationResult("totalcost must be more than 0", new[] { "totalcost" });
            }


        }
        #endregion
    }
}

