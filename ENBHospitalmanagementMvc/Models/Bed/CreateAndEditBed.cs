using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;

namespace ENBHospitalmanagementMvc.Models
{
    public class CreateAndEditBed :IValidatableObject
    {
        public int Id { get; set; }
        public int WardId { get; set; }
        [Required]
        public string Bed_Number { get; set; }
        [Required]
        public string Bed_Location { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Bed_Number))
            {
                yield return new ValidationResult("Bed_Number can't be None.", new[] { "Bed_Number" });
            }

            if (string.IsNullOrEmpty(Bed_Location))
            {
                yield return new ValidationResult("Bed_Location can't be None.", new[] { "Bed_Location" });
            }
        }
    }
}