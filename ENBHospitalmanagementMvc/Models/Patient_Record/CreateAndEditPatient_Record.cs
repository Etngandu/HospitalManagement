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
    public class CreateAndEditPatient_Record : IValidatableObject
    {
        public int Id { get; set; }
        public int Staff_Id { get; set; }
        public int Patient_Id { get; set; }
        public DateTime Admisssion_datetime { get; set; }
        public string Medical_Condition { get; set; }
        public string Other_Details { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Patient_Id==0)
            {
                yield return new ValidationResult("Date_stay_from not good.", new[] { "Date_stay_from" });
            }
            //if (Date_depart_to < DateTime.Now || Date_depart_to < Date_stay_from)
            //{
            //    yield return new ValidationResult("Date_depart_to not good.", new[] { "Date_depart_to" });
            //}
        }
    }
}