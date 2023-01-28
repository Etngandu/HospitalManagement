using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;

namespace ENBHospitalmanagementMvc.Models
{
    public class CreateAndEditPatient :IValidatableObject
    {
        public int Id { get; set; }

        [Required, DisplayName("First Name")]
        public string Patient_first_name { get; set; }       

        [Required, DisplayName("Last Name")]
        public string Patient_last_name { get; set; }

        [Required, DisplayName("Outpatient YN")]
        public string Outpatient_yn { get; set; }

        [Required, DisplayName("Hospital Number")]
        public string Hospital_Number { get; set; }

        [Required, DisplayName("Nhs Number")]
        public string nhs_number { get; set; }

        public Gender Gender { get; set; }

        
        [DisplayName("Date of birth")]
        public DateTime Date_Of_Birth { get; set; }        
        public string Height { get; set; }
        public string Weight { get; set; }
        [Required, DisplayName("Next of kin")]
        public string Next_of_kin { get; set; }
        [Required, DisplayName("Home phone")]
        public string Home_phone { get; set; }
        [Required, DisplayName("Work phone")]
        public string Work_phone { get; set; }
        [Required, DisplayName("Mobile phone")]
        public string Cell_Mobile_phone { get; set; }
        [Required, DisplayName("Other patient details")]
        public string Other_patient_details { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Gender == Gender.None)
            {
                yield return new ValidationResult("Patient Gender can't be None.", new[] { "Gender" });
            }
        }

    }
}