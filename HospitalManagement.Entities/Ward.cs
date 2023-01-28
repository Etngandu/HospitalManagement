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
  public class Ward: DomainEntity<int>
    {


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Person class.
        /// </summary>
        public Ward()
        {
            Beds = new Beds();            
        }

        #endregion

        /// <summary>
        /// Gets or sets the Ward_Name.
        /// </summary>
        public string Ward_Name { get; set; }

        /// <summary>
        /// Gets or sets the Ward_Location.
        /// </summary>
        public string Ward_Location { get; set; }

        /// <summary>
        /// Gets or sets the Ward_description.
        /// </summary>
        public string Ward_description { get; set; }

        /// <summary>
        /// Gets or sets many to many relationship between Ward and Bed
        /// </summary>

        public Beds Beds { get; set; }

        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            

            if (String.IsNullOrEmpty(Ward_Name))
            {
                yield return new ValidationResult("Ward_Name can't be null", new[] { "Ward_Name" });
            }
            if (string.IsNullOrEmpty(Ward_Location))
            {
                yield return new ValidationResult("Ward_Location can't be null", new[] { "Ward_Location" });
            }

            if (string.IsNullOrEmpty(Ward_description))
            {
                yield return new ValidationResult("Ward description can't be null.", new[] { "Ward_description" });
            }

            
        }
        #endregion
    }
}
