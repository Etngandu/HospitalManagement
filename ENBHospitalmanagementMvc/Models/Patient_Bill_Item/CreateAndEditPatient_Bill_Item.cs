using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;
using HospitalManagement.Entities.Collections;

namespace ENBHospitalmanagementMvc.Models
{
    public class CreateAndEditPatient_Bill_Item:IValidatableObject
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public decimal Totalcost { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Quantity==0)
            {
                yield return new ValidationResult("Date_stay_from not good.", new[] { "Date_stay_from" });
            }
            if (Totalcost ==0)
            {
                yield return new ValidationResult("Date_depart_to not good.", new[] { "Date_depart_to" });
            }
        }
    }
}