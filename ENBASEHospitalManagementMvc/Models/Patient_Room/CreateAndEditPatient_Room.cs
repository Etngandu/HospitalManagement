using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;
using HospitalManagement.Entities.Collections;

namespace ENBASEHospitalManagementMvc.Models
{
    public class CreateAndEditPatient_Room: IValidatableObject
    {
        public int Patient_Id { get; set; }
        public int Patient_Room_Id { get; set; }
        public DateTime Date_stay_from { get; set; }
        public DateTime Date_depart_to { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date_stay_from < DateTime.Now)
            {
                yield return new ValidationResult("Date_stay_from not good.", new[] { "Date_stay_from" });
            }
            if (Date_depart_to<DateTime.Now || Date_depart_to<Date_stay_from)
            {
                yield return new ValidationResult("Date_depart_to not good.", new[] { "Date_depart_to" });
            }
        }
    }
}