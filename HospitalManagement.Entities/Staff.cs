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
    public class Staff : DomainEntity<int>, IDateTracking
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Person class.
        /// </summary>
        public Staff()
        {
            Addresses = new Addresses();            
        }

        #endregion



        #region "Public Properties"

        /// <summary>
        /// Gets or sets Staff_Category_code
        /// </summary>
        [Required]
        public Staff_Category_Code Staff_Category_code { get; set; }

        /// <summary>
        /// Gets or sets the gender of this staff person.
        /// </summary>
        [Required]
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the Staff member job_title.
        /// </summary>
        [Required]
        public Staff_JobTitle Staff_job_title { get; set; }



        /// <summary>
        /// Gets or sets the staff member first name.
        /// </summary>
        [Required]
        public string Staff_first_name { get; set; }        

        /// <summary>
        /// Gets or sets the staff member last name.
        /// </summary>
        
        public string Staff_last_name { get; set; }

        /// <summary>
        /// Gets or sets the text of the e-mail address.
        /// </summary>
        [Required]
        [EmailAddressAttribute]
        public string EmailAddressText { get; set; }



        /// <summary>
        /// Gets or sets the staff member date of birth.
        /// </summary>
        public DateTime Staff_birth_date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets many to many relationship between Patient and addres
        /// </summary>

        public Addresses Addresses { get; set; }

        /// <summary>
        /// Gets or sets the Other_staff_details.
        /// </summary>
        
        public string Other_staff_details { get; set; }


        /// <summary>
        /// Gets the full name of this patient.
        /// </summary>
        public string FullName
        {
            get
            {
                string temp = Staff_first_name ?? string.Empty;

                if (!string.IsNullOrEmpty(Staff_last_name))
                {
                    if (temp.Length > 0)
                    {
                        temp += " ";
                    }
                    temp += Staff_last_name;
                }
                else
                {

                    temp += Staff_last_name;
                }

                return temp;
            }
        }


        /// <summary>
        /// Gets or sets the Image path of this lawyer.
        /// </summary>
        public string ImagePath { get; set; }
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
            //if (Type == PersonType.None)
            //{
            //    yield return new ValidationResult("Type can't be None.", new[] { "Type" });
            //}

            if (Staff_birth_date < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
            if (Staff_birth_date > DateTime.Now)
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }

            //foreach (var result in PhoneNumbers.Validate())
            //{
            //    yield return result;
            //}

            //foreach (var result in OrderPickings.Validate())
            //{
            //    yield return result;
            //}

            //foreach (var result in EmailAddresses.Validate())
            //{
            //    yield return result;
            //}

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
