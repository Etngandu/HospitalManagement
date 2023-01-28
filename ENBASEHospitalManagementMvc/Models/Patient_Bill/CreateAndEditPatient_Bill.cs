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
    public class CreateAndEditPatient_Bill: IValidatableObject
    {
        public int Patient_Id { get; set; }
        public int Patient_Bill_Id { get; set; }
        public DateTime Date_bill_paid { get; set; }
        public decimal Total_amount_due { get; set; }
        public string Other_bill_details { get; set; }
        public Patient_Bill_Items Patient_Bill_Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date_bill_paid == DateTime.Now.AddDays(15))
            {
                yield return new ValidationResult("Date_bill_paid can't exceed 15 days.", new[] { "Date_bill_paid" });
            }
            //if (decimal.Total_amount_due  )
            //{
            //    yield return new ValidationResult("Date_bill_paid can't exceed 15 days.", new[] { "Date_bill_paid" });
            //}
        }
    }
}