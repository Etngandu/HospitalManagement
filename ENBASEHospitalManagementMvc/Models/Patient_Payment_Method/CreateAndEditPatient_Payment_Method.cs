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
    public class CreateAndEditPatient_Payment_Method: IValidatableObject
    {
        
        public int Patient_Payment_Method_Id { get; set; }
        public int Patient_Id { get; private set; }
        public Payment_Method_Code Payment_method_code { get; set; }
        public string Payment_method_details { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Payment_method_code == Payment_Method_Code.None)
            {
                yield return new ValidationResult("Payment_method_code can't be None.", new[] { "Payment_method_code" });
            }
        }

    }
}