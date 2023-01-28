using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Collections;
using HospitalManagement.Entities.Enums;

namespace ENBHospitalmanagementMvc.Models
{
    public class CreateAndEditStaff : IValidatableObject
    {
        public int Id { get; set; }

        [Required, DisplayName("First Name")]
        public string Staff_first_name { get; set; }

        [Required, DisplayName("Email")]
        public string EmailAddressText { get; set; }

        [Required, DisplayName("Last Name")]
        public string Staff_last_name { get; set; }

        [Required, DisplayName("Staff Category code")]
        public Staff_Category_Code Staff_Category_code { get; set; }
        public Gender Gender { get; set; }

        [Required, DisplayName("Staff Job Title")]
        public Staff_JobTitle Staff_job_title { get; set; }        

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Date of birth")]
        public DateTime Staff_birth_date { get; set; }
        public Addresses Addresses { get; set; }
        [DisplayName("Other staff details")]
        public string Other_staff_details { get; set; }        

        [DisplayName("Image")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public CreateAndEditStaff()
        {

            ImagePath = "~/AppFiles/Images/user_add.png";
            // ImageUpload = HttpPostedFile(object);

        }




        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Gender == Gender.None)
            {
                yield return new ValidationResult("Staff Gender can't be None.", new[] { "Gender" });
            }
        }
    }
}