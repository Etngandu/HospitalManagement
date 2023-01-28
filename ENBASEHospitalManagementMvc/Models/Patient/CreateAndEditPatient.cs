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
    public class CreateAndEditPatient :IValidatableObject
    {
        public int Id { get; set; }

        [Required, DisplayName("First Name")]
        public string Patient_first_name { get; set; }
        public string Patient_middle_name { get; set; }

        [Required, DisplayName("Last Name")]
        public string Patient_last_name { get; set; }
        public string Outpatient_yn { get; set; }

        public string Hospital_Number { get; set; }

        public string nhs_number { get; set; }

        public Gender Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        [DisplayName("Date of birth")]
        public DateTime Date_Of_Birth { get; set; }        
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Next_of_kin { get; set; }
        public string Home_phone { get; set; }
        public string Work_phone { get; set; }
        public string Cell_Mobile_phone { get; set; }
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