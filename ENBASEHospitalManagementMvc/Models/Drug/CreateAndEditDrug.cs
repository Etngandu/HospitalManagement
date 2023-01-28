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
    public class CreateAndEditDrug: IValidatableObject
    {
        public int Id { get; set; }
        public Ref_Drug_Category Ref_Drug_Category { get; set; }
        public string Drug_name { get; set; }
        public string Drug_Description { get; set; }
        public decimal Drug_cost { get; set; }
        public string Drug_Other_details { get; set; }
        public Decimal Drug_Cost_unit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Ref_Drug_Category == Ref_Drug_Category.None)
            {
                yield return new ValidationResult("Ref_Drug_Category can't be None.", new[] { "Ref_Drug_Category" });
            }
        }

    }
}