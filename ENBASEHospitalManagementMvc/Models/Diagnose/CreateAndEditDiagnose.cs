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
    public class CreateAndEditDiagnose: IValidatableObject
    {
        public int Id { get; set; }
        public int Patient_Id { get; set; }
        public int Staff_Id { get; set; }
        public string Diagnose_details { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Patient_Id == 0)
            {
                yield return new ValidationResult("Payment_method_code can't be None.", new[] { "Payment_method_code" });
            }
        }
    }
}