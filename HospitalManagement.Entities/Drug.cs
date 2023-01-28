using HospitalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Entities.Enums;
using HospitalManagement.Entities.Collections;

namespace HospitalManagement.Entities
{
   public class Drug: DomainEntity<int>, IDateTracking
    {

        #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the Drug class.
        ///// </summary>
        public Drug()
        {
            Patient_Drug_Treatments = new Patient_Drug_Treatments();
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets Ref_Drug_Category.
        /// </summary>

        public Ref_Drug_Category Ref_Drug_Category { get; set; }

        /// <summary>
        /// Gets or sets Patient_middle_name
        /// </summary>
        
        public string Drug_name { get; set; }

        /// <summary>
        /// Gets or sets Patient_last_name.
        /// </summary>
        
        public string Drug_Description { get; set; }

        /// <summary>
        /// Gets or sets Patient_middle_name
        /// </summary>

        public decimal Drug_cost { get; set; }

        /// <summary>
        /// Gets or sets Patient_last_name.
        /// </summary>

        public string Drug_Other_details { get; set; }

        /// <summary>
        /// Gets or sets Patient_last_name.
        /// </summary>

        public Decimal Drug_Cost_unit { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Drug and Patient_Drug_Treatment
        /// </summary>

        public Patient_Drug_Treatments Patient_Drug_Treatments { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Ref_Drug_Category == Ref_Drug_Category.None)
            {
                yield return new ValidationResult("Ref_Drug_Category can't be None.", new[] { "Ref_Drug_Category" });
            }

            if (string.IsNullOrEmpty(Drug_name))
            {
                yield return new ValidationResult("Drug_name can't be null", new[] { "Drug_name" });
            }
            if (String.IsNullOrEmpty(Drug_Description))
            {
                yield return new ValidationResult("Drug_Description can't be null.", new[] { "Drug_Description" });
            }

            
        }
        #endregion


    }
}
