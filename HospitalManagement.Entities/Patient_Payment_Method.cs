using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Entities.Enums;

namespace HospitalManagement.Entities
{
    /// <summary>
    /// Represents Patient_Bill_Items in the system.
    /// </summary>
  public class Patient_Payment_Method :  DomainEntity<int>
    {
        #region Properties
       /// <summary>
        /// Gets or sets the owner (Patient) of the address.
        /// </summary>
        [Owner("")]
        public Patient Owner { get; set; }


        /// <summary>
        /// Gets or sets the payment_method_code.
        /// </summary>
        public Payment_Method_Code  Payment_method_code { get; set; }


        /// <summary>
        /// Gets or sets the Datum of room occupation.
        /// </summary>
        public string Payment_method_details { get; set; }




        #endregion

        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {           

            if (Payment_method_code == Payment_Method_Code.None)
            {
                yield return new ValidationResult("payment_method_code can not be None", new[] { "payment_method_code" });
            }            

        }
        #endregion
    }
}
