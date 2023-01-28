using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;

namespace ENBASEHospitalManagementMvc.Models
{
    public class CreateAndEditWard: IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ward Name")]
        public string Ward_Name { get; set; }

        [Required]
        [Display(Name = "Ward Location")]
        public string Ward_Location { get; set; }
        
        [Display(Name = "Ward Description")]
        public string Ward_description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Ward_Name))
            {
                yield return new ValidationResult("Ward_Name can't be None.", new[] { "Ward_Name" });
            }

            if (string.IsNullOrEmpty(Ward_Location))
            {
                yield return new ValidationResult("Ward_Location can't be None.", new[] { "Ward_Location" });
            }
        }
    }
}