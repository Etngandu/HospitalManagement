using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;

namespace ENBASEHospitalManagementMvc.Models
{
    public class CreateAndEditStaff : IValidatableObject
    {
        public int Id { get; set; }

        [Required, DisplayName("First Name")]
        public string Staff_first_name { get; set; }
        public string EmailAddressText { get; set; }
        public string Staff_last_name { get; set; }
        public Staff_Category_Code Staff_Category_code { get; set; }
        public Gender Gender { get; set; }
        public Staff_JobTitle Staff_job_title { get; set; }        

        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        [DisplayName("Date of birth")]
        public DateTime Staff_birth_date { get; set; }
        public Address Addresses { get; set; }
        public string Other_staff_details { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Gender == Gender.None)
            {
                yield return new ValidationResult("Staff Gender can't be None.", new[] { "Gender" });
            }
        }
    }
}